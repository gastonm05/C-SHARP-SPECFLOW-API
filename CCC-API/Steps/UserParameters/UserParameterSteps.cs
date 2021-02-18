using BoDi;
using CCC_API.Data.Responses.UserParameters;
using CCC_API.Services.UserParameters;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.UserParameters
{
    public class UserParameterSteps : AuthApiSteps
    {
        public UserParameterSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string USER_PARAM_RESPONSE_KEY = "UserParam Key";

        [Given(@"I create a user parameter with the name '(.*)' and a value of '(.*)'")]
        [When(@"I create a user parameter with the name '(.*)' and a value of '(.*)'")]
        public void WhenICreateAUserParameterWithTheNameAndAValueOf(string name, string value)
        {
            var response = new UserParametersService(SessionKey).CreateUserParameter(name, value);
            PropertyBucket.Remember(USER_PARAM_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for user parameters with the name '(.*)'")]
        public void WhenIPerformAGETForUserParametersWithTheName(string name)
        {
            var response = new UserParametersService(SessionKey).GetUserParameter(name);
            PropertyBucket.Remember(USER_PARAM_RESPONSE_KEY, response, true);
        }

        [When(@"I perform a GET for user parameters with the name '(.*)' to get value only")]
        public void WhenIPerformAGETForUserParametersWithTheNameToGetValueOnly(string name)
        {
            var response = new UserParametersService(SessionKey).GetUserParameterValue(name);
            PropertyBucket.Remember(USER_PARAM_RESPONSE_KEY, response, true);
        }


        [Then(@"the returned user parameter name is '(.*)' and the value is '(.*)'")]
        public void ThenTheReturnedUserParameterNameIsAndTheValueIs(string name, string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<UserParameter>>>(USER_PARAM_RESPONSE_KEY);
            var data = response.Data.FirstOrError("Failed to retrieve UserParameter object from response");
            Assert.AreEqual(name, data.ParameterName);
            Assert.AreEqual(value, data.ParameterValue);            
        }

        [Then(@"the returned user parameter value is '(.*)'")]
        public void ThenTheReturnedUserParameterValueIs(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<UserParameter>>>(USER_PARAM_RESPONSE_KEY);
            var data = response.Data.FirstOrError("Failed to retrieve UserParameter object from response");            
            Assert.AreEqual(value, data.ParameterValue);
        }

        [Then(@"the user parameter response code should be '(.*)'")]
        public void ThenTheUserParameterResponseCodeShouldBe(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(USER_PARAM_RESPONSE_KEY);
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(response));
        }

    }
}
