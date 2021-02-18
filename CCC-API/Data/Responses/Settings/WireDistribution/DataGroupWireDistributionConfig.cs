using System;

namespace CCC_API.Data.Responses.Settings.WireDistribution
{
    public class DataGroupWireDistributionConfig
    {
        public int DataGroupId { get; set; }
        public string WireDistributionAccountId { get; set; }
        public Boolean Enabled { get; set; }
        public bool Disabled => !Enabled;
        public Boolean ViewAll { get; set; }
        public bool ViewAllDisabled => !ViewAll;
    }
}
