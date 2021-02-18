using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsAlerts
    {
        public List<NewsAlert> Items { get; set; }
        public int ItemCount { get; set; }
    }

    public class NewsAlert
    {
        public int Id { get; set; }
        public int SearchId { get; set; }
        public string Subject { get; set; }
    }
}
