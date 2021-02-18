using System.Collections.Generic;
using CCC_API.Data.PostData.Settings.CustomFields;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Data.Responses.Media.Outlet;

namespace CCC_API.Data.Responses.Activities
{
    /// <summary>
    /// Create New > New Activity.
    /// </summary>
    public class CustomActivity
    {
        public string ScheduleTime { get; set; }
        public string TimeZoneIdentifier { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
                
        public string Notes { get; set; }
        public ContactsItem Contact { get; set; }
        public OutletsItem Outlet { get; set; }
        public List<int> CampaignIds { get; set; }
        public List<AllowValue> CustomFields { get; set; }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DataGroupId { get; set; }
        public int PublicationState { get; set; }      
    }    
}
