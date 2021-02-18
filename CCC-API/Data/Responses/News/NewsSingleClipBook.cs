using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsSingleClipBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
        public string Summary { get; set; }
        public int SortType { get; set; }
        public int GroupType { get; set; }
        public List<NewsClipbookGroup> Groups { get; set; }
        public int NumberOfClips { get; set; }
        public string Owner { get; set; }
        public string CreatedDate { get; set; }
        public string LastEditedDate { get; set; }
        public List<ClipbookItem> NewsItems { get; set; }
        public NewsSingelClipbookLinks _links { get; set; }
        public NewsSingleClipbookMeta _meta { get; set; }
        public NewsClipbookDeliveryOptions DeliveryOptions { get; set; }
    }

    public class NewsSingleClipbookMeta
    {
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class NewsSingelClipbookLinks
    {
        public string _self { get; set; }
    }
}
