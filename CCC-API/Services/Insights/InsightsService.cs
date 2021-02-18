using CCC_API.Data.Responses.Insights;
using RestSharp;

namespace CCC_API.Services.Insights
{
    public class InsightsService : AuthApiService
    {
        public static string InsightsEndPoint = "social/analytics/";

        public InsightsService(string sessionKey) : base(sessionKey) { }


        /// <summary>
        /// This method retrieves Insights stats.
        /// </summary>
        /// <param name="social_network">social network to request insights data</param>
        /// <param name="days">period of time of requested stats</param>
        /// <returns> Specified social network insights data</returns> 
        public IRestResponse GetInsights(string social_network, int days)
        {
            return Get<InsightsResponse>(InsightsEndPoint + social_network + "?from=" + days);
        }
    }
}
