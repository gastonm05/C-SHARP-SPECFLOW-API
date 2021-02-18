using BoDi;
using CCC_API.Services.Media.Updates;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.Updates
{
    public class MediaUpdatesSteps : AuthApiSteps
    {
        public MediaUpdatesSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I perform a GET for Media Updates")]
        public void WhenIPerformAGETForMediaUpdates()
        {
            var response = new MediaUpdatesService(SessionKey).GetMediaUpdates();
            PropertyBucket.Remember("media updates key", response);
        }

        [Then(@"all returned Media Updates are valid")]
        public void ThenAllReturnedMediaUpdatesAreValid()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Data.Responses.Media.Updates>>("media updates key");
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No items were returned in the response");
            Assert.IsTrue(response.Data.Items.TrueForAll(i => i.Title != null), "Not all updates have a title");
        }

    }
}
