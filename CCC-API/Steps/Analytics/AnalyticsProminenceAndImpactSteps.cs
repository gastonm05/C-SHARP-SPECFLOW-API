using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Analytics.Mentions;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Services.Analytics.Common;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps
{
    [Binding]
    public class AnalyticsProminenceAndImpactSteps : AuthApiSteps
    {
        public const string GET_WIDGET_DATA_KEY = "GetWidgetData";
        public const string CALCULATED_AVERAGES_KEY = "CalculatedAverages";

        public AnalyticsProminenceAndImpactSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"I perform a GET for company (impact|prominence) with type '(.*)', y axis metric '(.*)' and frequency '(.*)'")]
        public void WhenIPerformAGETForCompanyImpactOrProminence(string impactOrProminence, TypeId type, YAxisMetric yAxisMetric, Frequency frequency)
        {
            // get an appropriate startDate for the test case
            DateTime startDate;
            switch (frequency)
            {
                case Frequency.Monthly:
                    startDate = DateTime.Now.AddDays(-366); // leap year
                    break;
                case Frequency.Yearly:
                    startDate = DateTime.Now.AddDays(-1096); // 3 years + 1 day
                    break;
                default:
                    startDate = DateTime.Now.AddDays(-31); // longest month
                    break;
            }

            // get test data
            var widgetData = (impactOrProminence.ToLower() == "impact") ?
                new ImpactService(SessionKey).GetCompanyImpact(type, yAxisMetric, frequency, startDate) :
                new ProminenceService(SessionKey).GetCompanyProminence(type, yAxisMetric, frequency, startDate);
            PropertyBucket.Remember(GET_WIDGET_DATA_KEY, widgetData);

            // get total data in case we need it for average
            if (yAxisMetric == YAxisMetric.Average)
            {
                var calculatedAverages = new Dictionary<string, double>();
                // store totals by series name (temp storage)
                var totalData = (impactOrProminence.ToLower() == "impact") ? // total
                    new ImpactService(SessionKey).GetCompanyImpact(type, YAxisMetric.Total, frequency, startDate) :
                    new ProminenceService(SessionKey).GetCompanyProminence(type, YAxisMetric.Total, frequency, startDate);

                totalData.Series.ForEach(series => {
                    Assert.That(calculatedAverages.Keys.Contains(series.Name), Is.False,
                        $"Duplicate series name detected: '{series.Name}'");

                    if (series.Average != null)
                    {
                        var average = (series.NumberOfClips == null || series.NumberOfClips == 0) ? 0 : Math.Round((double)series.Sum / (long)series.NumberOfClips);
                        calculatedAverages.Add(series.Name, average);
                    }
                });
                PropertyBucket.Remember(CALCULATED_AVERAGES_KEY, calculatedAverages);
            }
        }

        [Then(@"the company (?:impact|prominence) endpoint has series data with total and average")]
        public void ThenTheCompanyProminenceEndpointHasSeriesData()
        {
            var widgetData = PropertyBucket.GetProperty<WidgetData>(GET_WIDGET_DATA_KEY);
            Assert.IsTrue(widgetData.ShowTotalsInLegend, "Show Totals In Legend is false");
            Assert.That(widgetData.Series.ToList().Count, Is.GreaterThan(0), "No series data found in widget data");
            foreach (AnalyticsSeries series in widgetData.Series)
            {
                Assert.IsFalse(string.IsNullOrEmpty(series.Name), "Series name is null or empty");
                Assert.That(series.Data.Count, Is.GreaterThan(0), $"Series '{series.Name}' had no data.");

                if (series.Average == null) // we're verifying yaxismetric as total
                {
                    var sum = series.GetDataSum();
                    Assert.That(series.Total, Is.GreaterThan(0), $"Series total not greater than 0, series: {series.Name}, data: {series.GetDataAsString()}");
                    Assert.That(sum, Is.GreaterThan(0), $"Sum of series data not greater than 0, series: {series.Name}, data: {series.GetDataAsString()}");
                    Assert.AreEqual(sum, series.Total, $"Sum of series data did not match the series total, series: {series.Name}, data: {series.GetDataAsString()}");
                }
                else // we're verifying yaxismetric as average
                {
                    var calculatedAverages = PropertyBucket.GetProperty<IDictionary<string, double>>(CALCULATED_AVERAGES_KEY);
                    // missing series were placed in the other category
                    if (calculatedAverages.Keys.Contains(series.Name))
                    {
                        NUnit.Framework.Assert.AreEqual(calculatedAverages[series.Name], series.Average,
                            StackTraceErrorAppender.AddMultipleLines($"Average did not match calculated expected, series: {series.Name}"));
                    }
                    else
                    {
                        // nothing to do here - other series
                    }
                }
            }
        }
    }
}
