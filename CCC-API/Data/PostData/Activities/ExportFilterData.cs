using System.Collections.Generic;

namespace CCC_API.Data.PostData.Activities
{
    /// <summary>
    /// Export payload for activities export.
    /// </summary>
    public class ExportFilterData
    {
        public Filter filter { get; set; }
        public List<ExportField> exportFields { get; set; }
    }

    public class Filter
    {
        public List<string> PublicationStates { get; set; } = new List<string>();
        public List<string> Types { get; set; } = new List<string>();
        public List<string> CampaignIds { get; set; } = new List<string>();
        public List<string> OwnerIds { get; set; } = new List<string>();
        public int RowCount { get; set; }
        public int UpperBound { get; set; }
        public string SortField { get; set; }
        public string SortDirection { get; set; }

        public Filter()
        {
            RowCount = 0;
            UpperBound = 0;
            SortField = "Time";
            SortDirection = "descending";
        }
    }

    public class ExportField
    {
        public string Key { get; set; }
        public string Label { get; set; }
    }
}
