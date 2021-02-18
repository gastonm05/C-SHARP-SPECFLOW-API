using BoDi;
using CCC_API.Data.PostData.Media.Contact;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Services.Media.Contact;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using CCC_API.Data.PostData.Media.Outlet;

namespace CCC_API.Steps.Media.Contact
{
    [Binding]
    public class AddContactSteps : AuthApiSteps
    {
        private const string ENTITY_LISTS_FILTER_RESPONSE_KEY = "Entity Lists Filter Response";
        public const string CREATE_PRIVATE_CONTACT_RESPONSE_KEY = "Private Contact Response";
        private const string PRIVATE_CONTACT_LISTS_KEY = "Private Contact Lists";

        private readonly ContactsListService _contactsListService;
        private readonly ContactsService _contactsService;

        public AddContactSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _contactsListService = new ContactsListService(SessionKey);
            _contactsService = new ContactsService(SessionKey);
        }

        [When(@"I create a new contact with list data associated with outlet '(.*)' and the country '(.*)'")]
        public void WhenICreateANewContactWithListDataAndTheCountry(string outlet, int countryID)
        {
            int numLists = 2;
            var response = _contactsListService.PostEntityListsFilterByUser("MediaContact");
            PropertyBucket.Remember(ENTITY_LISTS_FILTER_RESPONSE_KEY, response);
            IEnumerable<ContactListData> lists = response.Data.Results.Some(numLists, $"Could not find {numLists} lists").Select(l => new ContactListData(l.Id, l.Name));
            PropertyBucket.Remember(PRIVATE_CONTACT_LISTS_KEY, lists);
            IRestResponse<ContactsItem> privateContactResponse = _contactsService.CreatePrivateContact(outlet, lists: lists, CountryId: countryID);
            PropertyBucket.Remember(CREATE_PRIVATE_CONTACT_RESPONSE_KEY, privateContactResponse);
        }

        [When(@"I create a new contact without list data associated with outlet '(.*)' and the country '(.*)'")]
        public void WhenICreateANewContactWithoutListDataAssociatedWithOutletAndTheCountry(string outlet, int countryID)
        {;
            var email = StringUtils.RandomEmail(7);
            IRestResponse<ContactsItem> privateContactResponse = _contactsService.CreatePrivateContact(outlet, CountryId: countryID, email:email);
            PropertyBucket.Remember(CREATE_PRIVATE_CONTACT_RESPONSE_KEY, privateContactResponse);
        }

        [When(@"I create a new contact data associated with outlet '(.*)' and the country '(.*)'")]
        public void WhenICreateANewContactDataAssociatedWithOutletAndTheCountry(string outlet, int Countryid)
        {            
            IRestResponse<ContactsItem> privateContactResponse = _contactsService.CreatePrivateContact(outlet, CountryId: Countryid);
            PropertyBucket.Remember(CREATE_PRIVATE_CONTACT_RESPONSE_KEY, privateContactResponse);
        }

        [When(@"I create a new private contact with country '(.*)'")]
        public void WhenICreateANewPrivateContactWithCountry(int countryId)
        {
            IRestResponse<ContactsItem> readonlyPrivateContactReponse = _contactsService.CreatePrivateContact(CountryId: countryId);
            PropertyBucket.Remember(CREATE_PRIVATE_CONTACT_RESPONSE_KEY, readonlyPrivateContactReponse);
        }


        [Then(@"the list data should match when I GET the new contact")]
        public void ThenTheListDataShouldMatch()
        {
            IRestResponse<ContactsItem> createResponse = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            IEnumerable<ContactListData> lists = PropertyBucket.GetProperty<IEnumerable<ContactListData>>(PRIVATE_CONTACT_LISTS_KEY);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, createResponse.StatusCode);
            int id = createResponse.Data.Id;
            IRestResponse<ContactsItem> getResponse = _contactsService.GetContactDetail(id);
            Assert.AreEqual(getResponse?.Data?.Lists?.Count, lists.Count());
            NUnit.Framework.Assert.True(getResponse?.Data?.Lists?.All(gl => lists.Any(l => l.ListId == gl.ListId && l.ListName == gl.ListName)),
                StackTraceErrorAppender.AddMultipleLines($"Lists did not match"));
        }

        [Then(@"the data should match when I GET the new contact")]
        public void ThenTheDataShouldMatchWhenIGETTheNewContact()
        {
            IRestResponse<ContactsItem> createResponse = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, createResponse.StatusCode);
            int id = createResponse.Data.Id;
            IRestResponse<ContactsItem> getResponse = _contactsService.GetContactDetail(id);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
            Assert.AreEqual(id, getResponse.Data.Id);
            Assert.AreEqual(createResponse.Data.OutletId, getResponse.Data.OutletId);
            Assert.AreEqual(createResponse.Data.Email, getResponse.Data.Email);
        }

        [Then(@"the contact is deleted")]
        public void ThenTheContactIsDeleted()
        {
            IRestResponse<ContactsItem> createResponse = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            int id = createResponse.Data.Id;
            _contactsService.DeletePrivateContact(id);
        }

        [Then(@"the country for the new contact created should contain '(.*)'")]
        public void ThenTheCountryForTheNewContactCreatedShouldContain(string country)
        {
            IRestResponse<ContactsItem> createResponse = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, createResponse.StatusCode, "The contact was not created");
            int id = createResponse.Data.Id;
            IRestResponse<ContactsItem> getResponse = _contactsService.GetContactDetail(id);
            Assert.AreEqual(getResponse.Data.CountryName, country, "The country was not added");
        }
    }
}
