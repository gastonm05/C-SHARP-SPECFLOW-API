using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class ClipbookView
    {
        public int ItemCount { get; set; }
        public List<ClipbookItem> Items {get; set;}
    }
}
