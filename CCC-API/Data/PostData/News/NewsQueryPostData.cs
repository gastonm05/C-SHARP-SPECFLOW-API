using System.Collections.Generic;

namespace CCC_API.Data.PostData.News
{
    public class NewsQueryPostData
    {
        public string searchCriteria { get; set; }
        public string searchValues { get; set; }
    }

    public class NewsQueryKeywordsPostData
    {
        public string q_keywords { get; set; }
    }

    public class NewsQueryOutletLocationsPostData
    {        
        public NewsQueryMultiTypeStringPostData q_outletlocations { get; set; }
    }

    public class NewsQueryOutletLocationPostData
    {
        public string q_outletlocation { get; set; }
    }

    public class NewsQueryDateRangePostData
    {
        public string q_enddate { get; set; }
    }

    public class NewsQueryTagsIncludeExcludePostData
    {
        public NewsQueryMultiTypePostData q_tags { get; set; }
    }

    public class NewsQueryOutletsListIncludeExcludePostData
    {
        public NewsQueryMultiTypePostData q_outletlists { get; set;}
    }

    public class NewsQueryOutletsIncludeExcludePostData
    {
        public NewsQueryMultiTypePostData q_outlets { get; set; }
    }

    public class NewsQueryOutletsIncludeExcludeAndKeywordsPostData
    {
        public NewsQueryMultiTypePostData q_outlets { get; set; }
        public string q_keywords { get; set; }
    }

    public class NewsCustomFieldsIncludeExcludePostData
    {
        public List<NewsCustomFieldPostData> q_customfields { get; set; }
    }

    public class NewsCustomFieldPostData
    {
        public List<int> values { get; set; }
        public string id { get; set; }
        public string @operator { get; set; }
    }

    public class NewsAnalyticsIncludeExcludePostData
    {
        public List<NewsAnalyticsPostdata> q_analyticssearches { get; set; }
    }

    public class NewsAnalyticsPostdata
    {
        public List<int> values { get; set; }
        public string id { get; set; }
        public string @operator { get; set; }
    }

    public class NewsQuerySmartTagsIncludeExcludePostData
    {
        public NewsQueryMultiTypePostData q_smarttags { get; set; }
    }

    public class NewsQueryMultiTypePostData
    {
        public List<int> ids { get; set; }
        public string @operator { get; set; }
    }

    public class NewsQueryOutletsInvalidPostData
    {
        public NewsQueryMultiTypeStringPostData q_outlets { get; set; }
    }

    public class NewsQueryMultiTypeStringPostData
    {
        public List<string> ids { get; set; }
        public string @operator { get; set; }
    }

    public class NewsQueryKeywordsStartDateEndDatePostData
    {
        public string q_keywords { get; set; }
        public string q_startdate { get; set; }
        public string q_enddate { get; set; }
    }

    public class NewsQueryAllDetailsPostData
    {
        public string q_keywords { get; set; }
        public string q_startdate { get; set; }
        public string q_enddate { get; set; }
        public NewsQueryMultiTypePostData q_tags { get; set; }
        public NewsQueryMultiTypePostData q_smarttags { get; set; }
        public List<int> q_tones { get; set; }
    }

    public class NewsQueryCompanyTonePostData
    {
        public int q_companytone { get; set; }
    }

    public class NewsQuerySocialLocationsPostData
    {
        public NewsQueryMultiTypeStringPostData q_sociallocations { get; set; }
    }

    public class NewsQueryMediaTypePostData
    {
        public NewsQueryMultiTypeStringPostData q_outletmediatypes { get; set; }
    }
}
