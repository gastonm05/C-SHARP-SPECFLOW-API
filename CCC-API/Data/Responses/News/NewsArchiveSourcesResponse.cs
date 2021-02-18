using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsArchiveSourcesResponse
    {
        public int ItemCount { get; set; }
        public List<NewsArchiveSource> Items { get; set; }
    }
}
