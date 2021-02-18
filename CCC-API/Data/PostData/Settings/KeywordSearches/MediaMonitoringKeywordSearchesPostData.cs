using CCC_API.Data.Responses.Settings.KeywordSearches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Settings
{
    public class MediaMonitoringKeywordSearchesPostData

    {
        public string Key { get; set; }
        public MediaMonitorUserSearch MediaMonitorUserSearch { get; set; }
        public int PageSize { get; set; }

        public MediaMonitoringKeywordSearchesPostData() { }

        public MediaMonitoringKeywordSearchesPostData(string key, MediaMonitorUserSearch mediaMonitorUserSearch)
        {
            this.Key= key;
            this.MediaMonitorUserSearch = mediaMonitorUserSearch;
            this.PageSize = 50;
            
        }
    }
}
