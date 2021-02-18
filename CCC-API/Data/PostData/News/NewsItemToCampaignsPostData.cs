namespace CCC_API.Data.PostData.News
{
    public class NewsItemToCampaignsAssignmentData
    {
        public int[] CampaignIds { get; set; }

        public NewsItemToCampaignsAssignmentData(int[] campaignIds)
        {
            CampaignIds = campaignIds;
        }
    }
}