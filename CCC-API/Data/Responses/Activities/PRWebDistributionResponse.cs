using CCC_API.Data.PostData.Activities;

namespace CCC_API.Data.Responses.Activities
{
    public class PRWebDistributionResponse : PRWebDistributionData
    {
        public int PRWebPressReleaseID { get; set; }
        public int DistributionID { get; set; }
        public PRWebSubscriptionResponse Subscription { get; set; }
    }
}
