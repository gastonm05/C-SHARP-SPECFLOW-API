namespace CCC_API.Data.PostData.Analytics
{
    /// <summary>
    /// body for post data of endpoint news/analytics/searches/group/
    /// </summary>
    public class AnalyticsSearchGroupPostBody
    {
            public string Name { get; set; }
            public int CategoryId { get; set; }
    }
}
