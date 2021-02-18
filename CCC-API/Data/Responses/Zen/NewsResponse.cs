using System.Collections.Generic;

namespace CCC_API.Data.Responses.Zen
{
    /// <summary>
    /// Response from the Zen News endpoint
    /// </summary>
    public class NewsResponse
    {
        public int ItemCount { get; set; }
        public int ActualItemCount { get; set; }
        public int Offset { get; set; }
        public int TotalItems { get; set; }
        public int Limit { get; set; }
        public string ZenVersion { get; set; }
        public List<NewsItem> NewsItems { get; set; }
    }
}
