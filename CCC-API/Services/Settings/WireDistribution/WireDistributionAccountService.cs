

using CCC_API.Data.Responses.Settings.WireDistribution;
using RestSharp;

namespace CCC_API.Services.Settings.WireDistribution
{
    public class WireDistributionAccountService : AuthApiService
    {
        private const string _wireDistributionEndPoint = "management/wiredistributionaccount";        
        public WireDistributionAccountService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a GET to WireDistributionAccount endpoint to get current Wire Distribution configuration
        /// </summary>               
        /// <returns> IRestResponse<WireDistributionPostData> </returns>
        public IRestResponse<WireDistributionConfig> GetCurrentWireDistributionConfiguration()
        {
            return Get<WireDistributionConfig>($"{_wireDistributionEndPoint}");
        }
        /// <summary>
        /// Performs a POST to WireDistributionAccount endpoint to set Wire Distribution configuration
        /// </summary>               
        /// 
        /// <returns>IRestResponse</returns>
        public IRestResponse SetCurrentWireDistributionConfiguration(WireDistributionConfig wireDistributionConfig)
        {
            return Post<WireDistributionConfig>(_wireDistributionEndPoint, GetAuthorizationHeader(), wireDistributionConfig);            
        }        
    }
}
