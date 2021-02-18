using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsCustomFieldsView
    {
        public int ItemCount { get; set; }
        public List<NewsCustomFields> Items { get; set; }
    }
}
