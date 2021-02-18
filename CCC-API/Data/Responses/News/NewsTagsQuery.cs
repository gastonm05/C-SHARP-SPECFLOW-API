using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsTagsQuery
    {
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int ItemCount { get; set; }
        public List<NewsTag> Items { get; set; }
        public NewsTagsLinks _links { get; set; }
        public NewsTagsMeta _meta { get; set; }
    }
}
