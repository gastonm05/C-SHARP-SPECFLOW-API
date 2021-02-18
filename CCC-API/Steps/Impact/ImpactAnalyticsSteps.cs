using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.Impact;
using CCC_API.Data.Responses.Impact.CisionId;
using CCC_API.Services.Impact;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Services.Impact.ImpactService;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Impact
{
    class ImpactAnalyticsSteps : AuthApiSteps
    {
        public const string RESPONSE = "response";
        public const string CISION_ID_RESPONSE = "cision_id_response";

        public const int DEFAULT_LIMIT = 50;
        public const int DEFAULT_PAGE = 1;
        public const SortField DEFAULT_SORT_FIELD = SortField.date;
        public const SortDirection DEFAULT_SORT_DIRECTION = SortDirection.Descending;
        public const AllAccounts DEFAULT_ALL_ACCOUNT = AllAccounts.ignoring;
        const int NUMBER_OF_SUPPORTED_CISION_IDS = 23;

        string[] hiddenValues = { "Less than -20000", "-20000 to -2499", "-2500 to 2499", "2500 - 24999" };
        string[] hiddenHouseSizeValues = { "# Adults - 1", "# Adults - 2", "# Adults - 3+" };
        string[] hiddenEducationValues = { "Some College", "Recently Graduated", "Some Graduate School" };

        private ImpactAnalyticsServices _impactAnalyticsServices;
        private ImpactService _impactServices;

        public ImpactAnalyticsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _impactAnalyticsServices = new ImpactAnalyticsServices(SessionKey);
            _impactServices = new ImpactService(SessionKey);
        }

        [When(@"I call the engagement endpoint (.*) all accounts")]
        public void WhenICallTheEngagementEndpointAllAccounts(AllAccounts allAccounts)
        {
            EngagementData[] response = _impactAnalyticsServices.GetEngagement(allAccounts);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the webEvents endpoint (.*) all accounts")]
        public void WhenICallTheWebEventsEndpointAllAccounts(AllAccounts allAccounts)
        {
            WebEventsData[] response = _impactAnalyticsServices.GetWebEvents(allAccounts);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the views endpoint (.*) all accounts")]
        public void WhenICallTheViewsEndpointAllAccounts(AllAccounts allAccounts)
        {
            Reach[] response = _impactAnalyticsServices.GetViews(allAccounts);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the audience endpoint (.*) all accounts")]
        public void WhenICallTheAudienceEndpointAllAccounts(AllAccounts allAccounts)
        {
            Audience[] response = _impactAnalyticsServices.GetAudience(allAccounts);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the releases endpoint in order to get the Id and language code for a single release")]
        public void WhenICallTheReleasesEndpointInOrderToGetTheIdAndLanguageCodeForASingleRelease()
        {
            ReleasesImpact response = _impactServices.GetReleases(DEFAULT_LIMIT, DEFAULT_PAGE, DEFAULT_SORT_DIRECTION, DEFAULT_SORT_FIELD, DEFAULT_ALL_ACCOUNT);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the cisionId engagement endpoint (.*) all accounts and (.*) includeTimeSeries")]
        public void WhenICallTheCisionIdEngagementEndpointAllAccountsAndIncludeTimeSeries(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            EngagementCisionId responseEngagementCisionId = _impactAnalyticsServices.GetEngagementCisionId(allAccounts, timeSeries);
            PropertyBucket.Remember(CISION_ID_RESPONSE, responseEngagementCisionId);
        }

        [When(@"I call the cisionId webEvents endpoint (.*) all accounts and (.*) includeTimeSeries")]
        public void WhenICallTheCisionIdWebEventsEndpointAllAccountsAndIncludeTimeSeries(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            WebEventsCisionId responseWebEventsCisionId = _impactAnalyticsServices.GetWebEventsCisionId(allAccounts, timeSeries);
            PropertyBucket.Remember(CISION_ID_RESPONSE, responseWebEventsCisionId);
        }

        [When(@"I call the cisionId views endpoint (.*) all accounts and (.*) includeTimeSeries")]
        public void WhenICallTheCisionIdViewsEndpointAllAccountsAndIncludeTimeSeries(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            ViewCisionId responseViewsCisionId = _impactAnalyticsServices.GetViewsCisionId(allAccounts, timeSeries);
            PropertyBucket.Remember(CISION_ID_RESPONSE, responseViewsCisionId);
        }

        [When(@"I call the cisionId audience endpoint (.*) all accounts and (.*) includeTimeSeries")]
        public void WhenICallTheCisionIdAudienceEndpointAllAccountsAndIncludeTimeSeries(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            AudienceCisionId responseAudienceCisionId = _impactAnalyticsServices.GetAudienceCisionId(allAccounts, timeSeries);
            PropertyBucket.Remember(CISION_ID_RESPONSE, responseAudienceCisionId);
        }

        [Then(@"the Impact Views endpoint has the correct response")]
        public void ThenTheImpactViewsEndpointHasTheCorrectResponse()
        {
            Reach[] response = PropertyBucket.GetProperty<Reach[]>(RESPONSE);

            if (response.Count() == 0) Assert.Ignore("There are not reach to verify");

            //Using the first set of parameters for this request always will return just one element in the response
            // we will remove FirstOrDefault() later at adding more parameters
            Assert.That(response.FirstOrDefault().DataSetId, Is.GreaterThan(0), "DataSetId is not correct");
            Assert.That(response.FirstOrDefault().Series.ShowTotalsInLegend, Is.True, "ShowTotalsInLegend is not correct");
            Assert.That(response.FirstOrDefault().Series.Series.FirstOrDefault().Total, Is.GreaterThan(0), "The total is not displayed");
            Assert.That(response.FirstOrDefault().Series.Series.FirstOrDefault().Name, Is.EqualTo("Total Views"), "The name is not correct");
            Assert.That(response.FirstOrDefault().Series.Series.FirstOrDefault().Id, Is.EqualTo("total views"), "The id is not correct");
        }

        [Then(@"the Impact Engagement endpoint has the correct response")]
        public void ThenTheImpactEngagementEndpointHasTheCorrectResponse()
        {
            EngagementData[] response = PropertyBucket.GetProperty<EngagementData[]>(RESPONSE);

            if (response.Count() == 0) Assert.Ignore("There is not Engagement data to verify");

            Assert.That(response.All(i => !string.IsNullOrEmpty(i.Name)), "Not all Names are valid");
            Assert.That(response.All(i => i.Value >= 0), "Not all Values are valid");
        }

        [Then(@"the Impact web events endpoint has the correct response")]
        public void ThenTheImpactWebEventsEndpointHasTheCorrectResponse()
        {
            WebEventsData[] response = PropertyBucket.GetProperty<WebEventsData[]>(RESPONSE);

            if (response.Count() == 0) Assert.Ignore("There is not WebEvent data to verify");

            Assert.That(response.All(i => !string.IsNullOrEmpty(i.Name)), "Not all Names are valid");
            Assert.That(response.All(i => i.Value >= 0), "Not all Values are valid");
        }

        [Then(@"the Impact audience endpoint has the correct response")]
        public void ThenTheImpactAudienceEndpointHasTheCorrectResponse()
        {
            Audience[] response = PropertyBucket.GetProperty<Audience[]>(RESPONSE);

            if (response.Count() == 0) Assert.Ignore("There are not Audience data to verify");

            Assert.That(response.Count() > 0, "There are not elements in the response");


            foreach (Audience audience in response)
            {
                Assert.That(audience.DataSetId, Is.GreaterThan(0), "The DataSetId is not correct");
                Assert.IsTrue(audience.Series.ShowTotalsInLegend, "The ShowTotalsInLegend is not correct");

                if (!(audience.Series.Type == null))
                {
                    Assert.That(audience.Series.Type, !Is.Empty, "The Type is not correct");
                }

                foreach (AnalyticsSeries series in audience.Series.Series)
                {
                    Assert.That(series.Data.Count, Is.GreaterThan(0), "There are not data in the response");
                    Assert.That(series.Data.ToList().Count, Is.GreaterThan(0), "There are not series in the response's data");
                }
            }
        }

        [Then(@"the Impact Views endpoint has the correct response for a single release")]
        public void ThenTheImpactViewsEndpointHasTheCorrectResponseForASingleRelease()
        {
            ReleasesImpact response = PropertyBucket.GetProperty<ReleasesImpact>(RESPONSE);

            if (response.Releases.Count() == 0) Assert.Ignore("There are not releases to verify");
            int count = 0;

            foreach (var releases in response.Releases)
            {
                Reach[] responseReach = _impactAnalyticsServices.GetViews(AllAccounts.including, DateTime.Now,
                    releases.LanguageCode, releases.Id, releases.Date);

                if (responseReach.Count() <= 0)
                {
                    count++;
                    continue;
                }
                else
                {
                    foreach (var reach in responseReach)
                    {
                        Assert.That(reach.DataSetId, Is.GreaterThan(0), "DataSetId is not correct");
                        Assert.That(reach.Series.ShowTotalsInLegend, Is.True, "ShowTotalsInLegend is not correct");
                        AnalyticsSeries[] series = reach.Series.Series;

                        for (int x = 0; x < series.Count(); x++)
                        {
                            Assert.That(series[x].Total >= 0, "The total is not correct");
                            Assert.That(series[x].Name, Is.EqualTo(GetIdNameForReachEndpoint(x)), "The Name is not correct");
                            Assert.That(series[x].Id, Is.EqualTo(GetIdNameForReachEndpoint(x).ToLower()), "The Id is not correct");
                        }
                    }
                }
            }
            if (count == response.Releases.Count()) Assert.Ignore("There is not Reach data to verify");
        }

        /**
         * Get the name of the series based on its position in the response
         */
        private string GetIdNameForReachEndpoint(int id)
        {
            switch (id)
            {
                case 0:
                    return "Total Views";
                case 1:
                    return "Unique Visitors";
                case 2:
                    return "Repeat Visitors";
                default:
                    return "no data";
            }
        }

        [Then(@"the Impact Engagement endpoint has the correct response for a single release")]
        public void ThenTheImpactEngagementEndpointHasTheCorrectResponseForASingleRelease()
        {
            ReleasesImpact response = PropertyBucket.GetProperty<ReleasesImpact>(RESPONSE);

            if (response.Releases.Count() == 0) Assert.Ignore("There are not releases to verify");
            int count = 0;

            foreach (var release in response.Releases)
            {
                EngagementData[] responseEngagement = _impactAnalyticsServices.GetEngagement(AllAccounts.including, DateTime.Now, release.LanguageCode, release.Id, release.Date);

                if (responseEngagement.Count() <= 0)
                {
                    count++;
                    continue;
                } else
                {
                    Assert.That(responseEngagement.All(x => !string.IsNullOrEmpty(x.Name)), "Not all Names are valid");
                    Assert.That(responseEngagement.All(x => x.Value >= 0), "Not all Values are valid");
                }
            }
            if (count == response.Releases.Count()) Assert.Ignore("There is not Enaggement data to verify");
        }

        [Then(@"the Impact web events endpoint has the correct response for a single release")]
        public void ThenTheImpactWebEventsEndpointHasTheCorrectResponseForASingleRelease()
        {
            ReleasesImpact response = PropertyBucket.GetProperty<ReleasesImpact>(RESPONSE);

            if (response.Releases.Count() == 0) Assert.Ignore("There are not releases to verify");
            int count = 0;

            foreach (var release in response.Releases)
            {
                WebEventsData[] responseWebEvents = _impactAnalyticsServices.GetWebEvents(AllAccounts.including, DateTime.Now, release.LanguageCode, release.Id, release.Date);

                if (responseWebEvents.Count() <= 0)
                {
                    count++;
                    continue;
                }
                else
                {
                    Assert.That(responseWebEvents.All(x => !string.IsNullOrEmpty(x.Name)), "Not all Names are valid");
                    Assert.That(responseWebEvents.All(x => x.Value >= 0), "Not all Values are valid");
                }
            }
            if (count == response.Releases.Count()) Assert.Ignore("There is not WebEvents data to verify");
        }

        [Then(@"the Impact audience endpoint has the correct response for a single release")]
        public void ThenTheImpactAudienceEndpointHasTheCorrectResponseForASingleRelease()
        {
            ReleasesImpact response = PropertyBucket.GetProperty<ReleasesImpact>(RESPONSE);

            if (response.Releases.Count() == 0) Assert.Ignore("There are not releases to verify");
            int count = 0;

            foreach (var release in response.Releases)
            {
                Audience[] responseAudience = _impactAnalyticsServices.GetAudience(AllAccounts.including, DateTime.Now, release.LanguageCode, release.Id, release.Date);

                if(responseAudience.Count() <= 0)
                {
                    count++;
                    continue;
                }
                else
                {
                    Assert.That(responseAudience.Count(), Is.GreaterThan(0), "There are not elements in the response");
                    Assert.That(responseAudience.All(x => x.DataSetId > 0), "DataSetId is not correct");
                    Assert.True(responseAudience.All(x => x.Series.ShowTotalsInLegend), "ShowTotalsInLegend is not correct");
                    Assert.True(responseAudience.All(x => x.Series.Series.All(y => y.Total >= 0)), "The total is not correct");
                }
            }
            if (count == response.Releases.Count()) Assert.Ignore("There is not Audience data to verify");
        }

        [Then(@"both retrieved data for engagement match")]
        public void ThenBothRetrievedDataForEngagementMatch()
        {
            EngagementCisionId responseCisionID = PropertyBucket.GetProperty<EngagementCisionId>(CISION_ID_RESPONSE);
            EngagementData[] responseEngament = PropertyBucket.GetProperty<EngagementData[]>(RESPONSE);

            MediaTypeData[] mediaTypeDataList = responseCisionID.MediaTypeData;

            if (mediaTypeDataList != null)
            {
                for (int i = 0; i < mediaTypeDataList.Count(); i++)
                {

                    string mediaTypeCisionId = mediaTypeDataList[i].MediaType.ToLower();
                    string mediaTypeImpact = (mediaTypeDataList[i].MediaType.Equals("clicks")) ? "clicks-through" : mediaTypeDataList[i].MediaType.ToLower();
                    Assert.That(mediaTypeImpact.StartsWith(mediaTypeCisionId), "The media type data is not the samee");
                    Assert.That(mediaTypeDataList[i].ImpressionCount == (int)responseEngament[i].Value, "The value data is not the same");
                }
            } else
            {
                Assert.That(responseEngament.Count() == 0, "The server did not retrieve any data properly");
            }
        }

        [Then(@"both retrieved data for webEvents match")]
        public void ThenBothRetrievedDataForWebEventsMatch()
        {
            WebEventsCisionId responseCisionID = PropertyBucket.GetProperty<WebEventsCisionId>(CISION_ID_RESPONSE);
            WebEventsData[] responseWebEvents = PropertyBucket.GetProperty<WebEventsData[]>(RESPONSE);

            EventData[] eventDataList = responseCisionID.EventData;

            if (eventDataList != null)
            {
                for (int i = 0; i < eventDataList.Count(); i++)
                {
                    string eventDateCisionId = eventDataList[i].EventName;
                    string eventDateImpact = responseWebEvents[i].Name.ToLower();
                    Assert.That((eventDateCisionId.ToLower()).Contains(eventDateImpact), "The event data name is not the same");
                    Assert.That(eventDataList[i].EventCount == responseWebEvents[i].Value, "The value data is not the same");
                }
            } else
            {
                Assert.That(responseWebEvents.Count() == 0, "The server did not retrieve any data properly");
            }
        }

        [Then(@"both retrieved data for views match")]
        public void ThenBothRetrievedDataForViewsMatch()
        {
            ViewCisionId responseCisionId = PropertyBucket.GetProperty<ViewCisionId>(CISION_ID_RESPONSE);
            Reach[] responseReach = PropertyBucket.GetProperty<Reach[]>(RESPONSE);

            TimeSeriesData[] reachDataListCisionId = responseCisionId.TimeSeriesData;

            float sum = 0;

            if (reachDataListCisionId != null)
            {
                for (int i = 0; i < responseReach.Count(); i++)
                {
                    AnalyticsSeries[] reachSeries = responseReach[i].Series.Series;

                    List<Tuple<float, float>> data = reachSeries[i].GetData();

                    long reachTotal = reachSeries[i].Total;
                    float sumData = reachSeries[i].GetDataSum();
                    Assert.That(reachTotal == sumData, "The totals are not the same");

                    for (int j = 0; j < data.Count(); j++)
                    {
                        Tuple<float, float> dataTuple = data[j];
                        Assert.That(dataTuple.Item2 == reachDataListCisionId[j].TotalViews,
                            "The totals are not the same");
                    }

                    sum = sum + reachTotal;
                }
                Assert.That(responseCisionId.TotalViews == sum, "The Total views are not the same. Cision Id total views are: " + responseCisionId.TotalViews + ", while Reach total views are: " + sum);
            } else
            {
                Assert.That(responseReach.Count() == 0, "The server did not retrieve any data properly");
            }
        }

        [Then(@"both retrieved data for audience match")]
        public void ThenBothRetrievedDataForAudienceMatch()
        {
            AudienceCisionId responseCisionId = PropertyBucket.GetProperty<AudienceCisionId>(CISION_ID_RESPONSE);
            Audience[] responseAudience = PropertyBucket.GetProperty<Audience[]>(RESPONSE);

            CharacteristicsData[] characteristicData = responseCisionId.CharacteristicsData;

            if (characteristicData != null)
            {
                Assert.That(responseAudience.Count() == characteristicData.Count(), "The data is not the same");

                for (int i = 0; i < characteristicData.Count(); i++)
                {
                    SubCategory[] subCategories = characteristicData[i].Subcategories;
                    AnalyticsSeries[] seriesForCategories = null;

                    if (characteristicData[i].TaxonomyId == 116)
                    {
                        VerifyAudienceForWealthDecile(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else if (characteristicData[i].TaxonomyId == 144)
                    {
                        VerifyAudienceForAgeOfChildren(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else if (characteristicData[i].TaxonomyId == 134)
                    {
                        VerifyAudienceForHouseholdSize(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else if (characteristicData[i].TaxonomyId == 158)
                    {
                        VerifyAudienceForMediaHomeValue(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else if (characteristicData[i].TaxonomyId == 98)
                    {
                        VerifyAudienceForEstimatedNetWorth(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else if (characteristicData[i].TaxonomyId == 29)
                    {
                        VerifyAudienceForIncome(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else if (characteristicData[i].TaxonomyId == 18)
                    {
                        VerifyAudienceForEducation(characteristicData, responseAudience, subCategories, seriesForCategories, i);
                    }
                    else
                    {
                        foreach (SubCategory subCategory in subCategories)
                        {
                            seriesForCategories = responseAudience[i].Series.Series;

                            string[] categories = new string[seriesForCategories.Length];

                            for (int c = 0; c < seriesForCategories.Length; c++)
                            {
                                categories[c] = seriesForCategories[c].Name;
                            }

                            string label = subCategory.Label;
                            //this is a temporary fix until cisionId team change the endpoint response for age label from 55-59 to 55-64
                            if (label.Equals("55-59"))
                            {
                                label = "55-64";
                            }
                            //Validating labels and categories
                            Assert.That(categories.Any(label.Contains), "Data does not match for " + subCategory.Label);


                            //Validating percentages and totals
                            int indexOfCategory = categories.ToList().IndexOf(label.Replace("**", ""));
                            Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same" +
                                "Cision ID percentage was: " + (Math.Round(subCategory.Percentage)) + ", but Audience total was: " + seriesForCategories[indexOfCategory].Total);
                        }
                    }
                }
            } else
            {
                Assert.That(responseAudience.Count() == 0, "The server did not retrieve any data properly");
            }
        }

        public string GetRangeForAudienceSubCategory(string label)
        {
            string[] range = label.Split(new[] { "-" }, StringSplitOptions.None);

            string firstPartOfRange = (range[0].Contains("$")) ? range[0].Replace("$", "").Replace(",", "") : range[0];
            string secondPartOfRange = (range[1].Contains("$")) ? range[1].Replace("$", "").Replace(",", "") : range[1];

            long lowerLimit = Int64.Parse(firstPartOfRange.Trim());
            long upperLimit = Int64.Parse(secondPartOfRange.Trim());

            long newLowerLimit = (lowerLimit == 1) ? 0 : (lowerLimit / 1000);
            long newUpperLimit = ((upperLimit + 1) > 999999) ? (upperLimit + 1) / 1000000 : (upperLimit + 1) / 1000;

            string metricSymbolForUpperLimit = ((upperLimit + 1) > 999999) ? "M" : "K";
            string metricSymbolForLowerLimit = (metricSymbolForUpperLimit.Equals("M")) ? "K" : "";

            return "$" + newLowerLimit + metricSymbolForLowerLimit + "-" + newUpperLimit + metricSymbolForUpperLimit;
        }

        public void VerifyAudienceForWealthDecile(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 58 || audience.DataSetId == 83) seriesForCategories = audience.Series.Series;
            }

            Assert.That(subCategories.Count() == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            string[] categories = new string[seriesForCategories.Length];

            for (int c = 0; c < seriesForCategories.Length; c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;

                if (label.Equals("Lowest"))
                {
                    //Validating labels and categories
                    Assert.That(categories.Any("Poorest".Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf("Poorest");
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
                else if (label.Equals("Top"))
                {
                    //Validating labels and categories
                    Assert.That(categories.Any("Richest".Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf("Richest");
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
                else
                {
                    //Validating labels and categories
                    Assert.That(categories.Any(label.Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(label);
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
            }
        }

        public void VerifyAudienceForAgeOfChildren(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 60 || audience.DataSetId == 85) seriesForCategories = audience.Series.Series;
            }

            Assert.That(subCategories.Count() == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            string[] categories = new string[seriesForCategories.Length];

            for (int c = 0; c < seriesForCategories.Length; c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;
                //Validating labels and categories
                Assert.That(categories.Any(label.Contains), "Data does not match for " + subCategory.Label);

                //Validating percentages and totals
                int indexOfCategory = categories.ToList().IndexOf(label);
                Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
            }
        }

        public void VerifyAudienceForHouseholdSize(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 59 || audience.DataSetId == 84) seriesForCategories = audience.Series.Series;
            }

            Assert.That((subCategories.Count() - hiddenHouseSizeValues.Count()) == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            string[] categories = new string[seriesForCategories.Length];

            for (int c = 0; c < seriesForCategories.Length; c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            //Validating labels and categories
            double totalOfPercentages = 0;
            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;
                if (!hiddenHouseSizeValues.Any(label.Contains))
                {
                    totalOfPercentages += subCategory.Percentage;
                    Assert.That(categories.Any(label.Contains), "Data does not match for " + subCategory.Label);
                }
            }

            //Validating percentages and totals
            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;
                if (!hiddenHouseSizeValues.Any(label.Contains))
                {
                    int indexOfCategory = categories.ToList().IndexOf(label);
                    double percentage = (subCategory.Percentage * 100) / totalOfPercentages;

                    bool condOne = (Math.Round(percentage)) <= (seriesForCategories[indexOfCategory].Total + 1);
                    bool condTwo = (Math.Round(percentage)) >= (seriesForCategories[indexOfCategory].Total - 1);

                    //Assert.That((Math.Round(percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                    Assert.That(condOne && condTwo, "The percentage are not the same" +
                                "Cision ID percentage was: " + (Math.Round(percentage)) + ", but Audience total was: " + seriesForCategories[indexOfCategory].Total);
                }
            }
        }

        public void VerifyAudienceForMediaHomeValue(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 63 || audience.DataSetId == 88) seriesForCategories = audience.Series.Series;
            }

            Assert.That(subCategories.Count() == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            string[] categories = new string[seriesForCategories.Length];

            for (int c = 0; c < seriesForCategories.Length; c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;

                if (label.Contains("+"))
                {
                    //Validating labels and categories
                    Assert.That(categories.Any("$1M+".Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf("$1M+");
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");

                }
                else
                {
                    //Validating labels and categories
                    Assert.That(categories.Any(GetRangeForAudienceSubCategory(subCategory.Label).Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(GetRangeForAudienceSubCategory(subCategory.Label));
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
            }
        }

        public void VerifyAudienceForEstimatedNetWorth(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 56 || audience.DataSetId == 81) seriesForCategories = audience.Series.Series;
            }

            Assert.That(subCategories.Count() == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            string[] categories = new string[seriesForCategories.Length];

            for (int c = 0; c < seriesForCategories.Length; c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;

                if (hiddenValues.Any(label.Contains)) continue;

                if (label.Contains("Less") && label.Contains("$"))
                {
                    //Validating labels and categories
                    string[] lessArray = subCategory.Label.Split(new[] { "than" }, StringSplitOptions.None);
                    string lessValue = "< " + lessArray[1].Trim();
                    Assert.That(categories.Any(lessValue.Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(lessValue);
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");

                }
                else if (label.Contains("-") && label.Contains("$") && label.Contains(","))
                {
                    //Validating labels and categories
                    string range = GetRangeForAudienceSubCategory(label);
                    Assert.That(categories.Any(range.Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(range);
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
                else if (label.Contains("-") && !label.Contains("$") && !label.Contains("Less"))
                {
                    //Validating labels and categories
                    string range = GetRangeForAudienceSubCategory(label);
                    Assert.That(categories.Any(range.Contains), range + "did not work");

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(range);
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
                else if (label.Contains("+"))
                {
                    //Validating labels and categories
                    Assert.That(categories.Any("$1M+".Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf("$1M+");
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
            }
        }

        public void VerifyAudienceForIncome(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 43 || audience.DataSetId == 72) seriesForCategories = audience.Series.Series;
            }

            Assert.That(subCategories.Count() == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            string[] categories = new string[seriesForCategories.Length];

            for (int c = 0; c < seriesForCategories.Length; c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;

                if (label.Contains("Greater"))
                {
                    //Validating labels and categories
                    string[] greaterArray = subCategory.Label.Split(new[] { "than" }, StringSplitOptions.None);
                    long value = Int32.Parse(greaterArray[1]) / 1000;
                    string greaterValue = ">$" + value + "K";

                    Assert.That(categories.Any(greaterValue.Contains), "Data does not match for " + subCategory.Label);

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(greaterValue);
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
                else if (label.Contains("-"))
                {
                    //Validating labels and categories
                    string range = GetRangeForAudienceSubCategory(label);
                    Assert.That(categories.Any(range.Contains), range + "did not work");

                    //Validating percentages and totals
                    int indexOfCategory = categories.ToList().IndexOf(range);
                    Assert.That((Math.Round(subCategory.Percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                }
            }
        }

        public void VerifyAudienceForEducation(CharacteristicsData[] characteristicData, Audience[] responseAudience, SubCategory[] subCategories, AnalyticsSeries[] seriesForCategories, int i)
        {
            foreach (Audience audience in responseAudience)
            {
                if (audience.DataSetId == 42 || audience.DataSetId == 71) seriesForCategories = audience.Series.Series;
            }

            string[] categories = new string[seriesForCategories.Count()];

            int hiddenValuesCount = 0;

            for (int c = 0; c < subCategories.Count(); c++)
            {
                if (hiddenEducationValues.Any(subCategories[c].Label.Contains)) hiddenValuesCount++;
            }

            for (int c = 0; c < seriesForCategories.Count(); c++)
            {
                categories[c] = seriesForCategories[c].Name;
            }

            Assert.That((subCategories.Count() - hiddenValuesCount) == seriesForCategories.Count(), "The data is not the same."
                + "Cision ID has " + subCategories.Count() + " categories but Analytics has " + seriesForCategories.Count()
                + ", for characteristic data " + characteristicData[i].Label);

            //Validating labels and categories
            double totalOfPercentages = 0;
            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;
                if (!hiddenEducationValues.Any(label.Contains))
                {
                    totalOfPercentages += subCategory.Percentage;
                    Assert.That(categories.Any(label.Contains), "Data does not match for " + subCategory.Label);
                }
            }

            //Validating percentages and totals
            foreach (SubCategory subCategory in subCategories)
            {
                string label = subCategory.Label;
                if (!hiddenEducationValues.Any(label.Contains))
                {
                    int indexOfCategory = categories.ToList().IndexOf(label);
                    double percentage = (subCategory.Percentage * 100) / totalOfPercentages;

                    bool condOne = (Math.Round(percentage)) <= (seriesForCategories[indexOfCategory].Total + 1);
                    bool condTwo = (Math.Round(percentage)) >= (seriesForCategories[indexOfCategory].Total - 1);

                    //Assert.That((Math.Round(percentage)) == (seriesForCategories[indexOfCategory].Total), "The percentage are not the same");
                    Assert.That(condOne && condTwo, "The percentage are not the same" +
                                "Cision ID percentage was: " + (Math.Round(percentage)) + ", but Audience total was: " + seriesForCategories[indexOfCategory].Total);
                }
            }
        }
    }
}
