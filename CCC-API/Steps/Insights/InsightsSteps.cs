using BoDi;
using CCC_API.Data.Responses.Insights;
using CCC_API.Services.Insights;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;
using Zukini;

namespace CCC_API.Steps.Insights
{
    [Binding]
    public class InsightsSteps : AuthApiSteps

    {
        public InsightsSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        private const string GET_RESPONSE_KEY = "get_response";

        [When(@"I retrieve (.*) insights for the last (.*) days")]
        public void WhenIRetrieveInsightsForTheLastDays(string social_network, int days)
        {
            var insightsService = new InsightsService(SessionKey);
            //  Get the insights for the the specified time period for a the specific social network
            var response = insightsService.GetInsights(social_network, days);

            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"The insights response should be (.*)")]
        public void ThenTheInsightsResponseShouldBe(string status)
        {
            //  Get response from insights request
            var response = PropertyBucket.GetProperty<IRestResponse<InsightsResponse>>(GET_RESPONSE_KEY);

            //  Verify response's status
            Assert.AreEqual(status.Replace("\"", ""), response.StatusCode.ToString(), "Wrong Status code on the response");
        }
    }
}
