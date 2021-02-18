using System;
using System.Collections.Generic;

namespace CCC_API.Data.PostData.Activities
{
    public class BulkAddToCampaignData
    {
        public bool allSelected { get; set; }
        public List<Delta> delta { get; set; }
        public filter filter { get; set; }
        public List<int> Values { get; set; }
        public string Operation { get; set; }
    }

    public class filter
    {
        public List<string> PublicationStates { get; set; } = new List<string>();
        public List<string> Types { get; set; } = new List<string>();
        public List<string> CampaignIds { get; set; } = new List<string>();
        public List<string> OwnerIds { get; set; } = new List<string>();
        public List<string> OutletIds { get; set; } = new List<string>();
        public List<string> TagsIds { get; set; } = new List<string>();
        public string StartDate { get; set; } = null;
        public string EndDate { get; set; } = null;
        public int Page { get; set; }
        public int RowCount { get; set; }
        public int UpperBound { get; set; }
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int SortOnCustomFieldId { get; set; }

        public filter()
        {
            Page = 1;
            RowCount = 50;
            UpperBound = 999999;
            SortField = "Time";
            SortDirection = "descending";
            SortOnCustomFieldId = 0;
        }
    }

    public class Delta
    {
        public string Type { get; set; }
        public string EntityId { get; set; }
    }
}
