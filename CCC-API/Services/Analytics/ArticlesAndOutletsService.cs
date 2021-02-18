using CCC_API.Data.Responses.Common;
using RestSharp;
using Newtonsoft.Json;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.Media.Outlet;

namespace CCC_API.Services.Analytics
{
    public class ArticlesAndOutletsService : AuthApiService
    {
        public static string AnalyticsArticlesEndPoint = "news/analytics/articles";
        public static string AnalyticsOutletsEndPoint = "news/analytics/outlets";
        public static string MediaOutletsUri = "media/outlets";

        public enum OutletSortField { NumberOfClips, Reach, PublicityValue }

        public enum ArticleSortField { Reach } // #TODO Prominence and Impact will be more involved due to (dynamic) custom categories

        public ArticlesAndOutletsService(string sessionKey) : base(sessionKey)
        {
        }

        /// <summary>
        /// Gets the count of available outlets.
        /// </summary>
        /// <returns>count of outlets</returns>
        public int GetTotalOutletsCount()
        {
            var resource = $"{AnalyticsOutletsEndPoint}?CreateScratchTable=true&SortField=Reach&TypeId=7"; // min request params required
            GenericResponseWithTotal response = JsonConvert.DeserializeObject<GenericResponseWithTotal>(Get<GenericResponseWithTotal>(resource).Content);
            return response.Total;
        }

        /// <summary>
        /// Gets all outlets.
        /// </summary>
        /// <returns>Content of the GET reponse</returns>
        public string GetAllOutlets()
        {
            return GetOutlets(GetTotalOutletsCount()).Content;
        }

        /// <summary>
        /// Gets the outlets.
        /// </summary>
        /// <param name="count">The number of outlets to get.</param>
        /// <returns>response as object</returns>
        private IRestResponse<object> GetOutlets(int count)
        {
            var resource = $"{AnalyticsOutletsEndPoint}?CreateScratchTable=true&RowCount={count}&RowOffset=0&SortDirection=1&SortField=Reach&TimezoneOffset=-300&TypeId=7&sort=1";
            return Get<object>(resource);
        }

        /// <summary>
        /// Gets outlets by given criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>Outlets</returns>
        public Outlets GetOutlets(string criteria)
        {
            return Request().Get()
                 .ToEndPoint($"{MediaOutletsUri}?{criteria}")
                 .ExecCheck<Outlets>();
        }

        /// <summary>
        /// Gets the count of outlets using offset and sorting by sortField
        /// </summary>
        /// <param name="count">The number of outlets to get.</param>
        /// <param name="offset">The result paging offset.</param>
        /// <param name="sortField">The field to sort outlets by.</param>
        /// <returns>count of outlets</returns>
        /// <seealso cref="OutletSortField"/>
        public int GetOutletsCount(int count, int offset, OutletSortField sortField)
        {
            var resource = $"{AnalyticsOutletsEndPoint}?CreateScratchTable=true&RowCount={count}&RowOffset={offset}&SortField={sortField.ToString()}&TypeId=7";
            AnalyticsSeriesDataResponse <AnalyticsSeries, object> response = JsonConvert.DeserializeObject<AnalyticsSeriesDataResponse<AnalyticsSeries, object>>(Get <AnalyticsSeriesDataResponse <AnalyticsSeries, object>>(resource).Content);
            return response.Data.Length;
        }

        /// <summary>
        /// Gets the count of articles using offset and sorting by sortField
        /// </summary>
        /// <param name="count">The number of articles to get.</param>
        /// <param name="offset">The result paging offset.</param>
        /// <param name="sortField">The field to sort articles by.</param>
        /// <returns>count of articles</returns>
        /// <seealso cref="ArticleSortField"/>
        public int GetArticlesCount(int count, int offset, ArticleSortField sortField)
        {
            var resource = $"{AnalyticsArticlesEndPoint}?CreateScratchTable=true&RowCount={count}&RowOffset={offset}&SortDirection=1&SortField=Headline&TypeId=7&articlesSearchBy={sortField.ToString()}";
            AnalyticsSeriesDataResponse<AnalyticsSeries, object> response = JsonConvert.DeserializeObject<AnalyticsSeriesDataResponse<AnalyticsSeries, object>>(Get<AnalyticsSeriesDataResponse<AnalyticsSeries, object>>(resource).Content);
            return response.Data.Length;
        }
    }
}
