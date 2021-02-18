using BoDi;
using CCC_API.Data.Responses.Common;
using CCC_API.Services.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;
using Does = NUnit.Framework.Does;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class GeoSteps : AuthApiSteps
    {
        public GeoSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        private const string GET_RESPONSE_KEY = "GetResponse";

        #region When Steps

        [When(@"I call get valid Countries endpoint")]
        public void WhenICallGetValidCountriesEndpoint()
        {
            var response = new GeoService(SessionKey).GetValidCountries();
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for places with a param of '(.*)'")]
        public void WhenIPerformAGETForPlacesWithAParamOf(string value)
        {
            var response = new GeoService(SessionKey).GetPlaces(value);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        #endregion

        #region Then Steps

        [Then(@"the number of countries returned should equal '(.*)'")]
        public void ThenTheNumberOfCountriesReturnedShouldEqual(int number)
        {
            IRestResponse<List<Country>> response = PropertyBucket.GetProperty<IRestResponse<List<Country>>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            Assert.That(response.Data.Count, Is.EqualTo(number), $"Found: {response.Data.Count} Countries but expected: {number}");
        }

        [Then(@"each country name should not be null")]
        public void ThenEachCountryNameShouldNotBeNull()
        {
            //This can be improved by comparing a list of known countries to returned values
            IRestResponse<List<Country>> response = PropertyBucket.GetProperty<IRestResponse<List<Country>>>(GET_RESPONSE_KEY);
            foreach (var e in response.Data)
            {
                Assert.That(e.Name, Is.Not.Null, "At least one country returned with a null value for name");
            }
        }

        [Then(@"all result descriptions should contain '(.*)'")]
        public void ThenAllResultDescriptionsShouldContain(string value)
        {
            IRestResponse<GeoItems> response = PropertyBucket.GetProperty<IRestResponse<GeoItems>>(GET_RESPONSE_KEY);
            var items = response.Data.Items;
            items.ForEach(item => Assert.That(item.Description.ToLower(), Does.Contain(value.ToLower())));
        }

        #endregion

    }
}
