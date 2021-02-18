using System;

namespace CCC_API.Data.Responses.Analytics
{
    public class AnalyticsNewsItem
    {
        public int CirculationAudience { get; set; }
        public DateTime NewsDate { get; set; }
        public string OutletName { get; set; }
        public string OutletType { get; set; }
        public int PublicityValue { get; set; }
        public int UniqueVisitors { get; set; }
    }
}
