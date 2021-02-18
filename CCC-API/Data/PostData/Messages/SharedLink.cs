using System.Collections.Generic;

namespace CCC_API.Data.PostData.Messages
{
    public class SharedLink
    {
        public string url { get; set; }
        public object imageUrl { get; set; }
        public List<object> imageUrlsOnPage { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public object name { get; set; }
    }
}