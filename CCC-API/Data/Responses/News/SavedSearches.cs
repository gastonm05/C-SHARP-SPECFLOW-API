using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class SavedSearches
    {
        public int ItemCount { get; set; }
        public List<SavedSearchItem> Items { get; set; }
    }
}
