using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Analytics.Mentions;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Services.Analytics.Common;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps
{
    [Binding]
    public class AnalyticsMomentumSteps : AuthApiSteps
    {
        public const string GET_WIDGET_DATA_KEY = "GetWidgetData";
        public const string EXPECTED_NUMBER_OF_DATA_KEY = "ExpectedNumberOfData";

        public AnalyticsMomentumSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"I perform a GET for mentions momentum on the last day of the previous year with type '(.*)' and frequency '(daily|weekly|monthly|yearly|.*)'")]
        public void WhenIPerformAGETForMentionsMomentumOnTheLastDayOfThePreviousYearWith(TypeId type, Frequency frequency)
        {
            var lastDay = DateTime.Parse($"12/31/{DateTime.Now.Year - 1}");
            WhenIPerformAGETForMentionsMomentumWith((int)DateTime.Now.Subtract(lastDay).TotalDays, type, frequency);
        }

        [When(@"I perform a GET for mentions momentum with days '(.*)', type '(.*)' and frequency '(.*)'")]
        public void WhenIPerformAGETForMentionsMomentumWith(int daysPrevious, TypeId type, Frequency frequency)
        {
            var previousDate = DateTime.Now.AddDays(-daysPrevious);
            var widgetData = new MentionsOverTimeService(SessionKey).GetMentionsMomentum(type, MentionsOverTimeService.MentionsCalculation.Momentum, frequency, previousDate, DateTime.Now);
            PropertyBucket.Remember(GET_WIDGET_DATA_KEY, widgetData);
            int dataCount = 0;
            // handle the case when months/years wrap around by one day
            if (daysPrevious == 1 && frequency == Frequency.Monthly)
            {
                dataCount = DateTime.Now.Month - previousDate.Month;
            }
            else if (daysPrevious == 1 && frequency == Frequency.Yearly)
            {
                dataCount = DateTime.Now.Year - previousDate.Year;
            }
            PropertyBucket.Remember(EXPECTED_NUMBER_OF_DATA_KEY, dataCount);
        }

        [Then(@"the mentions momentum endpoint (has|does not have) series data")]
        public void ThenTheMentionsMomentumEndPointHasSeriesData(string hasDataOrDoesNotHave)
        {
            var hasData = (hasDataOrDoesNotHave.ToLower() == "has");

            var widgetData = PropertyBucket.GetProperty<WidgetData>(GET_WIDGET_DATA_KEY);
            Assert.IsTrue(widgetData.ShowTotalsInLegend, "Show Totals In Legend is false");
            if (hasData)
            {
                Assert.That(widgetData.Series.ToList().Count, Is.GreaterThan(0), "No series data found in widget data");
                foreach (AnalyticsSeries series in widgetData.Series)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(series.Name), "Series name is null or empty");
                    Assert.That(series.Data.Count, Is.GreaterThan(0), $"Series {series.Name} had no data.");

                    var sum = series.GetDataSum();
                    Assert.IsNull(series.Average, $"Series '{series.Name}' did not have a null Average.");
                    Assert.AreEqual(sum, series.Total,
                        $"Sum of series data did not match the series total, data: '{series.GetDataAsString()}'");
                }
            }
            else
            {
                Assert.AreEqual(0, widgetData.Series.ToList().Count,
                    $"Unexpected series data found in widget data: '{string.Join(", ", widgetData.Series.Select(s => s.Name)) }'");
            }
        }
    }
}
