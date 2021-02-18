using BoDi;
using CCC_API.Data.PostData.Common;
using CCC_API.Data.TestDataObjects;
using CCC_API.Services.Common;
using CCC_API.Utils.Assertion;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.ComponentModel;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class ContactSupportSteps : AuthApiSteps
    {
        private const string POST_RESPONSE_EDITORIAL_SUPPORT_REQUEST_KEY = "PostResponseEditorialSupportRequest";
        private const string EDITORIAL_CONTACT_DETAILS_ID_KEY = "EditorialContactDetailsid";
        public enum KeyType { VALID, INVALID }
        public ContactSupportSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        
        [When(@"I perform a POST for contact/editorial endpoint to send a Editorial Contact Request using a '(.*)' EditorialContactDetailsId")]
        public void WhenIPerformAPOSTForContactEditorialEndpointToSendAEditorialContactRequestUsingLanguageKeyWithALanguageKey(KeyType keytype)
        {

            var fromName = "Test Automation" + Guid.NewGuid();
            var fromEmail = "test@testemail.com";
            var message = "This is a automation test email message";
            var editorialContactDetailsId = 0;
            switch (keytype)
            {
                case KeyType.VALID:
                    editorialContactDetailsId = Int32.Parse(PropertyBucket.GetProperty<string>(EDITORIAL_CONTACT_DETAILS_ID_KEY));
                    break;
                case KeyType.INVALID:
                    editorialContactDetailsId = 99999;
                    break;
            }
            EditorialSupportPostData editorialSupportPostData = new EditorialSupportPostData(editorialContactDetailsId, fromName, fromEmail, message);
            var postResponse = new ContactService(SessionKey).SendEditorialContactRequest(editorialSupportPostData);
            PropertyBucket.Remember(POST_RESPONSE_EDITORIAL_SUPPORT_REQUEST_KEY, postResponse);
        } 

        [Then(@"Editorial Contact endpoint response should be '(.*)'")]
        public void ThenEditorialSupportEndpointResponseShouldBe(int responseCode)
        {
            IRestResponse<Object> response = PropertyBucket.GetProperty<IRestResponse<Object>>(POST_RESPONSE_EDITORIAL_SUPPORT_REQUEST_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }
        [Then(@"Editorial Contact Endpoint response message should be '(.*)'")]
        public void ThenEditorialContactEndpointResponseMessageShouldBe(string responseMessage)
        {
            IRestResponse<Object> response = PropertyBucket.GetProperty<IRestResponse<Object>>(POST_RESPONSE_EDITORIAL_SUPPORT_REQUEST_KEY);
            Assert.IsTrue(response.Content.Contains(responseMessage), "Message differ from expected");
        }
        [When(@"Send a GET request to Contact Support  endpoint with a separate request for each supported language key and  verify response should be correct on each case")]
        public void WhenSendAGETRequestToContactSupportEndpointWithASeparateRequestForEachSupportedLanguageKeyAndVerifyResponseShouldBeCorrectOnEachCase()
        {
            var languageKey = typeof(LanguageKeys);

            foreach (LanguageKeys eValue in Enum.GetValues(languageKey))
            {
                var type = typeof(LanguageKeys);
                var memInfo = type.GetMember(eValue.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                false);
                string description = ((DescriptionAttribute)attributes[0]).Description;

                var getResponse = new ContactService(SessionKey).GetContactSupportInfo(description);
                var info= JsonConvert.DeserializeObject<ContactSupport>(getResponse.Content);
                Assert.IsTrue(info.VerifyResponse(description, eValue), "Response for key:"+ description+ " was invalid");
            }            
        }
    }
}
