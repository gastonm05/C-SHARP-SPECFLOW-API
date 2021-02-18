using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsView
    {
        public string Key { get; set; }
        public int ActiveCount { get; set; }
        public Export Export { get; set; }
        public int TotalCount { get; set; }
        public int ItemCount { get; set; }
        public List<NewsItem> Items { get; set; }
        public Links links { get; set; }
    }
}
