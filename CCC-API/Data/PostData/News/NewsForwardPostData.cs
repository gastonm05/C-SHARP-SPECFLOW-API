using System.Collections.Generic;

namespace CCC_API.Data.PostData.News
{
    public class NewsForwardPostData
    {
        public string Key { get; set; }
        public bool SelectAll { get; set; }
        public List<int> Delta { get; set; }
        public List<string> Recipients { get; set; }
        public string Template { get; set; }
        public string Subject { get; set; }
        public string GroupBy { get; set; }
        public string Message { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<string> Items { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}
