using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.WireDistribution
{
    public class WireDistributionConfig
    {
        public string CompanyWireDistributionAccountId { get; set; }
        public bool ImpactStartDateDefaultEnabled { get; set; }
        public bool ImpactStartDateDefaultEnabledDisabled => !ImpactStartDateDefaultEnabled;
        public string ImpactStartDate { get; set; }
        public List<DataGroupWireDistributionConfig> DataGroupWireDistributionAccounts { get; set; }
        
        public WireDistributionConfig() { }
    }
}
