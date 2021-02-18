namespace CCC_API.Data.Responses.ACLS
{
    public class DistributionACLS
    {
        public bool HasAccess { get; set; }
        public bool HasPRWeb { get; set; }
        public bool HasSummaryForSearchEngines { get; set; }
        public bool HasPressRelease { get; set; }
        public bool HasOMC { get; set; }
        public bool HasPRNConnectLink { get; set; }
        public DistributionEmail Email { get; set; }
        public DistributionReConnect ReConnect { get;set; }
    }

    public class DistributionEmail {

      public bool  CanGenerateReport { get; set; }
      public bool  CanView { get; set; }
      public bool  CanCreate { get; set; }
      public bool  CanEdit { get; set; }
      public bool  CanDelete { get; set; }

    }

    public class DistributionReConnect {

    public bool IsDemo { get; set; }
    public bool ShowFeaturedImage { get; set; }
    public DistributionServices Services { get; set; }
    public bool CanView { get; set; }
    public bool CanCreate { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; } 

    }

    public class DistributionServices {

        public bool HasAccessToCisionNews { get; set; }
        public bool HasAccessToFacebook { get; set; }
        public bool HasAccessToLinkedIn { get; set; }
        public bool HasAccessToTwitter { get; set; }
        public bool HasAccessToYHP { get; set; }
        public bool HasAccessToYHPSubscribers { get; set; }
        public bool HasAccessToDISE { get; set; }
        public bool HasAccessToPolitics { get; set; }
        public bool HasAccessToRealtid { get; set; }
        public bool HasAccessToCisionPromoted { get; set; }
        public bool HasAccessToDirektPremium { get; set; }
        public bool HasAccessToSwedenBio { get; set; }
        public bool HasAccessToBreakit { get; set; }
        public bool HasAccessToWireNordic { get; set; }
        public bool HasAccessToWebsiteNetwork { get; set; }
        public bool HasAccessToWireFinland { get; set; }
        public bool HasAccessToWireNorway { get; set; }
        public bool HasAccessToPRNVisibilityReport { get; set; }
        public bool HasAccessToPRNWireAsia { get; set; }
        public bool HasAccessToPRNWireAsiaAutomotive { get; set; }
        public bool HasAccessToPRNWireAsiaBioTechHealth { get; set; }
        public bool HasAccessToPRNWireAsiaTech { get; set; }
        public bool HasAccessToPRNWireEu { get; set; }
        public bool HasAccessToPRNWireEuAutomotive { get; set; }
        public bool HasAccessToPRNWireEuBioTechHealth { get; set; }
        public bool HasAccessToPRNWireEuTech { get; set; }
        public bool HasAccessToPRNWireBrazil { get; set; }
        public bool HasAccessToPRNWireGermany { get; set; }
        public bool HasAccessToPRNWireUs { get; set; }
        public bool HasAccessToPRNWireUsAutomotive { get; set; }
        public bool HasAccessToPRNWireUsBioTechHealth { get; set; }
        public bool HasAccessToPRNWireUsTech { get; set; }
        public bool HasAccessToPRNWirePortugal { get; set; }
        public bool HasAccessToPRNWireUk { get; set; }
        public bool HasAccessToExpressen { get; set; }
        public bool HasAccessToTaloussanomat { get; set; }
    }
}
