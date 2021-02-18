using BoDi;
using CCC_API.Data.Responses.Common;
using CCC_API.Services.Company;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;
using Zukini;
using CCC_API.Data.Responses.Company;

namespace CCC_API.Steps.Common
{
    public class CompanySteps : AuthApiSteps
    {
        public CompanySteps(IObjectContainer objectContainer) : base(objectContainer) { }

        public static readonly string COMPANY_RESPONSE = "company response";

        /// <summary>
        /// Perform a GET to company endpoint using a customerId stored in PropertyBucket.
        /// </summary>
        [When(@"I get the Company Id using a Customer Id")]
        public void WhenIGetTheCompanyIdUsingTheCustomerId()
        {
            var customerId = PropertyBucket.GetProperty<string>("customerId");
            var response = new CompanyService().GetCompanyId(customerId);
            PropertyBucket.Remember(COMPANY_RESPONSE, response);
        }

        [When(@"I get the Company Id using customerId '(.*)'")]
        public void WhenIGetTheCompanyIdUsingCustomerId(string customerId)
        {
            var response = new CompanyService().GetCompanyId(customerId);
            PropertyBucket.Remember(COMPANY_RESPONSE, response);
        }

        /// <summary>
        /// Perform a GET to company endpoint using a non existing customerID.
        /// Stores CompanyIdUnauthorized into PropertyBucket.
        /// </summary>
        /// <param name="customerId"></param>
        [When(@"I get the Company Id using invalid customerId '(.*)'")]
        public void WhenIGetTheCompanyIdUsingInvalidCustomerId(string customerId)
        {
            var response = new CompanyService().GetCompanyIdUnauthorized(customerId);
            PropertyBucket.Remember(COMPANY_RESPONSE, response);
        }

        /// <summary>
        /// Assert that Company endpoint response stored in PropertyBucket has the correct customerId value.
        /// expected companyId must be stored in PropertyBucket
        /// </summary>
        [Then(@"the Company ID is correct")]
        public void ThenTheCompanyIDIsCorrect()
        {
            string expectedCompanyId = PropertyBucket.GetProperty<string>("companyId");
            var response = PropertyBucket.GetProperty<IRestResponse<CompanyId>>(COMPANY_RESPONSE);
            Assert.AreEqual(response.Content, expectedCompanyId, $"expected CustomerId to be {expectedCompanyId} but was {response.Content}");
        }

        [Then(@"the Company endpoint should return a 404 error with message '(.*)'")]
        public void ThenTheCompanyEndpointShouldReturnAErrorWithMessage(string message)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<CompanyIdUnauthorized>>(COMPANY_RESPONSE);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound, $"expected status code to be 404 but was {response.StatusCode}");
            Assert.AreEqual(message, response.Data.Message, $"expected Message to be {message} but was {response.Data.Message}");
        }
    }
}
