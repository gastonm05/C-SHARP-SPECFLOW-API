using System.Collections.Generic;

namespace CCC_API.Services.Media.Contact
{
    internal class DeleteListData
    {
        public IEnumerable<int> Delta { get; set; }
        public bool SelectAll { get; set; }
        public Filter Filter { get; set; }
    }

    internal class Filter
    {
        public string FilterType { get; set; }
        public string EntityType { get; set; }
        public bool IncludePublicAndShared { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public int SortDirection { get; set; }
    }
}