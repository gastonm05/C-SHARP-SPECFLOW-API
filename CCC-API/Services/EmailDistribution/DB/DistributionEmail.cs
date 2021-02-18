using System;

namespace CCC_API.Services.EmailDistribution.DB
{
    /// <summary>
    /// Represents DistributionEmail table from the DB.
    /// </summary>
    public class DistributionEmail
    {
        public int DistributionID { get; set; }
        public int DistributionEmailId { get; set; }
        public int TrackingType { get; set; }
        public string HTMLBody { get; set; }
        public object TrackingParameters { get; set; }

        public int EmailType { get; set; }
        public string Subject { get; set; }

        public string TextBody { get; set; }
        public DateTime PriorityDate { get; set; }
        public int PriorityType { get; set; }
        public int OverrideSender { get; set; }

        public string DisplayName { get; set; }
        public string ReplyToAddress { get; set; }

        public string TimeZoneName { get; set; }

        public bool OverrideOptOutAddress { get; set; }

        public int OptOutMessageLanguageID { get; set; }

        public int CompanyID { get; set; }
        public string OptOutName { get; set; }
        public string OptOutAddressLine1 { get; set; }
        public string OptOutAddressLine2 { get; set; }
        public string OptOutCity { get; set; }
        public string OptOutState { get; set; }
        public string OptOutZip { get; set; }
        public int OptOutCountryID { get; set; }
        public bool CarbonCopy { get; set; }

        // TODO add more fields
    }
}
