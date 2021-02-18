using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class MediaSavedSearch
    {
        public int ItemCount { get; set; }
        public List<MediaSavedSearchItem> Items { get; set; }
    }
}
