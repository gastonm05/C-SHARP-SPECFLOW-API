using System.Collections.Generic;

namespace CCC_API.Data.Responses.Activities
{
    public class PRWebSubscriptionResponse
    {
        public string PRWebSubscriptionID { get; set; }
        public string Name { get; set; }
        public string RemainingDistributions { get; set; }
        public string ExpirationDate { get; set; }
        public string SKU { get; set; }
        public double UnitPrice { get; set; }
        public bool IsEmpty { get; set; }
        public string TotalDistributions { get; set; }
        public bool IsIndustryList { get; set; }
        public Package Package { get; set; }
        public string StartDate { get; set; }
        public string AvailableIndustryOutletCategories { get; set; }
        public List<AddOns> AddOns { get; set; }
        public bool SendToIris { get; set; }
    }

    public static class SubscriptionType
    {
        public static string ADVANCE { get { return "Advance"; } }
        public static string PREMIUM { get { return "Premium"; } }
        public static string INFLUENCER { get { return "Influencer"; } }
        public static string WEB_POWER { get { return "Web Power"; } }
    }
}
