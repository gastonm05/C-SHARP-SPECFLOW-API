using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsTags
    {
        public int ItemCount { get; set; }
        public List<NewsTag> Items { get; set; }
    }
}
