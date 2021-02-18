using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media
{
    public class EntityListFilter
    {
        public int PageSize { get; set; }
        public List<EntityList> Results { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
