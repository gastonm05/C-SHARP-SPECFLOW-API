using BoDi;
using CCC_API.Data.Responses.Accounts;
using CCC_API.Services.Common;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Reflection;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Security
{
    public class AuthenticationSteps : AuthApiSteps
    {
        public AuthenticationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        public const string TOKEN_RESPONSE_KEY = "token response key";

        [Given(@"I perform a GET to verify the token")]
        [When(@"I perform a GET to verify the token")]
        public void WhenIPerformAGETToVerifyTheToken()
        {
            var response = new AccountInfoService(SessionKey).VerifyToken();
            PropertyBucket.Remember(TOKEN_RESPONSE_KEY, response);
        }

        [Then(@"the token should be valid and return correct user information")]
        public void ThenTheTokenShouldBeValidAndReturnCorrectUserInformation()        
        {
            var returned = PropertyBucket.GetProperty<IRestResponse<VerifyTokenResponse>>(TOKEN_RESPONSE_KEY);
            var expected = TestData.DeserializedJson<VerifyTokenResponse>("TokenUserData.json", Assembly.GetExecutingAssembly());
            Assert.Multiple(() =>
                {
                    Assert.That(returned.Data.AccountId, Is.EqualTo(expected.AccountId), "AccountId does not equal expected value");
                    Assert.That(returned.Data.Email, Is.EqualTo(expected.Email), "Email does not equal expected value");
                    Assert.That(returned.Data.FirstName, Is.EqualTo(expected.FirstName), "First Name does not equal expected value");
                    Assert.That(returned.Data.LastName, Is.EqualTo(expected.LastName), "Last Name does not equal expected value");
                    Assert.That(returned.Data.Id, Is.EqualTo(expected.Id), "Id does not equal expected value");
                    Assert.That(returned.Data.OMCAccountID, Is.EqualTo(expected.OMCAccountID), "OMCAccountId does not equal expected value");
                    Assert.That(returned.Data.LanguageId, Is.EqualTo(expected.LanguageId), "LanguageId does not equal expected value");
                    Assert.That(returned.Data.LanguageCode, Is.EqualTo(expected.LanguageCode), "LanguageCode does not equal expected value");
                });
        }
        [Then(@"the token should be valid and return an empty AccountID")]
        public void ThenTheTokenShouldBeValidAndReturnCorrectUserInformationEmpty()
        {
            var returned = PropertyBucket.GetProperty<IRestResponse<VerifyTokenResponse>>(TOKEN_RESPONSE_KEY);
            Assert.That(returned.Data.OMCAccountID, Is.EqualTo(""), Err.Line("OMCAccountId Should be empty"));
        }
        [Then(@"the token should be valid and it shouldn't return an empty AccountID")]
        public void ThenTheTokenShouldBeValidAndReturnCorrectUserInformationNotEmpty()
        {
            var returned = PropertyBucket.GetProperty<IRestResponse<VerifyTokenResponse>>(TOKEN_RESPONSE_KEY);
            Assert.That(returned.Data.OMCAccountID, Is.Not.Null.And.Not.Empty, Err.Line("OMCAccountId Should NOT be empty"));
        }

        [Then(@"the token endpoint response status should be '(.*)'")]
        public void ThenTheTokenEndpointResponseStatusShouldBe(int status)
        {
            IRestResponse<VerifyTokenResponse> response = PropertyBucket.GetProperty<IRestResponse<VerifyTokenResponse>>(TOKEN_RESPONSE_KEY);
            Assert.AreEqual(status, Services.BaseApiService.GetNumericStatusCode(response), "Expected status code not received");
        }

    }
}
