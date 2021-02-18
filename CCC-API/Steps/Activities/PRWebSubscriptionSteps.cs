using BoDi;
using CCC_API.Data.Responses.Activities;
using CCC_API.Services.Activities;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.UserSupport;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Activities
{
    [Binding]
    public class PRWebSubscriptionSteps : AuthApiSteps
    {
        public PRWebSubscriptionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        private const string GET_RESPONSE_KEY = "GetResponse";
        private const string GET_SUBSCRIPTION_RESPONSE_KEY = "GetSubscriptionResponse";
        private const string GET_ADDONS_KEY = "GetAddons";

        [Given(@"I get subscription id of the published release '(.*)'")]
        public void GivenIGetSubscriptionIdOfThePublishedRelease(string distributionId)
        {
            var id = (string)PropertyBucket.GetProperty(distributionId);
            var response = new PRWebDistributionService(SessionKey).GetDistribution(int.Parse(id));
            PropertyBucket.Remember(GET_SUBSCRIPTION_RESPONSE_KEY, response.Data.SubscriptionId);
        }

        [When(@"I call get valid subscription endpoint")]
        public void WhenICallGetValidSubscriptionEndpoint()
        {
            var response = new PRWebDistributionService(SessionKey).GetValidSubscriptions();
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I get available addons for subscription '(.*)'")]
        public void WhenIGetAvailableAddonsForSubscription(string subscriptionType)
        {
            List<AddOns> addons = new PRWebDistributionService(SessionKey).GetAddonsBySubscriptionName(subscriptionType);
            PropertyBucket.Remember(GET_ADDONS_KEY, addons);
        }

        [When(@"I call the single subscription endpoint")]
        public void WhenICallTheSingleSubscriptionEndpoint()
        {
            var subscriotionUsed = PropertyBucket.GetProperty<string>(GET_SUBSCRIPTION_RESPONSE_KEY);
            var response = new PRWebDistributionService(SessionKey).GetSingleSubscription(subscriotionUsed);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"The response should have all valid subscription")]
        public void ThenTheResponseShouldHaveAllValidSubscription()
        {
            IRestResponse<List<PRWebSubscriptionResponse>> response = PropertyBucket.GetProperty<IRestResponse<List<PRWebSubscriptionResponse>>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            Assert.That(response.Data.Count, Is.GreaterThan(0), "The valid subscription should be more than 0.");
            var user = PropertyBucket.GetProperty<User>(ApiHooks.ScenarioUserKey);
            foreach(PRWebSubscriptionResponse sub in response.Data)
            {
                Assert.True(sub.Name.Contains("Premium") || sub.Name.Contains("Advance")
                    || sub.Name.Contains("Influencer") || sub.Name.Contains("Power"),
                    "This subscription should be the premium or advance subscription" +
                    "Connect with company = " + user.CompanyID + " And user = " + user.Username );
            }
            
        }

        [Then(@"I see only one subscription and it is the same as the release contain")]
        public void ThenISeeOnlyOneSubscriptionAndItIsTheSameAsTheReleaseContain()
        {
            var subscriotionUsed = PropertyBucket.GetProperty<string>(GET_SUBSCRIPTION_RESPONSE_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebSubscriptionResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "The endpoint call for single subscription fails.");
            Assert.AreEqual(subscriotionUsed, response.Data.PRWebSubscriptionID, "The subscription should be equal to the one used in the release but is not.");
        }

    }
}
