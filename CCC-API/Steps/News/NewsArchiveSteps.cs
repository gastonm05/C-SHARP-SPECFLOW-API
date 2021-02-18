using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using System.Linq;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;
using CCC_Infrastructure.Utils;

namespace CCC_API.Steps.News
{
    public class NewsArchiveSteps : AuthApiSteps
    {
        private NewsArchiveService _newsArchiveService;
        private const string GET_NEWS_ARCHIVE_RESPONSE_KEY = "GetNewsArchiveResponse";
        private const string GET_NEWS_ARCHIVE_IMPORT_RESPONSE = "GetNewsArchiveImportResponse";
        private const string GET_NEWS_PREFERENCES_RESPONSE = "GetNewsPreferences";

        public NewsArchiveSteps(IObjectContainer objectContainer) : base(objectContainer) {
            _newsArchiveService = new NewsArchiveService(SessionKey);
        }

        #region When Steps
        [When(@"I search for news archive by keywords with the value '(.*)' and by sources with a value of '(.*)'")]
        public void WhenISearchArchiveNewsByKeywordsAndSources(string keywords, string sources)
        {
            var response = _newsArchiveService.GetNewsArchiveByKeywordsAndSource(keywords, sources);
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_RESPONSE_KEY, response);
        }

        [When(@"I search for news archive by keywords with a value of '(.*)'")]
        public void WhenISearchArchiveNewsByKeywords(string keywords)
        {
            var response = _newsArchiveService.GetNewsArchiveByKeywords(keywords);
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_RESPONSE_KEY, response);
        }

        [When(@"I search for news archive by keywords with the value '(.*)' and by source name with a value of '(.*)'")]
        public void WhenISearchForNewsArchiveByKeywordsAndGivenSourceName(string keywords, string sourceName)
        {
            var response = _newsArchiveService.GetNewsArchiveByKeywordsAndSourceName(keywords, sourceName);
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_RESPONSE_KEY, response);
        }

        [When(@"I add the first '(.*)' results to My Coverage")]
        public void WhenIAddTheFirstResultsToMyCoverage(int limit)
        {
            var items = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY).Data.Items;
            var key = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY).Data.Key;
            var response = _newsArchiveService.AddNewsArchiveItemsToMyCoverage(items, key, limit);
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_IMPORT_RESPONSE, response);
        }

        [When(@"I perform a GET for archve search preferences")]
        public void WhenIPerformAGETForArchveSearchPreferences()
        {
            var response = _newsArchiveService.GetNewsPreferences();
            PropertyBucket.Remember(GET_NEWS_PREFERENCES_RESPONSE, response);
        }
        #endregion

        #region Then Steps
        [Then(@"the News Archive Endpoint has the correct response")]
        public void ThenTheNewsArchiveEndpointHasTheCorrectResponse()
        {
            IRestResponse<NewsViewArchive> response = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            NewsViewArchive newsArchiveView = response.Data;
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(newsArchiveView.Key, "Key was null");
                Assert.IsNotNull(newsArchiveView.TotalCount, "TotalCount was null");
                Assert.IsNotNull(newsArchiveView.ItemCount, "ItemCount was null");
                Assert.IsNotNull(newsArchiveView.ActiveCount, "ActiveCount was null");
                Assert.That(newsArchiveView.Items.Count, Is.GreaterThan(0), "Expected Item Count to be greater than zero but was not");
            });
        }

        [Then(@"I should see a value of '(.*)' for AddNewsToLimit")]
        public void ThenIShouldSeeAGivenValueForAddNewsToLimit(int limit)
        {
            IRestResponse<NewsViewArchive> response = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY);
            NewsViewArchive newsArchiveView = response.Data;
            Assert.That(newsArchiveView._meta.AddToNewsLimit, Is.EqualTo(limit), "The value for AddNewsToLimit is not correct");
        }

        [Then(@"I should see all the archive clips are coming from NLA Web source")]
        public void ThenIShouldSeeAllTheArchiveClipsAreComingFromNLAWebSource()
        {
            IRestResponse<NewsViewArchive> response = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            NewsViewArchive newsViewArchive = response.Data;
            Assert.True(newsViewArchive.Items.Any(item =>
                            (item.Feed.Key.Equals("Wise")) || (item.Feed.Key.Equals("NLAWeb")) || (item.Feed.Key.Equals("VisibleNLAEclipsWeb"))),
                            $"Some news coming from other than NLA Web source");
        }
        [Then(@"I should see all the archive clips are coming from CLA Web source")]
        public void ThenIShouldSeeAllTheArchiveClipsAreComingFromCLAWebSource()
        {
            IRestResponse<NewsViewArchive> response = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            NewsViewArchive newsViewArchive = response.Data;
            Assert.True(newsViewArchive.Items.Any(item =>
                        (item.Feed.Key.Equals("Wise")) || (item.Feed.Key.Equals("CLAWeb"))),
                        $"Some news coming from other than CLA Web source");
        }

        [Then(@"the News Archive Import Endpoint has the correct response")]
        public void ThenTheNewsArchiveImportEndpointHasTheCorrectResponse()
        {
            IRestResponse<NewsArchiveImport> response = PropertyBucket.GetProperty<IRestResponse<NewsArchiveImport>>(GET_NEWS_ARCHIVE_IMPORT_RESPONSE);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode, response.Content);
        }

        [Then(@"I should see the value for Archive Search Days Limit")]
        public void ThenIShouldSeeTheValueForArchiveSearchDaysLimit()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsArchivePreferences>>(GET_NEWS_PREFERENCES_RESPONSE);
            Assert.That(response.Data.ArchiveSearchDaysLimit, Is.Not.Null, "Archive Search Days Limit preference was not set");
        }

        [Then(@"the news archive endpoint response is correct")]
        public void ThenTheNewsArchiveEndpointResponseCodeShouldBe()
        {
            IRestResponse<NewsViewArchive> response = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, response.Content);
        }

        [Then(@"all the news clips are from TVEyes")]
        public void ThenAllTheNewsClipsAreFromTVEyes()
        {
            IRestResponse<NewsViewArchive> response = PropertyBucket.GetProperty<IRestResponse<NewsViewArchive>>(GET_NEWS_ARCHIVE_RESPONSE_KEY);
            var items = response.Data.Items;
            foreach(var item in items)
            { 
                Assert.True(item.Feed.Id.Equals(220) || item.Feed.Id.Equals(221), $"{item.Id} has an unexpectedd Feed Id -> {item.Feed.Id}");
            }
        }
        #endregion
    }
}
