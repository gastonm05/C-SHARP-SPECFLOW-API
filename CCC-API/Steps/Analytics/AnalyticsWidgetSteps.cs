using BoDi;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.News;
using CCC_API.Services.Analytics;
using CCC_API.Services.Analytics.Mentions;
using CCC_API.Services.Common;
using CCC_API.Services.News;
using CCC_API.Services.News.DB;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using Org.BouncyCastle.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static CCC_API.Services.Analytics.Common;
using Is = NUnit.Framework.Is;


namespace CCC_API.Steps.Analytics
{
    public class AnalyticsWidgetSteps : AuthApiSteps
    {
        public const string RESPONSE = "response";
        public const string SETTINGS = "settings";
        private readonly MentionsOverTimeService _widgetService;
        private DMAWidgetServices _dMAWidgetServices;
        private SentimentScoreService _sentimentScoreService;

        public AnalyticsWidgetSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _widgetService = new MentionsOverTimeService(SessionKey);
            _dMAWidgetServices = new DMAWidgetServices(SessionKey);
            _sentimentScoreService = new SentimentScoreService(SessionKey);
        }

        private static readonly MyCoverageNewsRepository MyCoverageNews = new MyCoverageNewsRepository();

        [BeforeFeature, Scope(Feature = "AnalyticsFilterByTone")]
        public static void CreateNewsInMyCoverage()
        {
            try
            {
                // FIXME - add FeatureContext parameter injection & read feature file, after Specflow 2.2 is updated.
                // See the specflow bug: https://github.com/techtalk/SpecFlow/pull/779
                // Temp fix
                const string company = "Analytics company with features enabled and dynamic news";

                // User to create test data
                var sessionKey = new LoginSteps(new ObjectContainer(), "AnalyticsFilterByTone")
                    .GivenSharedSessionForUserWithEdition(DynamicUser.PermissionType.system_admin, company);

                var companyInfo = new AccountInfoService(sessionKey).Me;
                var companyId = companyInfo.Account.Id;
                Assert.That(companyId, Is.GreaterThan(0), "Wrong company ID");

                // Clean company from previous crap
                using (var service = new NewsDbService(companyId))
                {
                    service.DeleteNewsForCompany();
                }

                // Create toned news for last several days
                var newsService = new NewsViewService(sessionKey);
                foreach (var tone in Enum.GetValues(typeof(ToneId)).Cast<ToneId>())
                {
                    var newsDate = DateTime.Now.AddDays(-new SecureRandom().Next(0, 30));
                    var item = newsService.CreateTestTonedNewsWithReachPubValue(tone, newsDate);
                    MyCoverageNews.RegisterNews(item);
                }

                // Some news without tone
                var itemWithoutTone = newsService.CreateTestNewsItemForDate(DateTime.Now.AddDays(-2));
                MyCoverageNews.RegisterNews(itemWithoutTone);

                // Some Week ago
                var weekAgo = newsService.CreateTestTonedNewsWithReachPubValue(ToneId.Neutral, DateTime.Now.AddDays(-7));
                MyCoverageNews.RegisterNews(weekAgo);

                // Some Month ago
                var monthAgo = newsService.CreateTestTonedNewsWithReachPubValue(ToneId.Positive, DateTime.Now.AddMonths(-1));
                MyCoverageNews.RegisterNews(monthAgo);

                // Add one news a year back for YearOverYear calc
                var yearAgo = newsService.CreateTestTonedNews(ToneId.Negative, DateTime.Now.AddDays(-1).AddYears(-1));
                MyCoverageNews.RegisterNews(yearAgo);

                var yearAgo2 = newsService.CreateTestTonedNews(ToneId.Negative, DateTime.Now.AddDays(-1).AddYears(-1));
                MyCoverageNews.RegisterNews(yearAgo2);
            }
            catch (Exception e)
            {
                Assert.Ignore("Something wrong with creating test data\n" + e);
            }
        }

        [When(@"I GET a widget with settings:")]
        public void WhenIgetaWidgetWithSettings(Table table)
        {
            var requestSettings = table.CreateInstance<AnalyticsWidgetSettings>();
            requestSettings.CreateScratchTable = true;

            var resp = _widgetService.GetWidgetWithSettings(requestSettings);

            PropertyBucket.Remember(RESPONSE, resp);
            PropertyBucket.Remember(SETTINGS, requestSettings);
        }

        [When(@"I perform a GET for DMA widget with '(.*)'")]
        public void WhenIPerformAGETForDMAWidgetWith(string type)
        {
            DMAWidget response = _dMAWidgetServices.GetDMA(type);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [Then(@"response is OK with type '(.*)'")]
        public void ThenReposnseIsOkWithType(string type)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<WidgetData>>(RESPONSE);
            var data = response.CheckCodeGetData();
            PropertyBucket.Remember(RESPONSE, data, true);

            var expType = string.IsNullOrEmpty(type) ? null : type;
            Assert.That(data.Type, Is.EqualTo(expType), "Wrong type");
        }

        [Then(@"maxseries is up to '(.*)'")]
        public void ThenMaxseriesIs(int maxseries)
        {
            var expSettings = PropertyBucket.GetProperty<AnalyticsWidgetSettings>(SETTINGS);
            var data = PropertyBucket.GetProperty<WidgetData>(RESPONSE);

            Assert.That(data.Series.Count, Is.LessThanOrEqualTo(expSettings.Maxseries), "Wrong number of series");
        }

        [Then(@"news filtered by tone with '(.*)' and '(.*)' for series: '(.*)'")]
        public void ThenNewsFilteredByToneWithAndForSeries(Frequency freq, Calculation calc, string series)
        {
            var expseries = series.Split(',').Select(_ => _.Replace(" ", "")).ToList();
            Assert.That(expseries, Is.Not.Null.And.Not.Empty, "No series to test. Please specify series correcly.");
            expseries.ForEach(s => ThenNewsFilteredByToneWithCalculation(s, freq, calc));
        }

        [Then(@"series '(.*)' of news filtered by specified tone with '(.*)' and '(.*)'")]
        public void ThenNewsFilteredByToneWithCalculation(string seriesName, Frequency freq, Calculation calc)
        {
            var expSettings = PropertyBucket.GetProperty<AnalyticsWidgetSettings>(SETTINGS);
            var data = PropertyBucket.GetProperty<WidgetData>(RESPONSE);

            // Facet for tone
            Predicate<NewsItem> facetByTone = news => // All OR Toned
                expSettings.Tones == null || (int)expSettings.Tones == news.Tone?.Id;

            // How to sort news by date
            Func<DateTime, DateTime> startDateFunc = _widgetService.GetStartDateLogicBasedOnFrequency(freq);

            // Expected sorted news by tone & dates range
            IEnumerable<Tuple<DateTime, List<NewsItem>>> expTonedNewsByDate =
                MyCoverageNews.GetCreatedNewsItemsGroupedBy(startDateFunc,
                    news =>
                        facetByTone(news)
                            && news.NewsDate
                                   .IsBetweenInclusive(startDateFunc(expSettings.StartDate), expSettings.EndDate)).ToList(); // Facet by start & end date
            // False positive test check
            Assert.That(expTonedNewsByDate.Any(), "No testing data to test this scenario");
            var actualSeries = data.Series.Where(s => s.Name == seriesName)
                                   .FirstOrError($"No series in response for: {seriesName}")
                                   .GetDataParsedTime();
            // For each of expected toned news by date
            foreach (var expPair in expTonedNewsByDate)
            {
                var actualItemsForDate = actualSeries
                    .Where(actPair => actPair.Item1 == expPair.Item1)
                    .FirstOrError($"No date present in series {expPair.Item1}").Item2;

                // How the chart series behaves
                var chartLogic = _widgetService.GetSeriesLogic(seriesName);

                // Apply widget logic to expected set of news
                var expCount = chartLogic(expPair.Item2);

                // Apply calculation
                if (calc == Calculation.YearOverYear) // TODO Logic for momentum etc
                {
                    var date = expPair.Item1.AddYears(-1);
                    var previousYear =
                        MyCoverageNews.GetCreatedNewsItemsGroupedBy(startDateFunc,
                            news => facetByTone(news) && startDateFunc(news.NewsDate) == date).ToList();
                    // If any news from previous year - subtract those
                    expCount -= (previousYear.Any() ? chartLogic(previousYear.FirstOrError().Item2) : 0);
                }

                Assert.That(actualItemsForDate, Is.EqualTo(expCount),
                    $"Series: {seriesName} items count for date {expPair.Item1} unxpected");
            }
        }

        [Then(@"news filtered by tone grouped for period for series: '(.*)'")]
        public void ThenNewsFilteredByToneGroupedForPeriodForSeries(string series)
        {
            var expseries = series.Split(',').Select(_ => _.Replace(" ", "")).ToList();
            Assert.That(expseries, Is.Not.Null.And.Not.Empty, "No series to test. Please specify series correcly.");

            expseries.ForEach(s => ThenNewsFilteredByToneWithCalculation(s, Frequency.Daily, Calculation.Count));
        }

        [Then(@"series not present: '(.*)'")]
        public void ThenSeriesNotPresent(string excluded)
        {
            var data = PropertyBucket.GetProperty<WidgetData>(RESPONSE);
            var notExpected = excluded.Split(',').ToList();

            var notFilteredSeries = data.Series.Where(s => notExpected.Contains(s.Name)).ToList();
            Assert.That(notFilteredSeries, Is.Empty, "Some series were not filtered");
        }

        [Then(@"the data is the correct")]
        public void ThenTheDataIsTheCorrect()
        {
            DMAWidget response = PropertyBucket.GetProperty<DMAWidget>(RESPONSE);

            if (response.DataPoints.Count() == 0) Assert.Ignore("There are not data to verify");

            Assert.That(response.Name.Contains("Bubble Map"), "The name is not correct");

            foreach (var data in response.DataPoints)
            {
                Assert.That(data.Name, Is.Not.Null.And.Not.Empty, "The name is not correct");
                Assert.That(data.Id, Is.GreaterThan(0), "The Id is not correct");
                Assert.That(data.Lat, Is.Not.Null, "The Lat is not correct");
                Assert.That(data.Lon, Is.Not.Null, "The Lon is not correct");
                Assert.That(data.Z, Is.GreaterThan(0), "The data is not correct");
            }
        }


        [When(@"I perform a GET for Company sentiment score Widget '(.*)'")]
        public void WhenIPerformAGETForCompanySentimentScoreWidget(String type)
        {
            var response = _sentimentScoreService.GetSentimentScore(type);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [Then(@"the Sentiment score endpoint has the correct response for '(.*)'")]
        public void ThenTheSentimentScoreEndpointHasTheCorrectResponse(String type)
        {
            SentimentScoreWidget response = PropertyBucket.GetProperty<SentimentScoreWidget>(RESPONSE);

            if (response.Series.Count() == 0) Assert.Ignore("There are not data to verify");
           

            foreach (var dataToCheck in response.Series)
            {
                Assert.That(dataToCheck.Name, Is.Not.Null.And.Not.Empty, "The name is not correct");
                Assert.That(dataToCheck.Id, Is.Not.Null.And.Not.Empty, "The Id is not correct");
                Assert.That(dataToCheck.Data, Is.Not.Null.And.Not.Empty, "The data is not correct");
                Assert.That(dataToCheck.Total, Is.Not.Null, "The total is not correct");    
                
            }
            if (type.Equals ("Line"))
            {
                Assert.That(response.type, Is.EqualTo("areaspline"), "the type is not correct");              
            }
            if (type.Equals("HorizontalBar"))
            {
                Assert.That(response.type, Is.EqualTo("column"), "the type is not correct");
            }
        }
        

    }
}

