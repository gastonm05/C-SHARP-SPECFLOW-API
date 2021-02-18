using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    class SingleSavedSearch
    {
        public SavedSearchItem Item { get; set; }
        public List<Links> Links { get; set; }    
    }
}
