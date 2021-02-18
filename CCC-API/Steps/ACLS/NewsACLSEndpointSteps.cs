using BoDi;
using CCC_API.Data.Responses.ACLS;
using CCC_API.Services.ACLS;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.ACLS
{
    [Binding]
    public class NewsACLSEndpointSteps : AuthApiSteps
    {
        public NewsACLSEndpointSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string GET_RESPONSE_KEY = "GetResponse";

        /// <summary>
        /// Performs a GET to endpoint api/v1/acls
        /// </summary>
        [When(@"I perform a GET for News ACLS permissions")]
        public void WhenIPerformAGETForACLS()
        {
            var response = new NewsACLSService(SessionKey).GetNewsACLS();
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        /// <summary>
        /// Validates the Status Code Response from an endpoint
        /// </summary>
        [Then(@"the Endpoint response should be '(.*)'")]
        public void ThenTheEndpointResponseShouldBeAsExpected(int responseCode)
        {
            IRestResponse<NewsACLS> response = PropertyBucket.GetProperty<IRestResponse<NewsACLS>>(GET_RESPONSE_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        /// <summary>
        /// Validates a TRUE/FALSE value on a given set of ACLS for News
        /// </summary>
        [Then(@"the permission should be '(true|false)' for '(.*)' property from '(.*)' section")]
        public void ThenTheObjectShouldIncludeAValueOfTrueOrFalseForXProperty(bool value, string property, string section)
        {
            IRestResponse<NewsACLS> response = PropertyBucket.GetProperty<IRestResponse<NewsACLS>>(GET_RESPONSE_KEY);
            if (value == true)
                Assert.IsTrue(NewsACLSService.getPropertyValue(response, property, section), $"Permission '{property} {section}' was false");
            else
                Assert.IsFalse(NewsACLSService.getPropertyValue(response, property, section), $"Permission '{property} {section}' was false");
        }
    }
}
