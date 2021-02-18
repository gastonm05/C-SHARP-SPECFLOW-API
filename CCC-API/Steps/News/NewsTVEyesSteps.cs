using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.Utils;
using RestSharp;
using System.Linq;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.News
{
    class NewsTVEyesSteps : AuthApiSteps
    {
        private NewsTVEyesService _newsTVEyesService;
        private NewsViewService _newsViewService;
        private Teardown _td;

        private const string GET_TVEYES_EDITED_CLIPS_RESPONSE = "GetTVEyesEditedClipsResponse";

        public NewsTVEyesSteps(IObjectContainer objectContainer, Teardown teardown) : base(objectContainer)
        {
            _newsTVEyesService = new NewsTVEyesService(SessionKey);
            _newsViewService = new NewsViewService(SessionKey);
            _td = teardown;
        }

        #region When Steps
        [When(@"I perform a GET for TVEyes Clip with edited videos for News ID '(.*)'")]
        public void WhenIPerformAGETForTVEyesClipWithEditedVideosForNewsID(int newsId)
        {
            var clips = _newsTVEyesService.GetTVEyesEditedClips(newsId);
            PropertyBucket.Remember(GET_TVEYES_EDITED_CLIPS_RESPONSE, clips);
        }
        #endregion

        #region Then Steps
        [Then(@"I should see the TVEyes endppoint has the correct response")]
        public void ThenIShouldSeeTheTVEyesEndppointHasTheCorrectResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<TVEyesEditedClipsView>>(GET_TVEYES_EDITED_CLIPS_RESPONSE);
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see that a primary clip exists")]
        public void ThenIShouldSeeThatAPrimaryClipExists()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<TVEyesEditedClipsView>>(GET_TVEYES_EDITED_CLIPS_RESPONSE);
            var clips = response.Data;
            Assert.True(clips.Items.Any(item => item.IsPrimary.Equals(true)), "No Clip was tagged as Primary");
        }

        [Then(@"I should see that all clips have a Start Time")]
        public void ThenIShouldSeeThatAllClipsHaveAStartTime()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<TVEyesEditedClipsView>>(GET_TVEYES_EDITED_CLIPS_RESPONSE);
            var clips = response.Data.Items;
            foreach (var clip in clips)
            {
                Assert.IsNotNull(clip.StartDateTime, "Start Date Time was null");
            }
        }

        [Then(@"I should see that all clips have an End Time")]
        public void ThenIShouldSeeThatAllClipsHaveAnEndTime()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<TVEyesEditedClipsView>>(GET_TVEYES_EDITED_CLIPS_RESPONSE);
            var clips = response.Data.Items;
            foreach (var clip in clips)
            {
                Assert.IsNotNull(clip.EndDateTime, "End Date Time was null");
            }
        }

        [Then(@"I should see that all clips have a Type")]
        public void ThenIShouldSeeThatAllClipsHaveAType()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<TVEyesEditedClipsView>>(GET_TVEYES_EDITED_CLIPS_RESPONSE);
            var clips = response.Data.Items;
            foreach (var clip in clips)
            {
                Assert.IsNotNull(clip.Type, "Not all clips contain a value for Type");
            }
        }

        [Then(@"I should see that all clips hve a Download link")]
        public void ThenIShouldSeeThatAllClipsHveADownloadLink()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<TVEyesEditedClipsView>>(GET_TVEYES_EDITED_CLIPS_RESPONSE);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(response.Data._links.tveyesOrderSubmitCallback, "Order Submit Callback was null");
                Assert.IsNotNull(response.Data._links.tveyesOrderPlacedCallback, "Order Placed Callback was null");
                Assert.IsNotNull(response.Data._links.tveyesOrderCompleteCallback, "Order Complete Callback was null");
            });
        }
        #endregion
    }
}
