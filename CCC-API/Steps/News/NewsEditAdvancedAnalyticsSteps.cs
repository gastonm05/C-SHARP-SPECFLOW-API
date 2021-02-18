using BoDi;
using CCC_API.Data;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Analytics;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using System.Linq;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.News
{
    [Binding]
    public class NewsEditAdvancedAnalyticsSteps : AuthApiSteps
    {
        private string PATCH_SINGLE_NEWS_RESPONSE = "patch singe news item";
        private NewsViewService _newsViewService;
        
        public NewsEditAdvancedAnalyticsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _newsViewService = new NewsViewService(SessionKey);
        }

        [When(@"I perform a PATCH for news item to (Add|Remove) company searches:")]
        public void WhenIPerformApatchToUpdateCompanySearches(string option, Table table)
        {
            var searches = PropertyBucket.GetProperty<AnalyticsSearch[]>(AnalyticsSearchesEndPointSteps.GET_SEARCHES_KEY);
            var updates = table.Rows.ToList().Select(r =>
            {
                var toneId = (int)r["tone"].ParseEnum<Services.Analytics.Common.ToneId>();
                var update = $"{{\"tone\":{toneId},\"impact\":{r["impact"]},\"prominence\":{r["prominence"]}}}";

                var searchNameFromTable = r["search"];
                var searchId = searches
                    .Where(s => s.SearchName == searchNameFromTable)
                    .FirstOrError("No search found: " + searchNameFromTable).SearchId;

                var data = new PatchData
                {
                    Op = option,
                    Path = $"analyticssearches/{searchId}",
                    Value = option == "Remove" ? null : update
                };

                return data;
            }).ToArray();

            var item = PropertyBucket.GetProperty<NewsItem>(NewsViewService.NewsViewEndPoint);
            var resp = _newsViewService.PatchNewsItem(item.Id, updates).CheckCodeGetData();
            PropertyBucket.Remember(PATCH_SINGLE_NEWS_RESPONSE, resp, true);
        }

        [When(@"I perform a PATCH for news item to (Add|Remove) product searches: '(.*)'")]
        public void WhenIPerformApatchForNewsItemToUpdateProductSearches(string operation, string products)
        {
            var searches = PropertyBucket.GetProperty<AnalyticsSearch[]>(AnalyticsSearchesEndPointSteps.GET_SEARCHES_KEY);
            var prods = products.Split(',').Select(_ => _.Trim()).ToList();
            var updates = prods.Select(p =>
            {
                var searchId = searches
                    .Where(s => s.SearchName == p)
                    .FirstOrError("No search found: " + p).SearchId;
                var data = new PatchData
                {
                    Op   = operation,
                    Path = $"analyticssearches/{searchId}"
                };
                return data;
            }).ToArray();

            var item = PropertyBucket.GetProperty<NewsItem>(NewsViewService.NewsViewEndPoint);
            var resp = _newsViewService.PatchNewsItem(item.Id, updates).CheckCodeGetData();
            PropertyBucket.Remember(PATCH_SINGLE_NEWS_RESPONSE, resp, true);
        }
    }
}
