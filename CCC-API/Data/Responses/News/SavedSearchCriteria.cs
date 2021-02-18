using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class SavedSearchCriteriaSingleId<T>
    {
        public T Id { get; set; }
        public string Operator { get; set; }
    }

    public class SavedSearchCriteriaManyIds<T>
    {
        public List<T> Ids { get; set; }
        public string Operator { get; set; } 
        public List<string> Values { get; set; }
    }
}
