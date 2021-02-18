using BoDi;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Services.Media.Contact;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.Contact
{
    [Binding]
    public class InstagramStreamSteps : AuthApiSteps
    {
        public InstagramStreamSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string GET_SINGLE_CONTACT = "GetSingleContact";
        private const string GET_CONTACTS_INSTAGRAM_DETAILS_RESPONSE = "GetContactsInstagramDetails";
        private const string FOLLOW_UNFOLLOW_CONTACT_INSTAGRAM = "FollowUnfollowContactInstagram";

        #region When Steps

        [When(@"I perform a GET for Instagram details for the selected contact")]
        public void WhenIPerformAGETForInstagramDetailsForFirstContact()
        {
            int id = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(ContactSearchSteps.GET_SINGLE_CONTACT).Data.Id;
            var response = new InstagramStreamService(SessionKey).GetContactInstagramDetails(id);
            PropertyBucket.Remember(GET_CONTACTS_INSTAGRAM_DETAILS_RESPONSE, response);
        }

        [When(@"I Follow the selected contact's Instagram")]
        public void WhenIFollowTheInstagramForContact()
        {
            int id = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(ContactSearchSteps.GET_SINGLE_CONTACT).Data.Id;
            var response = new InstagramStreamService(SessionKey).PostFollowContactInstagram(id);
            PropertyBucket.Remember(FOLLOW_UNFOLLOW_CONTACT_INSTAGRAM, response, true);
        }

        [When(@"I Unfollow the selected contact's Instagram")]
        public void WhenIUnfollowTheInstagramForContact()
        {
            int id = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(ContactSearchSteps.GET_SINGLE_CONTACT).Data.Id;
            var response = new InstagramStreamService(SessionKey).DeleteUnfollowContactInstagram(id);
            PropertyBucket.Remember(FOLLOW_UNFOLLOW_CONTACT_INSTAGRAM, response, true);
        }

        #endregion

        #region Then Steps
        [Then(@"I should see the username '(.*)' in the response")]
        public void ThenIShouldSeeTheUsernameInTheResponse(string username)
        {
            IRestResponse<InstagramDetail> response = PropertyBucket.GetProperty<IRestResponse<InstagramDetail>>(GET_CONTACTS_INSTAGRAM_DETAILS_RESPONSE);
            Assert.That(response.Data.UserName, Is.EqualTo(username), "The correct instagram was not found!");
        }

        [Then(@"The response should contains '(.*)'")]
        public void ThenTheResponseShouldContains(string responseBody)
        {
            string response = PropertyBucket.GetProperty<string>(FOLLOW_UNFOLLOW_CONTACT_INSTAGRAM);
            Assert.That(response.Contains(responseBody), "The Instagram account was NOT followed.");
        }

        [Then(@"the response should be '(.*)'")]
        public void ThenTheResponseShouldBe(string responseBody)
        {
            string response = PropertyBucket.GetProperty<string>(FOLLOW_UNFOLLOW_CONTACT_INSTAGRAM);
            Assert.That(response, Is.EqualTo(responseBody), "The Instagram account was NOT followed.");
        }
        #endregion
    }
}
