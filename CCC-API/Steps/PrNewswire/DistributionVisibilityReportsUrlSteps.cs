using BoDi;
using CCC_API.Data.Responses.PrNewswire;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.PrNewswire
{
    [Binding]
    public class DistributionVisibilityReportsUrlSteps : AuthApiSteps
    {
        public DistributionVisibilityReportsUrlSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        private const string GET_RESPONSE_KEY = "GetResponse";

        /// <summary>
        /// Performs a GET to endpoint api/v1/prnewswire/distribution/VisibilityReportsUrl
        /// </summary>
        [When(@"I perform a GET for prnewswire/distribution/VisibilityReportsUrl endpoint")]
        public void WhenIPerformAGETForPrnewswireDistributionVisibilityReportsUrlEndpoint()
        {         
            var response = new DistributionVisibilityReportUrlService(SessionKey).GetOMCLink();
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        /// <summary>
        /// Validates the Status Code Response from an endpoint
        /// </summary>
        /// 
        [Then(@"The Endpoint response code should be OK")]
        public void ThenTheEndpointResponseCodeShouldBeOK()

        {
            IRestResponse<DistributionVisibilityReportUrl> response = PropertyBucket.GetProperty<IRestResponse<DistributionVisibilityReportUrl>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
        }

        [Then(@"the response URL should be valid")]
        public void ThenTheResponseURLShouldBeValid()
        {
            IRestResponse<DistributionVisibilityReportUrl> response = PropertyBucket.GetProperty<IRestResponse<DistributionVisibilityReportUrl>>(GET_RESPONSE_KEY);
            string UrlToBeValidated = response.Content.Substring(1);
            
            bool IsValidUrl = new DistributionVisibilityReportUrlService(SessionKey).IsValidUrl(UrlToBeValidated);
            Assert.IsTrue(IsValidUrl, "URL doesn't have correct format");
        }  
    }
}
