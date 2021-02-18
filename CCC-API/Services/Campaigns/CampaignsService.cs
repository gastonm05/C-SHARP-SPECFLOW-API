using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.Campaigns;
using CCC_API.Data.Responses.News;
using RestSharp;

namespace CCC_API.Services.Campaigns
{
    public class CampaignsService : AuthApiService
    {
        public static readonly string CampaignsUri = "campaigns";

        public CampaignsService(string sessionKey) : base(sessionKey){}

        /// <summary>
        /// Creates a campaign or otherwise throws AssertionError.
        /// </summary>
        /// <param name="campaign"></param>
        /// <returns></returns>
        public Campaign PostCampaign(Campaign campaign) =>
            Request().Post().Data(campaign).ToEndPoint(CampaignsUri).ExecCheck<Campaign>();

        /// <summary>
        /// Sends POST request to the campaigns endpoint.
        /// </summary>
        /// <param name="campaign"></param>
        /// <returns></returns>
        public IRestResponse<Campaign> TryPostCampaign(Campaign campaign) =>
            Request().Post().Data(campaign).ToEndPoint(CampaignsUri).Exec<Campaign>();

        /// <summary>
        /// Get campaign info by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Campaign GetCampaign(int id) =>
            Request().Get().ToEndPoint($"{CampaignsUri}/{id}").ExecCheck<Campaign>();

        /// <summary>
        /// Deletes campaign by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteCampaign(int id) =>
            Request().Delete().ToEndPoint($"{CampaignsUri}/{id}").Exec();

        /// <summary>
        /// Get list of campaigns for a session key.
        /// </summary>
        /// <returns>CampaingsList</returns>
        public CampaignsList GetCampaigns() =>
            Request().Get().ToEndPoint(CampaignsUri).ExecCheck<CampaignsList>();

        /// <summary>
        /// Edits existign campaign.
        /// </summary>
        /// <returns></returns>
        public Campaign EditCampaign(Campaign campaign) =>
            Request().Put().Data(campaign).ToEndPoint(CampaignsUri).ExecCheck<Campaign>();

        /// <summary>
        /// Assigns news article to specified campaign.
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="newsItemsToAdd"></param>
        public void SetCampaignsOnSingleNewsItem(NewsItem newsItem, NewsItemToCampaignsAssignmentData newsItemToCampaignsPostData)
        {   
            var response = Request().Data(newsItemToCampaignsPostData).Put()
                .ToEndPoint($"{CampaignsUri}/relationships/news/{newsItem.Id}")
                .ExecCheck(System.Net.HttpStatusCode.OK);
        }
    }
}
