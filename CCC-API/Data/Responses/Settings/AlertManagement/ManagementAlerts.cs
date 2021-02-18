using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.AlertManagement
{
    public class ManagementAlerts
    {
        public int ItemCount { get; set; }
        public List<ManagementAlert> Items { get; set; }
    }

    public class ManagementAnalyticsAlerts
    {
        public int ItemCount { get; set; }
        public List<AnalyticsAlert> Items { get; set; }
    }
}
