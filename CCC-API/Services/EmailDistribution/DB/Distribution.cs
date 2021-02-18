using System;

namespace CCC_API.Services.EmailDistribution.DB
{
    /// <summary>
    /// Represents a distribution table in the DB.
    /// </summary>
    public class Distribution
    {
        public int DistributionID { get; set; }
        public string Name { get; set; }
        public int TrackReaderInteractions { get; set; }
        public int EmailJobID { get; set; }

        public int DataGroupId { get; set; }
        public int CompanyID { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime SendDate { get; set; }

        public bool EmailWizard2015Version { get; set; }

    }
}
