using System;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.KeywordSearches
{
    public class SampleKeywordSearchResponse
    {
        public int SearchWindow { get; set; }
        public Boolean ResultLimitReached { get; set; }
        public string Key { get; set; }
        public int ActiveCount { get; set; }
        public string Export { get; set; }
        public int TotalCount { get; set; }
        public int ItemCount { get; set; }
        public int TotalPages { get; set; }
        public List<Item> Items { get; set; }
        public Links _links { get; set; }

        public class Item
        {            
            public string Headline { get; set; }
            public string TextPreview { get; set; }
            public string NewsDate { get; set; }
            public string OutletName { get; set; }
        }
    }
}
