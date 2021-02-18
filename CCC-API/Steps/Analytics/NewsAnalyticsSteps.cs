using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Analytics;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Services.Analytics.NewsAnalyticsService;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Analytics
{
    public class NewsAnalyticsSteps : AuthApiSteps
    {

        public const string ANALYTICS_NEWS_KEY = "analytics news key";

        public NewsAnalyticsSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        
        [When(@"I get a list of Analytics News items")]
        public void WhenIGetAListOfAnalyticsNewsItems()
        {
            var response = new NewsAnalyticsService(SessionKey).GetAnalyticsNewsItems();
            PropertyBucket.Remember(ANALYTICS_NEWS_KEY, response);
        }

        [When(@"I sort Analytics News items '(.*)' by '(.*)'")]
        public void WhenISortAnalyticsNewsItemsBySortDirection(SortDirection direction, AnalyticsField field)
         {
             var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
             var sortResponse = new NewsAnalyticsService(SessionKey).SortAnalyticsNewsItems(response.Data.Info.Key, field, direction);
             PropertyBucket.Remember(ANALYTICS_NEWS_KEY, sortResponse, true);
         }

        [When(@"I sort Analytics News Items '(.*)' without sort field")]
        public void WhenISortAnalyticsNewsItemsWithoutSortField(SortDirection direction)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
            var sortResponse = new NewsAnalyticsService(SessionKey).SortAnalyticsNewsItems(response.Data.Info.Key, direction);
            PropertyBucket.Remember(ANALYTICS_NEWS_KEY, sortResponse, true);
        }

        [When(@"I sort Analytics News Items by '(.*)' without sort direction")]
        public void WhenISortAnalyticsNewsItemsByNewsDateWithoutSortDirection(AnalyticsField field)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
            var sortResponse = new NewsAnalyticsService(SessionKey).SortAnalyticsNewsItems(response.Data.Info.Key, field);
            PropertyBucket.Remember(ANALYTICS_NEWS_KEY, sortResponse, true);
        }

        [When(@"I sort Analytics News items by '(.*)' with invalid direction '(.*)'")]
        public void WhenISortAnalyticsNewsItemsByWithInvalidDirection(AnalyticsField field, string direction)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
            var sortResponse = new NewsAnalyticsService(SessionKey).SortAnalyticsNewsItems(response.Data.Info.Key, field, direction);
            PropertyBucket.Remember(ANALYTICS_NEWS_KEY, sortResponse, true);
        }

        [When(@"I sort Analytics News items by '(.*)' with invalid field '(.*)'")]
        public void WhenISortAnalyticsNewsItemsByWithInvalidField(SortDirection direction, string field)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
            var sortResponse = new NewsAnalyticsService(SessionKey).SortAnalyticsNewsItems(response.Data.Info.Key, field, direction);
            PropertyBucket.Remember(ANALYTICS_NEWS_KEY, sortResponse, true);
        }

        [Then(@"all Analytics News item '(.*)' are sorted '(.*)'")]
        public void ThenAllAnalyticsNewsItemAreSorted(string field, SortDirection direction)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
            var items = response.Data.Result.Items;

            Assert.That(response.Data.Result.Items.Count, Is.GreaterThan(0), "No items returned");
            var sorted = direction == SortDirection.Ascending ? 
                items.OrderBy(s => s.GetType().GetProperty(field).GetValue(s, null)) : 
                items.OrderByDescending(s => s.GetType().GetProperty(field).GetValue(s, null));

            // Creating two lists with the dates of the news items
            List<DateTime> actualList = items.Select(x => x.NewsDate).ToList();
            List<DateTime> expectedList = sorted.Select(x => x.NewsDate).ToList();

            // Verifying whether they are sorted in the same way
            HashSet<DateTime> set = new HashSet<DateTime>(expectedList);
            bool areEquals = set.SetEquals(actualList);

            Assert.IsTrue(areEquals, "Not all items are sorted");
        }

        [Then(@"the Analytics News items endpoint response is '(.*)'")]
        public void ThenTheAnalyticsNewsItemsEndpointResponseIs(int code)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsNewsItems>>(ANALYTICS_NEWS_KEY);
            Assert.That(Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(code), "Expected status code not returned");
        }

    }
}
