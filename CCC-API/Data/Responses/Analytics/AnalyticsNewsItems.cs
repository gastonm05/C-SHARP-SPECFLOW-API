using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    public class AnalyticsNewsItems
    {
        public AnalyticsNewsInfo Info { get; set; }
        public AnalyticsNewsResult Result { get; set; }
    }

    public class AnalyticsNewsInfo
    {
        public string Key { get; set; }
        public string PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

    public class AnalyticsNewsResult
    {
        public int ActiveCount { get; set; }
        public List<AnalyticsNewsItem> Items { get; set; }
    }
}
