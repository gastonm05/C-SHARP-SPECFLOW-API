using CCC_API.Data.Responses.Analytics;
using RestSharp;

namespace CCC_API.Services.Analytics
{
    public class NewsAnalyticsService : AuthApiService
    {
        public const string ListNewsItemsUri = "news/analytics/listnewsitems";

        public enum SortDirection { Ascending, Descending };

        public enum AnalyticsField { CirculationAudience, NewsDate, OutletName, OutletId, OutletType, PublicityValue, UniqueVisitors }

        public NewsAnalyticsService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets the analytics news items for most recent 1000 news items.
        /// IMPORTANT - this is a generic method to return the most recent news items
        /// </summary>
        /// <returns></returns>
        public IRestResponse<AnalyticsNewsItems> GetAnalyticsNewsItems()
        {
            return Request().Get().ToEndPoint($"{ListNewsItemsUri}?RowCount=500").Exec<AnalyticsNewsItems>();
        }

        /// <summary>
        /// Sorts the analytics news items.
        /// </summary>
        /// <param name="key">The search key.</param>
        /// <param name="sortField">Field to sort on</param>
        /// <param name="sortDirection">The sort direction [Ascending = 0, Descending = 1]</param>
        /// <returns></returns>
        public IRestResponse<AnalyticsNewsItems> SortAnalyticsNewsItems(string key, AnalyticsField sortField, SortDirection sortDirection)
        {
            var direction = sortDirection == SortDirection.Ascending ? 0 : 1;
            return Request().Get().
                ToEndPoint($"{ListNewsItemsUri}?RowCount=500&ListNewsItemsKey={key}&SortDirection={direction}&SortField={sortField}")
                .Exec<AnalyticsNewsItems>();
        }

        /// <summary>
        /// Sorts the analytics news items without sort direction.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public IRestResponse<AnalyticsNewsItems> SortAnalyticsNewsItems(string key, AnalyticsField field)
        {
            return Request().Get().
                ToEndPoint($"{ListNewsItemsUri}?RowCount=500&ListNewsItemsKey={key}&SortField={field}")
                .Exec<AnalyticsNewsItems>();
        }

        /// <summary>
        /// Sorts the analytics news items without sort field.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<AnalyticsNewsItems> SortAnalyticsNewsItems(string key, SortDirection sortDirection)
        {
            var direction = sortDirection == SortDirection.Ascending ? 0 : 1;
            return Request().Get().
                ToEndPoint($"{ListNewsItemsUri}?RowCount=500&ListNewsItemsKey={key}&SortDirection={direction}")
                .Exec<AnalyticsNewsItems>();
        }

        /// <summary>
        /// Sorts the analytics news items. Use this when you need to add a sort direction other than Ascending/Descending
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        public IRestResponse<AnalyticsNewsItems> SortAnalyticsNewsItems(string key, AnalyticsField field, string sortDirection)
        {
            return Request().Get().
                ToEndPoint($"{ListNewsItemsUri}?RowCount=500&ListNewsItemsKey={key}&SortField={field}&SortDirection={sortDirection}")
                .Exec<AnalyticsNewsItems>();
        }

        /// <summary>
        /// Sorts the analytics news items. Use this when you need a field outside of the AnalyticsField enum.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        public IRestResponse<AnalyticsNewsItems> SortAnalyticsNewsItems(string key, string field, SortDirection sortDirection)
        {
            return Request().Get().
                ToEndPoint($"{ListNewsItemsUri}?RowCount=500&ListNewsItemsKey={key}&SortField={field}&SortDirection={sortDirection}")
                .Exec<AnalyticsNewsItems>();
        }
    }
}
