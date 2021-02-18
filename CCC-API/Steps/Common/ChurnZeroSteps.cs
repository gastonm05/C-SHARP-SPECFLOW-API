using BoDi;
using CCC_API.Services.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Common
{
    public class ChurnZeroSteps : AuthApiSteps
    {
        public ChurnZeroSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string CHURN_RESPONSE_KEY = "churn_response_key";

        [When(@"I perform a GET for ChurnZero Integrations")]
        public void WhenIPerformAGETForChurnZeroIntegrations()
        {
            var response = new ChurnZeroService(SessionKey).GetChurnZeroCompany();
            PropertyBucket.Remember(CHURN_RESPONSE_KEY, response);
        }

        [Then(@"the returned ChurnZero Company should be '(.*)'")]
        public void ThenTheReturnedChurnZeroCompanyShouldBe(string company)
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(CHURN_RESPONSE_KEY);
            Assert.That(response.Content, Is.EqualTo(company), "Enpoint returned incorrect company");
        }

    }
}
