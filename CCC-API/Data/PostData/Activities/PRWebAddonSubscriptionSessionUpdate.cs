

namespace CCC_API.Data.TestDataObjects
{
    public class PRWebAddonSubscriptionSessionUpdate
    {
        public int CompanyId { get; set; }
        public int ApplicationId { get; set; }
        public int DistributionPRWebSubscriptionId { get; set; }
        public int AddOnPRWebSubscriptionId { get; set; }
        public int QuantityUsedByDistribution { get; set; }
        public string XApiKey { get; set; }
        public int DistributionId { get; set; }
    }
}
