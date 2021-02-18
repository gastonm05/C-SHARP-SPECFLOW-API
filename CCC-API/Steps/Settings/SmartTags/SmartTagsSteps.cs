using BoDi;
using CCC_API.Data.Responses.Settings.SmartTags;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Settings.SmartTags
{
    [Binding]
    public class SmartTagsSteps : AuthApiSteps
    {
        private const string GET_SMART_TAGS_RESPONSE_KEY = "GetSmartTagsResponse";
        private const string PUT_SMART_TAGS_RESPONSE_KEY = "PutSmartTagsResponse";
        public SmartTagsSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I perform a GET on SmartTags Config endpoint endpoint")]
        public void WhenIPerformAGETOnSmartTagsConfigEndpointEndpoint()
        {
            var response = new NewsSmartTagsService(SessionKey).GetSmartTagsConfig();
            PropertyBucket.Remember(GET_SMART_TAGS_RESPONSE_KEY, response);
        }

        [Then(@"SmartTags Config GET endpoint response code should be (.*)")]
        public void ThenSmartTagsConfigGETEndpointResponseCodeShouldBe(int statusCode)
        {
            var responseGet = PropertyBucket.GetProperty<IRestResponse<SmartTagsConfig>>(GET_SMART_TAGS_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responseGet.StatusCode, responseGet.Content);
        }

        [When(@"I perform a PUT on SmartTags Config endpoint with these config values (.*) and (.*) and (.*) and (.*) and (.*)")]
        public void WhenIPerformAPUTOnSmartTagsConfigEndpointWithTheseConfigValuesAndAndAnd(int featureMentions, int featureWords, int briefMentions, int briefWords, string searchTerm)
        {
                var newSmartTagsConfig = new SmartTagsConfig
                {
                    FeatureMentions = featureMentions,
                    FeatureWords = featureWords,
                    BriefMentions = briefMentions,
                    BriefWords = briefWords,
                    SearchTerm = searchTerm
                };

                var putResponse = new NewsSmartTagsService(SessionKey).EditSmartTagsConfig(newSmartTagsConfig);
                PropertyBucket.Remember(PUT_SMART_TAGS_RESPONSE_KEY, putResponse);
        }

        [Then(@"SmartTags Config PUT endpoint response code should be (.*) and message should be (.*)")]
        public void ThenSmartTagsConfigEndpointResponseCodeShouldBe(int statusCode, string expectedMessage)
        {
            IRestResponse<SmartTagsConfig> responsePut = PropertyBucket.GetProperty<IRestResponse<SmartTagsConfig>>(PUT_SMART_TAGS_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responsePut.StatusCode, responsePut.Content);
            if (!expectedMessage.Equals("OK"))
            Assert.IsTrue(responsePut.Content.Contains(expectedMessage), "Message differs from expected message");
        }
    }
}