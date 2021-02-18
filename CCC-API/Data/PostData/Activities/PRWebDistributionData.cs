using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;
using System.Collections.Generic;

namespace CCC_API.Data.PostData.Activities
{
    public class PRWebDistributionData
    {
        public string SubscriptionId { get; set; }
        public string DistributionName { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string ReleaseDateTimezone { get; set; }
        public string ReleaseDate { get; set; }
        public List<City> Cities { get; set; }
        public string User { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public List<Topics> Topics { get; set; }
        public List<string> MediaDigests { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactCompany { get; set; }
        public string ContactWebsite { get; set; }
        public NewsImage NewsImage { get; set; }
        public List<NewsImage> AdditionalAttachments { get; set; }
        public string VideoURL { get; set; }
        public string CityState { get; set; }
        public List<PRWebIndustryOutletCategories> IndustryOutletCategories { get; set; }
        public bool IsCreatedInC3 { get; set; }
        public string PublicURL { get; set; }
        public string URLKeyword1 { get; set; }
        public string URLKeyword2 { get; set; }
        public string AdditionalContactName { get; set; }
        public string AdditionalContactCompany { get; set; }
        public string AdditionalContactWebsite { get; set; }
        public string AdditionalContactPhone { get; set; }
        public string AdditionalContactEmail { get; set; }
        public string CisionSocialPost { get; set; }
        public List<SocialMediaAccount> SocialMediaAccounts { get; set; }
        public string CompanyWebsite { get; set; }
		public string PullOutQuote { get; set; }
        public bool SendToIris { get; set; }
        public int CJLAddOnId { get; set; }
        public int CSPAddOnId { get; set; }
        public bool HasBeenSubmitted { get; set; }
        public bool HasBeenPublished { get; set; }
        public int PublicationState { get; set; }
        public SocialMediaMessageAccount SocialMediaMessageAccount { get; set; }
        public int DistributionVersion { get; set; }
        public int DistributionPRWebVersion { get; set; }
        public Dictionary<string, string> _Links { get; set; }
    }

    public class SocialMediaAccount
    {
        public string Type { get; set; }
        public string AccountName { get; set; }
        public string Avatar { get; set; }
        public bool Selected { get; set; }
        public bool Connected { get; set; }
    }

    public class SocialMediaMessageAccount
         : SocialMediaAccount
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
