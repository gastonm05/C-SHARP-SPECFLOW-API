using System.Collections.Generic;

namespace CCC_API.Data.PostData.Messages
{
    public class Activity
    {
        public int Type { get; set; }
        public string Notes { get; set; }
        public string ScheduleTime { get; set; }
        public string TimeZoneIdentifier { get; set; }
        public string Title { get; set; }
        public object Contact { get; set; }
        public object Outlet { get; set; }
        public List<object> CustomFields { get; set; }
    }

}
