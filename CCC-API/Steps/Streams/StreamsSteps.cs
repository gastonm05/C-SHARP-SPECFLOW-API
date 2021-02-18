using BoDi;
using CCC_API.Data.Responses.Streams;
using CCC_API.Services.Streams;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Streams
{
    [Binding]
    public class StreamsSteps : AuthApiSteps
    {
        public StreamsSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        private const string GET_RESPONSE_KEY = "get_response";

        [When(@"I create a stream (.*) for (.*) social network, using contact list(.*)")]
        public void WhenICreateAStreamTwitterForTwitterSocialNetworkUsingContactListGastonSListAndGroup(string stream_name, string platform, string list_name)
        {
            var streamsService = new StreamsService(SessionKey);
            int group = streamsService.GetGroups();
            int type = (int)(Platform)Enum.Parse(typeof(Platform), platform);
            var streamData = streamsService.ListName(list_name);

            //  Publish the message as a social media post
            var response = streamsService.CreateStream(type, stream_name, streamData, group);

            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I GET all streams")]
        public void WhenIGETAllStreams()
        {
            var streamService = new StreamsService(SessionKey);
            var response = streamService.GetStreams();
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }


        [Then(@"The response should be (.*)")]
        public void ThenTheResponseShouldBe(string status)
        {
            //  Get response from stream creation
            var response = PropertyBucket.GetProperty<IRestResponse<StreamResponse>>(GET_RESPONSE_KEY);

            //  Verify response's status
            Assert.AreEqual(status.Replace("\"", ""), response.StatusCode.ToString(), "Wrong Status code on the response");

        }

        [Then(@"I delete the previously created Stream")]
        public void ThenIDeleteThePreviouslyCreatedStream()
        {
            //  Get Stream Id 
            int streamId = (PropertyBucket.GetProperty<IRestResponse<StreamResponse>>(GET_RESPONSE_KEY)).Data.Item.Id;

            //  Delete the stream
            var streamsService = new StreamsService(SessionKey);
            IRestResponse response = streamsService.DeleteStream(streamId);
        }
    }
}
