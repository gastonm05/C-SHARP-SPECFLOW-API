namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// response from endpoint api/v1/news/analytics/searches
    /// </summary>
    public class SearchesResponse
    {
        public int ItemCount { get; set; }
        public AnalyticsSearch[] Items { get; set; }
    }
}
