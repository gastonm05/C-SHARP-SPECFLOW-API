using System.Collections.Generic;

namespace CCC_API.Data.Responses.Zen
{
    /// <summary>
    /// Complete NewsItem from a Zen News endpoint response
    /// </summary>
    public class NewsItem
    {
        public string NewsItemId  { get; set; }
        public string Title { get; set; }
        public long PublicationDate { get; set; }
        public string OriginalUrl { get; set; }
        public string DataGroupId { get; set; }
        public string CompanyId { get; set; }
        public string DocumentSentiment { get; set; }
        public string MediaId { get; set; }
        public string MediaType { get; set; }
        public List<string> SearchId { get; set; }
        public string Status { get; set; }
        public string Medium { get; set; }
    }
}
