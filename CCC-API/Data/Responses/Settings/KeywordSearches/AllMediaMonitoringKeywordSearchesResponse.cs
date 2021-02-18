
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.KeywordSearches
{
    public class AllMediaMonitoringKeywordSearchesResponse
    {
        public int itemsCount { get; set; }
        public List<MediaMonitorKeywordSearch> items { get; set; }
    }
}
