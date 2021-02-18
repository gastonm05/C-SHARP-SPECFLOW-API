using BoDi;
using CCC_API.Data.Responses.Media.Outlet;
using CCC_API.Services.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class MediaResearchRequestSteps : AuthApiSteps
    {
        private const string RESPONSE_KEY = "Media Research Request Response";
        private const string ENTITY_TYPE_MEDIA_CONTACT = "MediaContact";
        private const string ENTITY_TYPE_MEDIA_OUTLET = "MediaOutlet";
        private const string ENTITY_TYPE_NEWS = "News";
        private readonly ContactService _contactService;

        public MediaResearchRequestSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _contactService = new ContactService(SessionKey);
        }

        [When(@"I perform a POST to Media Research Request Endpoint for Contact '(.*)' and Change Type '(.*)'")]
        public void WhenIPerformAPOSTToMediaResearchRequestEndpointForContactAndChangeType(string contact, string changeType)
        {
            int id;
            var contacts = TestData.DeserializedJson<List<Data.TestDataObjects.Media.Contact>>("Contacts.json", Assembly.GetExecutingAssembly());
            id = contacts.FirstOrError(c => c.FullName?.ToLower() == contact.ToLower(), $"'{contact}' not found in Contacts.json file.").Id;
            PropertyBucket.Remember(RESPONSE_KEY, _contactService.SendMediaResearchRequest(changeType, id, ENTITY_TYPE_MEDIA_CONTACT));
        }

        [When(@"I perform a POST to Media Research Request Endpoint for Outlet '(.*)' and Change Type '(.*)'")]
        public void WhenIPerformAPOSTToMediaResearchRequestEndpointForOutletAndChangeType(string outlet, string changeType)
        {
            int id;
            var outlets = TestData.DeserializedJson<List<OutletsItem>>("Outlets.json", Assembly.GetExecutingAssembly());
            id = outlets.FirstOrError(o => o.FullName?.ToLower() == outlet.ToLower(), $"'{outlet}' not found in Outlets.json file.").Id;
            PropertyBucket.Remember(RESPONSE_KEY, _contactService.SendMediaResearchRequest(changeType, id, ENTITY_TYPE_MEDIA_OUTLET));
        }
        [When(@"I perform a POST to Media Research Request Endpoint for an invalid entity type and Change Type '(.*)'")]
        public void WhenIPerformAPOSTToMediaResearchRequestEndpointForAnInvalidEntityType(string changeType)
        {
            PropertyBucket.Remember(RESPONSE_KEY, _contactService.SendMediaResearchRequest(changeType, 12, ENTITY_TYPE_NEWS));
        }

        [Then(@"the Media Research Request Endpoint response code should be '(.*)'")]
        public void ThenTheMediaResearchRequestEndpointResponseCodeShouldBe(int responseCode)
        {
            IRestResponse<Object> response = PropertyBucket.GetProperty<IRestResponse<Object>>(RESPONSE_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }
    }
}
