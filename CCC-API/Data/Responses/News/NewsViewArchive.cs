using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsViewArchive
    {
        public string Key { get; set; }
        public int ActiveCount { get; set; }
        public int TotalCount { get; set; }
        public int ItemCount { get; set; }
        public List<NewsItemArchiveSearch> Items { get; set; }
        public NewsArchive_Links _links { get; set; }
        public NewsArchive_Meta _meta { get; set; }
    }
}
