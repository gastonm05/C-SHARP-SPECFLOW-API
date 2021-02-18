using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsClipBooks
    {
        public int ItemCount { get; set; }
        public List<NewsSingleClipBook> Items { get; set; }
        public string _links { get; set; }
        public string _meta { get; set; }
    }
}
