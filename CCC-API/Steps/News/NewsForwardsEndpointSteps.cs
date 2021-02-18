using BoDi;
using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.News
{
    [Binding]
    public class NewsForwardsEndpointSteps : AuthApiSteps
    {
        public NewsForwardsEndpointSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string POST_FORWARDS_RESPONSE_KEY = "PostForwardsResponse";

        [When(@"I POST to News Forwards endpoint with another Company template")]
        public void WhenIPostToNewsForwardsEndpointWithAnotherCompanyTemplate() {
            var response = new NewsViewService(SessionKey).GetAllNews();
            var forwardsService = new NewsForwardService(SessionKey);
            var itemsIDList = forwardsService.GetNewsItemsIds(response);
            var endDate = forwardsService.GetNewsForwardEndDate();
            var recipients = forwardsService.GetRecipientsList();
            var deltas = forwardsService.GetDelta();

            var forwardResponse = forwardsService.PostNewsForward(response.Data.Key, false,
                deltas, recipients, "XT15911", "Automation FWD", "1", "Automation FWD MSSG", 
                endDate, itemsIDList, "Sender Name", "sender@mailinator");
            PropertyBucket.Remember(POST_FORWARDS_RESPONSE_KEY, forwardResponse);
        }

        [When(@"I POST to News Forwards endpoint with all available fields")]
        public void WhenIPOSTToNewsForwardsEndpointWithAllAvailableFields()
        {
            var response = new NewsViewService(SessionKey).GetAllNews();
            var forwardsService = new NewsForwardService(SessionKey);
            var itemsIDList = forwardsService.GetNewsItemsIds(response);
            var endDate = forwardsService.GetNewsForwardEndDate();
            var recipients = forwardsService.GetRecipientsList();
            var deltas = forwardsService.GetDelta();

            var forwardResponse = forwardsService.PostNewsForward(response.Data.Key, false,
                deltas, recipients, "NT-2", "Automation FWD", "1", "Automation FWD MSSG",
                endDate, itemsIDList, "Sender Name", "sender@mailinator");
            PropertyBucket.Remember(POST_FORWARDS_RESPONSE_KEY, forwardResponse);
        }


        [Then(@"the News Forward endpoint should respond with a '(.*)'")]
        public void ThenTheNewsForwardEndpointShouldRespondWithAGivenStatusCode(int statusCode) {
            IRestResponse<Forwards> newsForward = PropertyBucket.GetProperty<IRestResponse<Forwards>>(POST_FORWARDS_RESPONSE_KEY);
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(newsForward), newsForward.Content);
        }

        [Then(@"the response message should be '(.*)'")]
        public void ThenTheResponseMessageShouldBe(string message) {
            IRestResponse<Forwards> newsForward = PropertyBucket.GetProperty<IRestResponse<Forwards>>(POST_FORWARDS_RESPONSE_KEY);
            var content = newsForward.Content;
            Assert.IsTrue(content.Contains(message), $"News Forward '{content}' did not contain '{message}'");
        }
    }
}
