using BoDi;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Media.Contact
{
    public class CommonContactSteps : AuthApiSteps
    {
        public CommonContactSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [Then(@"the Contact response code should be '(.*)'")]
        public void ThenTheContactResponseCodeShouldBe(int code)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(ContactSearchSteps.GET_CONTACTS_RESPONSE_KEY);
            var respCode = Services.BaseApiService.GetNumericStatusCode(response);
            Assert.AreEqual(code, respCode);
        }

    }
}
