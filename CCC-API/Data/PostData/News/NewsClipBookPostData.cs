using CCC_API.Data.Responses.News;
using System.Collections.Generic;
using static CCC_API.Services.News.NewsReportsService;

namespace CCC_API.Data.PostData.News
{
    public class NewsClipBookPostData
    {
        public string Title { get; set; }
        public List<int> NewsIds { get; set; }
        public string Summary { get; set; }
        public string Template { get; set; }
        public Sorting SortType { get; set; }
        public Grouping GroupType { get; set; }
        public List<NewsClipbookGroup> Groups { get; set; }
        public NewsClipbookDeliveryOptions DeliveryOptions { get; set; }
    }
}
