using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.Impact;
using CCC_API.Data.Responses.Impact.CisionId;
using CCC_API.Data.Responses.Impact.CisionIDEarned;
using CCC_API.Data.Responses.Impact.Earned;
using CCC_API.Services.Impact;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Data.Responses.Impact.CisionIDEarned.TopOutletsCID;
using static CCC_API.Data.Responses.Impact.Earned.TopOutletEarned;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Impact.EarnedMedia
{
    class EarnedAnalyticsSteps : AuthApiSteps
    {
        public const string RESPONSE = "response";
        public const string RESPONSE_VIEWS = "responseViews";
        public const string RESPONSE_WEBEVENTS = "responseWebEvents";
        public const string RESPONSE_AUDIENCE = "responseAudience";
        public const string RESPONSE_TOP_OUTLET = "responseTopOutlet";


        private EarnedAnalyticsSevices _earnedAnalyticsServices;
        private EarnedMediaServices _earnedMediaServices;
        private ImpactAnalyticsSteps _impactAnalyticsSteps;

        public EarnedAnalyticsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _earnedAnalyticsServices = new EarnedAnalyticsSevices(SessionKey);
            _earnedMediaServices = new EarnedMediaServices(SessionKey);
            _impactAnalyticsSteps = new ImpactAnalyticsSteps(objectContainer);
        }

        [Given(@"I call the search endpoint in order to get the search id")]
        public void WhenICallTheSearchEndpointInOrderToGetTheSearchId()
        {
            SearchEarned response = _earnedMediaServices.GetSearches();
            if (response.Searches.Count() == 0) Assert.Ignore("There are not searches to verify");
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the earned views endpoint on the last (.*) days")]
        public void WhenICallTheEarnedViewsEndpointOnTheLastDays(int days)
        {
            SearchEarned response = PropertyBucket.GetProperty<SearchEarned>(RESPONSE);
            List<ViewsEarned[]> viewsList = new List<ViewsEarned[]>();
            foreach (var search in response.Searches)
            {
                ViewsEarned[] responseViews = _earnedAnalyticsServices.GetViews(days, search.Id);
                viewsList.Add(responseViews);
            }
            PropertyBucket.Remember(RESPONSE_VIEWS, viewsList);
        }

        [When(@"I call the earned WebEvents endpoint on the last (.*) days")]
        public void WhenICallTheEarnedWebEventsEndpointOnTheLastDays(int days)
        {
            SearchEarned response = PropertyBucket.GetProperty<SearchEarned>(RESPONSE);
            List<WebEventsEarned[]> webEventsList = new List<WebEventsEarned[]>();
            foreach (var search in response.Searches)
            {
                WebEventsEarned[] responseWebEvents = _earnedAnalyticsServices.GetWebEvents(days, search.Id);
                webEventsList.Add(responseWebEvents);
            }
            PropertyBucket.Remember(RESPONSE_WEBEVENTS, webEventsList);
        }

        [When(@"I call the earned Audience endpoint on the last (.*) days")]
        public void WhenICallTheEarnedAudienceEndpointOnTheLastDays(int days)
        {
            SearchEarned response = PropertyBucket.GetProperty<SearchEarned>(RESPONSE);
            List<AudienceEarned[]> audienceList = new List<AudienceEarned[]>();
            foreach (var search in response.Searches)
            {
                AudienceEarned[] responseAudience = _earnedAnalyticsServices.GetAudience(days, search.Id);
                audienceList.Add(responseAudience);
            }
            PropertyBucket.Remember(RESPONSE_AUDIENCE, audienceList);
        }

        [When(@"I call the earned TopOutlet endpoint on the last (.*) days")]
        public void WhenICallTheEarnedTopOutletEndpointOnTheLastDays(int days)
        {
            SearchEarned response = PropertyBucket.GetProperty<SearchEarned>(RESPONSE);
            List<TopOutletEarned> topOutletsList = new List<TopOutletEarned>();
            foreach (var search in response.Searches)
            {
                TopOutletEarned responseTopOutlet = _earnedAnalyticsServices.GetTopOutlets(days, search.Id);
                topOutletsList.Add(responseTopOutlet);
            }
            PropertyBucket.Remember(RESPONSE_TOP_OUTLET, topOutletsList);
        }

        [Then(@"the earned views endpoint has the correct response")]
        public void ThenTheEarnedViewsEndpointHasTheCorrectResponse()
        {
            List<ViewsEarned[]> viewsList = PropertyBucket.GetProperty<List<ViewsEarned[]>>(RESPONSE_VIEWS);
            int count = 0;
            foreach (ViewsEarned[] views in viewsList)
            {
                if (views.Count() <= 0)
                {
                    count++;
                    continue;
                }
                else
                {
                    foreach (var view in views)
                    {
                        Assert.That(view.DataSetId, Is.GreaterThan(0), "DataSetId is not correct");
                        Assert.That(view.Series.ShowTotalsInLegend, Is.True, "ShowTotalsInLegend is not correct");

                        AnalyticsSeries[] series = view.Series.Series;

                        for (int x = 0; x < series.Count(); x++)
                        {
                            Assert.That(series[x].Total >= 0, "The total is not correct");
                            Assert.That(series[x].Name, Is.EqualTo("Total Views"), "The Name is not correct");
                            Assert.That(series[x].Id, Is.EqualTo("total views"), "The Id is not correct");
                        }
                    }
                }
            }
            if (count == viewsList.Count()) Assert.Ignore("There are not views to verify");
        }

        [Then(@"the earned WebEvents endpoint has the correct response")]
        public void ThenTheEarnedWebEventsEndpointHasTheCorrectResponse()
        {
            List<WebEventsEarned[]> responseWebEvents = PropertyBucket.GetProperty<List<WebEventsEarned[]>>(RESPONSE_WEBEVENTS);

            if (responseWebEvents.Count() == 0) Assert.Ignore("There is not WebEvent data to verify");

            foreach (var webEvents in responseWebEvents)
            {
                Assert.That(webEvents.All(i => !string.IsNullOrEmpty(i.Name)), "Not all Names are valid");
                Assert.That(webEvents.All(i => i.Value >= 0), "Not all Values are valid");
            }
        }

        [Then(@"the earned Audience endpoint has the correct response")]
        public void ThenTheEarnedAudienceEndpointHasTheCorrectResponse()
        {
            List<AudienceEarned[]> responseAudience = PropertyBucket.GetProperty<List<AudienceEarned[]>>(RESPONSE_AUDIENCE);

            if (responseAudience.Count() == 0) Assert.Ignore("There is not Audiencet data to verify");
            int count = 0;
            foreach (AudienceEarned[] audiences in responseAudience)
            {
                if (audiences.Count() <= 0)
                {
                    count++;
                    continue;
                }
                else
                {
                    Assert.That(audiences.Count(), Is.GreaterThan(0), "There are not elements in the response");
                    Assert.That(audiences.All(x => x.DataSetId > 0), "DataSetId is not correct");
                    Assert.True(audiences.All(x => x.Series.ShowTotalsInLegend), "ShowTotalsInLegend is not correct");
                    Assert.True(audiences.All(x => x.Series.Series.All(y => y.Total >= 0)), "The total is not correct");
                    Assert.That(audiences.All(x => x.Series.Series.All(y => !string.IsNullOrEmpty(y.Name))), "The total is not correct");
                }
            }
            if (count == responseAudience.Count()) Assert.Ignore("There is not Audience data to verify");
        }

        [Then(@"the earned TopOutlet endpoint has the correct response")]
        public void ThenTheEarnedTopOutletEndpointHasTheCorrectResponse()
        {
            List<TopOutletEarned> responseTopOutlet = PropertyBucket.GetProperty<List<TopOutletEarned>>(RESPONSE_TOP_OUTLET);

            if (responseTopOutlet.Count() == 0) Assert.Ignore("There is not Audiencet data to verify");
            var count = 0;
            foreach (var topOutlet in responseTopOutlet)
            {
                if (topOutlet.Total == 0)
                {
                    count++;
                    continue;
                }

                DataList[] dataList = topOutlet.Data;

                if (dataList.Count() == 0) Assert.Ignore("There is not Outlets to verify");

                for (int x = 0; x < dataList.Count(); x++)
                {
                    Assert.That(dataList[x].Name, !Is.Empty, "The name is not correct");
                    Assert.That(dataList[x].SearchId, !Is.Empty, "There searchId is not correct");
                    Assert.That(dataList[x].Url, !Is.Empty, "The url is not correct");
                }
            }

            if (count == responseTopOutlet.Count()) Assert.Ignore("All of the top outlets are empty. Cannot perfrom verification.");
        }

        [Then(@"the earned searches endpoint has the correct response")]
        public void ThenTheEarnedSearchesEndpointHasTheCorrectResponse()
        {
            List<SearchEarned[]> responseSearches = PropertyBucket.GetProperty<List<SearchEarned[]>>(RESPONSE);

        }
    }
}