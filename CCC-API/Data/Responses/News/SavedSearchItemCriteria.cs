using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class SavedSearchItemCriteria
    {
        public string Q_Keywords { get; set; }
        public string Q_StartDate { get; set; }
        public string Q_EndDate { get; set; }
        public bool Q_IncludeDuplicates { get; set; }
        public SavedSearchCriteriaManyIds<int> Q_Tags { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_SmartTags { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_Outlets { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_OutletDmas { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_OutletMediums { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_OutletLists { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_OutletLocations { get; set; }
        public List<int> Q_Tones { get; set; }
        public SavedSearchCriteriaManyIds<string> Q_Campaigns { get; set; }
        public List<SavedSearchCriteriaSingleId<string>> Q_Customfields { get; set; }
        public List<SavedSearchCriteriaSingleId<string>> Q_AnalyticsSearches { get; set; }
    }
}
