using BoDi;
using CCC_API.Data;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Contact;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Services.Common;
using CCC_API.Services.Media;
using CCC_API.Services.Media.Contact;
using CCC_API.Services.Media.Outlet;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using ZukiniWrap;
using static CCC_API.Services.Media.Contact.ContactsService;
using ContactItem = CCC_API.Data.TestDataObjects.Media.Contact;
using Does = NUnit.Framework.Does;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.Contact
{
    public class ContactSearchSteps : AuthApiSteps
    {
        public ContactSearchSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        public const string GET_CONTACTS_RESPONSE_KEY = "Get Contacts Response";
        private const string GET_FACETS_RESPONSE_KEY = "GetFacetsResponse";
        private const string GET_SIMILARCONTACTS_RESPONSE_KEY = "GetSimilarContactsResponse";
        private const string POST_CREATE_CONTACTS_LIST_KEY = "PostCreateContactsList";
        private const string POST_CREATE_CONTACT_LIST_KEY = "PostCreateContactList";
        private const string DELETE_LIST_RESPONSE_KEY = "DeleteListResponseKey";
        private const string POST_CREATE_PRIVATE_CONTACT = "PostCreatePrivateContact";
        private const string POST_DELETE_PRIVATE_CONTACT = "PostDeletePrivateContact";
        private const string GET_CONTACTS_DETAILS = "GetContactsDetails";
        private const string LIST_NAME_KEY = "listname";
        private const string OUTLET_ID = "outlet id";
        public const string GET_SINGLE_CONTACT = "GetSingleContact";
        public Key POST_RECENT_SEARCH_KEY; // also in OutletsSteps
        public Key GET_RECENT_SEARCH_KEY; // also in OutletsSteps
        private const string GET_SECOND_FILTER = "GETSECONDFILTER";
        private const string GET_SUGGESTION_RESPONSE_KEY = "GeContactSuggestion";
        public Key POST_SAVED_SEARCH_KEY; // also in OutletsSteps
        public Key GET_SAVED_SEARCH_KEY; // also in OutletsSteps
        public Key DELETE_RESPONSE_KEY; // also in OutletsSteps
        public Key EDIT_SAVED_SEARCH_KEY; // also in OutletsSteps
        private const string GET_CONTACTS_CONSOLIDATED_RESPONSE_KEY = "GetContactsConsolidatedResponse";
        private const string GET_CONTACTS_SORTED_KEY = "Get Contacts Sorted";
        private const string GET_CONTACTS_DETAILS_EDITED = "Get Contacts details edited";
        public const string GET_CONTACTS_EXPORT_RESPONSE = "Get Contacts export response";
        public Key PATCH_NOTES_KEY; // also in OutletsSteps
        public Key LIST_NOTE_KEY; // also in OutletsSteps and SocialInfluencersSteps
        public Key GET_RECENT_TWEETS_RESPONSE_KEY; // also in OutletsSteps
        public const string CONTACT_ID = "Contact ID";
        public const string SENDER_NAME = "sender name";
        public const string PATCH_OPTIN = "patch data";
        public const string CONTACT_KEY_IDS = "CONTACT_KEY_IDS";
        public const string CONTACT_IDS = "CONTACT_IDS";
        public Key RESPONSE_DELETE_KEY; // also in OutletsSteps
        public Key LIST_DETAILS_KEY;
        private const string GET_RESPONSE_KEY = "get_response";


        #region When Steps

        [When(@"I perform a GET for Contacts by '(.*)' criteria with a value of '(.*)'")]
        public void WhenIPerformAGETForContactsByCriteriaWithAValueOf(ContactsSearchCriteria criteria, string parameter)
        {
            var response = new ContactsService(SessionKey).GetContacts(criteria, parameter);
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, response);
        }
        
        [When(@"I perform a GET for Contacts by the following criteria:")]
        public void WhenIPerformAGETForContactsByTheFollowingCriteria(Table criteriaTable)
        {
            var criteria = new Dictionary<ContactsSearchCriteria, string>();
            
            criteriaTable.Rows.ToList().ForEach(r =>
            {
                criteria.Add(r["criteriaName"].ParseEnum<ContactsSearchCriteria>(), r["criteriaValue"]);
            });

            var response = new ContactsService(SessionKey).GetContactsMultipleCriteria(criteria);
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, response);
        }

        [When(@"I perfom a GET using the Twitter filter '(.*)'")]
        public void WhenIPerfomAGETUsingTheAndTheFilter(string facet)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var responseFacet = new ContactsService(SessionKey).GetFilterResults(response.Data.Key, facet);
            PropertyBucket.Remember(GET_FACETS_RESPONSE_KEY, responseFacet);
        }

        [When(@"I perform a GET using the Linkedin filter '(.*)'")]
        public void WhenIPerformAGETUsingTheLinkedinFilter(string facet)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var responseFacet = new ContactsService(SessionKey).GetFilterResults(response.Data.Key, facet);
            PropertyBucket.Remember(GET_FACETS_RESPONSE_KEY, responseFacet);
        }

        [When(@"I perform a GET using the Facebook filter '(.*)'")]
        public void WhenIPerformAGETUsingTheFacebookFilter(string facet)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var responseFacet = new ContactsService(SessionKey).GetFilterResults(response.Data.Key, facet);
            PropertyBucket.Remember(GET_FACETS_RESPONSE_KEY, responseFacet);
        }

        [When(@"I perform a GET for Contacts by '(.*)' Education ID:'(.*)'")]
        public void WhenIPerformAGETForContactsByEducationID(ContactsSearchCriteria criteria, string parameter)
        {
            var response = new ContactsService(SessionKey).GetContacts(criteria, parameter);
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for Similar Contacts using the first id from the previous search")]
        public void WhenIPerformAGETForSimilarContactsUsingTheFirstIdFromThePreviousSearch()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);            
            var responseSimilar = new ContactsService(SessionKey).GetSimilarContacts(response.Data.Items[0].Id);            
            PropertyBucket.Remember(GET_SIMILARCONTACTS_RESPONSE_KEY, responseSimilar);

        }

        [When(@"I perform a GET for Contacts with the filter '(.*)' in the filter category '(.*)'")]
        public void WhenIPerformAGETForContactsWithTheFilterInTheFilterCategory(string filter, string category)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var contactService = new ContactsService(SessionKey);
            IRestResponse<Facets> facets = contactService.GetFacets(response.Data.Key);
            var facetId = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower()) 
            && f.Text.ToLower().Equals(filter.ToLower())).Id;
            var facetText = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.Text.ToLower().Equals(filter.ToLower())).Text;
            IRestResponse<Contacts> contactsResponse = contactService.GetFilterResults(response.Data.Key, facetId.ToString());            
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, contactsResponse, true);
            PropertyBucket.Remember("text", facetText);
            PropertyBucket.Remember("facetID", facetId.ToString());
        }

        [When(@"I perform a GET for Contacts with the group '(.*)' in the filter category '(.*)'")]
        public void WhenIPerformAGETForContactsWithTheFilterInTheFilterGroup(string group, string category)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var contactService = new ContactsService(SessionKey);
            IRestResponse<Facets> facets = contactService.GetFacets(response.Data.Key);
            var facetId = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.CategoryText.ToLower().Equals(group.ToLower())).Id;
            var facetText = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.CategoryText.ToLower().Equals(group.ToLower())).Text;
            IRestResponse<Contacts> contactsResponse = contactService.GetFilterResults(response.Data.Key, facetId.ToString());
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, contactsResponse, true);
            PropertyBucket.Remember("text", facetText);
        }

        [When(@"I perform a GET for Contacts applying a second filter '(.*)' in the filter category '(.*)'")]
        public void WhenIPerformAGETForContactsApplyingASecondFilterInTheFilterCategory(string filter, string category)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var firstFacet = PropertyBucket.GetProperty<string>("facetID");
            var contactService = new ContactsService(SessionKey);
            IRestResponse<Facets> facets = contactService.GetFacets(response.Data.Key);
            var facetId = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.Text.ToLower().Equals(filter.ToLower())).Id;
            var facetsIds = ($"{firstFacet},{facetId.ToString()}");
            IRestResponse<Contacts> contactsResponse = contactService.GetFilterResults(response.Data.Key, facetsIds);
            PropertyBucket.Remember(GET_SECOND_FILTER, contactsResponse, true);
            
        }

        [When(@"I perform a Post for creating a list with a random name using the top three ids")]
        public void WhenIPerformAPostForCreatingAListNamedUsingTheTopThreeIds()
        {
            var listName = StringUtils.RandomAlphaNumericString(8);
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            string key = response.Data.Key;
            int[] ids = new int[3];
            for (int i = 0; i < 3; i++)
            {
                ids[i] = response.Data.Items[i].Id;
            }
            var contactListService = new ContactsListService(SessionKey);
            IRestResponse<Lists> contactsListResponse =  contactListService.PostContactsList(key, ids, listName);
            PropertyBucket.Remember(POST_CREATE_CONTACTS_LIST_KEY, contactsListResponse, true);
            PropertyBucket.Remember(LIST_NAME_KEY, listName);
        }


        [When(@"I perform a Post for creating a list with a long name")]
        public void WhenIPerformAPostForCreatingAListWithALongName()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            string key = response.Data.Key;

            int[] ids = new int[3];
            for (int i = 0; i < 3; i++)
            {
                ids[i] = response.Data.Items[i].Id;
            }

            var listName = StringUtils.RandomAlphaNumericString(257);

            var contactListService = new ContactsListService(SessionKey);
            IRestResponse<Lists> contactsListResponse = contactListService.PostContactsList(key, ids, listName);
            PropertyBucket.Remember(LIST_NAME_KEY, listName, true);
            PropertyBucket.Remember(POST_CREATE_CONTACTS_LIST_KEY, contactsListResponse, true);
        }

        [When(@"I create a random private contact associating with '(.*)' outlet")]
        public void WhenICreateARandomPrivateContactAssociatingWithOutlet(string outlet)
        {
            var contactService = new ContactsService(SessionKey);
            IRestResponse<ContactsItem> privateContactResponse = contactService.CreatePrivateContact(outlet);
            PropertyBucket.Remember(OUTLET_ID, privateContactResponse.Data.OutletId);
            PropertyBucket.Remember(POST_CREATE_PRIVATE_CONTACT, privateContactResponse);

        }

        [When(@"I delete the contact created")]
        public void WhenIDeleteTheContactCreated()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(AddContactSteps.CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            var contactService = new ContactsService(SessionKey);
            IRestResponse<DeleteItem> responseDelete = contactService.DeletePrivateContact(response.Data.Id);
            PropertyBucket.Remember(POST_DELETE_PRIVATE_CONTACT, responseDelete);
            //save the contact detail to verify if it was deleted properly
            IRestResponse<ContactsItem> contactDetail = contactService.GetContactDetail(response.Data.Id);
            PropertyBucket.Remember(GET_CONTACTS_DETAILS, contactDetail);
        }

        [When(@"I perform a GET for Contacts suggestion using the key '(.*)'")]
        public void WhenIPerformAGETForContactsSuggestionUsingTheKey(string param)
        {
            var responseSuggestion = new ContactsService(SessionKey).GetSuggestion(param);
            PropertyBucket.Remember(GET_SUGGESTION_RESPONSE_KEY, responseSuggestion);
        }

        [When(@"I perform a DELETE for the list")]
        public void WhenIPerformADELETEForTheList()
        {
            var list = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_CONTACTS_LIST_KEY);
            var response = new ContactsListService(SessionKey).DeleteContactsList(list.Data.Id);
            PropertyBucket.Remember(DELETE_LIST_RESPONSE_KEY, response, true);
        }

        [When(@"I perform a GET for the first contact listed")]
        public void WhenIPerformAGETForTheFirstContactListed()
        {
           var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
           int id = response.Data.Items.Select(i => i.Id).FirstOrError();
           IRestResponse<ContactsItem> responseDetail = new ContactsService(SessionKey).GetSingleContact(id);
            PropertyBucket.Remember(GET_SINGLE_CONTACT, responseDetail);

        }

        [When(@"I perform a POST to save the search using the key generated before")]
        public void WhenIPerformAPOSTToSaveTheSearchUsingTheKeyGeneratedBefore()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            IRestResponse<MediaSavedSearchItem> responseSearch = new ContactsService(SessionKey).PostRecentSearch(response.Data.Key);
            PropertyBucket.Remember(POST_RECENT_SEARCH_KEY, responseSearch);
        }

        [When(@"I perform a get for recent searches endpoint")]
        public void WhenIPerformAGetForRecentSearchesEndpoint()
        {
            IRestResponse<MediaSavedSearch> responseSavedSearch = new ContactsService(SessionKey).GetRecentSearches();
            PropertyBucket.Remember(GET_RECENT_SEARCH_KEY, responseSavedSearch);
        }

        [When(@"I perform a POST to keep the contact search using the key generated before")]
        public void WhenIPerformAPOSTToKeepTheContactSearchUsingTheKeyGeneratedBefore()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var name = StringUtils.RandomAlphaNumericString(8);
            IRestResponse<MediaSavedSearchItem> responseSearch = new ContactsService(SessionKey).PostSavedSearch(response.Data.Key, name);
            PropertyBucket.Remember(POST_SAVED_SEARCH_KEY, responseSearch);                   
        }


        [When(@"I perform a GET for saved searches endpoint")]
        public void WhenIPerformAGETForSavedSearchesEndpoint()
        {           
            IRestResponse<MediaSavedSearch> responseSavedSearch = new ContactsService(SessionKey).GetSavedSearches();
            PropertyBucket.Remember(GET_SAVED_SEARCH_KEY, responseSavedSearch);
        }

        [When(@"I perform a DELETE for the saved search created")]
        public void WhenIPerformADELETEForTheSavedSearchCreated()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);
            var id = response.Data.Id;
            var responseDeleted = new ContactsService(SessionKey).DeleteSavedSearch(id);
            PropertyBucket.Remember(DELETE_RESPONSE_KEY, response);          
        }

        [When(@"I save the current Datagroup id")]
        public void WhenISaveTheCurrenDatagroupId()
        {
            var DataGroupId = new AccountInfoService(SessionKey).GetDataGroups().Items.FirstOrError().Id;            
            PropertyBucket.Remember<int>("id", DataGroupId);
        }

        [When(@"I edit the name of the first contact saved search listed")]
        public void WhenIEditTheNameOfTheFirstContactSavedSearchListed()
        {
            var newName = StringUtils.RandomAlphaNumericString(10);
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_SAVED_SEARCH_KEY);
            var id = response.Data.Items.FirstOrError().Id;
            var responsePatch = new ContactsService(SessionKey).EditSavedSearch(id, newName);
            PropertyBucket.Remember(EDIT_SAVED_SEARCH_KEY, responsePatch);           
        }

        [When(@"I add all results to a random list")]
        public void WhenIAddAllResultsToARandomList()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var listName = StringUtils.RandomAlphaNumericString(10);
            var responseList = new ContactsService(SessionKey).PostContactsList(response.Data.Key, listName);
            PropertyBucket.Remember<string>("listname", listName);
        }

        [When(@"I add all results to a new list with special characters")]
        public void WhenIAddAllResultsToAListWithSpecialCharacters()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var listNameWithSpecialCharacters = $"{StringUtils.RandomAlphaNumericString(5)};";
            var createListResponse = new ContactsService(SessionKey).PostContactsList(response.Data.Key, listNameWithSpecialCharacters);
            PropertyBucket.Remember(POST_CREATE_CONTACTS_LIST_KEY, createListResponse, true);
        }

        [When(@"I add the first result to a new list with special characters using the profile endpoint")]
        public void WhenIAddFirstResultToANewListWithSpecialCharactersUsingProfileEndpoint()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var listNameWithSpecialCharacters = $"{StringUtils.RandomAlphaNumericString(5)};";
            var contactId = response.Data.Items[0].Id;
            var createListResponse = new ContactsService(SessionKey).PostContactToNewList(contactId, listNameWithSpecialCharacters);
            PropertyBucket.Remember(POST_CREATE_CONTACT_LIST_KEY, createListResponse, true);
        }

        [When(@"I perform a GET for Contacts by '(.*)'criteria with a consolidated profiles as '(.*)'")]
        public void WhenIPerformAGETForContactsByCriteriaWithAConsolidatedProfilesAs(ContactsSearchCriteria criteria, string consolidated)
        {
            var listName = PropertyBucket.GetProperty<string>("listname");
            var responseConsolidated = new ContactsService(SessionKey).GetContactsConsolidated(criteria, listName, consolidated);
            PropertyBucket.Remember(GET_CONTACTS_CONSOLIDATED_RESPONSE_KEY, responseConsolidated);
        }

        [When(@"I perform a GET for Contacts by '(.*)'criteria with a value of '(.*)' and the consolidated profiles as '(.*)'")]
        public void WhenIPerformAGETForContactsByCriteriaWithAValueOfAndTheConsolidatedProfilesAs(ContactsSearchCriteria criteria,string parameter, string consolidated)
        {
            var response = new ContactsService(SessionKey).GetContactsConsolidated(criteria, parameter, consolidated);
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, response);
        }

        [When(@"I sort Contacts items '(.*)'")]
        public void WhenISortContactsItems(string sortfield)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var sort = new ContactsService(SessionKey).SortContactsBy(response.Data.Key, sortfield);
            PropertyBucket.Remember(GET_CONTACTS_SORTED_KEY, sort);
        }

        [When(@"I perform a GET to the detail using the first id from the previous search")]
        public void WhenIPerformAGETToTheDetailUsingTheFirstIdFromThePreviousSearch()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var responseDetail = new ContactsService(SessionKey).GetContactDetail(response.Data.Items[0].Id);            
            PropertyBucket.Remember(GET_CONTACTS_DETAILS, responseDetail);           
        }

        [When(@"I perform a PATCH for the opted out property '(.*)'")]
        public void WhenIPerformAPATCHForTheOptedOutProperty(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(GET_CONTACTS_DETAILS);
            var list = new List<PatchData>();
            list.Add(new PatchData("update", "IsOptedOut", value));
            var responseEdited = new ContactsService(SessionKey).EditContact(response.Data.Id, list);
            PropertyBucket.Remember(GET_CONTACTS_DETAILS_EDITED, responseEdited);
        }

        [When(@"I perform a POST to export Contacts including '(.*)' field")]
        public void WhenIPerformAPOSTToExportContactsIncludingField(string field)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var postResponse = new ContactsService(SessionKey).ExportContacts(response.Data.Key, field);
            PropertyBucket.Remember(GET_CONTACTS_EXPORT_RESPONSE, postResponse);
        }

        [When(@"I perform a Patch for the list created before adding some ramdom notes")]
        public void WhenIPerformAPatchForTheListCreatedBeforeAddingSomeRamdomNotes()
        {
            var note = StringUtils.RandomAlphaNumericString(6);
            var response = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_CONTACTS_LIST_KEY);
            var patchNotesResponse = new EntityListService(SessionKey).PatchNotes(response.Data.Id, note);
            PropertyBucket.Remember(PATCH_NOTES_KEY, patchNotesResponse);
            PropertyBucket.Remember(LIST_NOTE_KEY, note);
        }

        [When(@"I perform a GET to for history using the first id from the previous search")]
        public void WhenIPerformAGETToForHistoryUsingTheFirstIdFromThePreviousSearch()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var responseDetail = new ContactsService(SessionKey).GetContactHistory(response.Data.Items[0].Id);
            PropertyBucket.Remember(GET_CONTACTS_DETAILS, responseDetail);
        }

        [When(@"I save the first ID of the response gotten from the GET request")]
        public void WhenISaveTheFirstIDOfTheResponseGottenFromTheGETRequest()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var firstContactObjectInResponse = new ContactsService(SessionKey).GetContactDetail(response?.Data?.Items?.FirstOrDefault().Id ?? 0);
            PropertyBucket.Remember(GET_CONTACTS_DETAILS, firstContactObjectInResponse);
        }

        [When(@"I perform a GET for Contacts recent tweets endpoint")]
        public void WhenIPerformAGETForContactsRecentTweetsEndpoint()
        {
            var contactDetails = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(GET_CONTACTS_DETAILS);
            var recentTweets = new ContactsService(SessionKey).GetRecentTweets(contactDetails.Data.Id);
            PropertyBucket.Remember(GET_RECENT_TWEETS_RESPONSE_KEY, recentTweets);
        }

        [When(@"I sort Contacts '(.*)' by '(.*)'")]
        public void WhenISortContactsBy(ContactsService.SortDirection sortDirection, ContactSortField field)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var sortedResponse = new ContactsService(SessionKey).SortContacts(response.Data.Key, field, sortDirection);
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, sortedResponse, true);
        }

        [When(@"I perform a POST for optin requests endopoint for the id of the contact '(.*)'")]
        public void WhenIPerformAPOSTForOptinRequestsEndopointForTheIdOfTheContact(string contact)
        {
            int id;
            var contacts = TestData.DeserializedJson<List<ContactItem>>("Contacts.json", Assembly.GetExecutingAssembly());
            try
            {
                id = contacts.Find(c => c.FullName.ToLower().Equals(contact.ToLower())).Id;
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException(Err.Msg($"'{contact}' not found in Contacts.json file."));
            }
            var sender = StringUtils.RandomAlphaNumericString(6);
            PropertyBucket.Remember(CONTACT_ID, id);
            var response = new ContactsService(SessionKey).PostOptinRequest(id, 1, sender);
            PropertyBucket.Remember(GET_CONTACTS_RESPONSE_KEY, response);
            PropertyBucket.Remember(SENDER_NAME, sender);
        }                

        [When(@"I perform a PATCH to edit the Has Opted in value")]
        public void WhenIPerformAPATCHToEditTheHasOptedInValue()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OptinRequest>>(GET_SINGLE_CONTACT);
            var responsePatch = new ContactsService(SessionKey).PatchOptinRequest(response.Data.Key, "/HasOptedIn");
            PropertyBucket.Remember(PATCH_OPTIN, responsePatch);
        }              

        [When(@"I perform a PATCH for opted out the private contact created setting as '(.*)'")]
        public void WhenIPerformAPATCHForOptedOutThePrivateContactCreatedSettingAs(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(AddContactSteps.CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            var list = new List<PatchData>();
            list.Add(new PatchData("update", "IsOptedOut", value));
            var responseEdited = new ContactsService(SessionKey).EditContact(response.Data.Id, list);
            PropertyBucket.Remember(GET_CONTACTS_DETAILS_EDITED, responseEdited);
        }

        [When(@"I perform a POST for opt in request using the same private contact created before")]
        public void WhenIPerformAPOSTForOptInRequestUsingTheSamePrivateContactCreatedBefore()
        {
            var sender = StringUtils.RandomAlphaNumericString(6);
            var response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(GET_CONTACTS_DETAILS_EDITED);            
            var responseOptin = new ContactsService(SessionKey).PostOptinRequest(response.Data.Id, 1, sender);
            PropertyBucket.Remember(GET_SINGLE_CONTACT, responseOptin);
            PropertyBucket.Remember(SENDER_NAME, sender);
        }

        [When(@"I perform a GET for contacts ids for the following contacts '(.*)'")]
        public void WhenIPerformAGETForContactsIdsForTheFollowingOutlets(string contacts)
        {
            string[] contact = contacts.Split(new string[] { "," }, StringSplitOptions.None);
            var contactJson = TestData.DeserializedJson<List<ContactsItem>>("Contacts.json", Assembly.GetExecutingAssembly());
            var matches = contactJson.Where(c => contact.Any(item => c.FullName.Equals(item)));
            Assert.AreEqual(contact.Length, matches.Count(), $"{string.Join(",", contact.Where(c => !matches.Any(m => m.FullName.Equals(c))))} not found in Contacts.json file.");
            var ids = matches.Select(m => m.Id).ToList();
            var response = new ContactsService(SessionKey).GetContactsUsingIds(ids);
            PropertyBucket.Remember(CONTACT_KEY_IDS, response);
            PropertyBucket.Remember(CONTACT_IDS, ids);

        }

        [When(@"I delete the first item returned")]
        public void WhenIDeleteTheFirstItemReturned()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var deleteResponse = new ContactsService(SessionKey).DeletePrivateContact(response.Data.Items.FirstOrError().Id);
            PropertyBucket.Remember(RESPONSE_DELETE_KEY, deleteResponse);
        }

        [When(@"I duplicate the list for the same datagroup changing the name")]
        public void WhenIDuplicateTheListForTheSameDatagroupChangingTheName()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_CONTACTS_LIST_KEY);
            var DataGroupId = new AccountInfoService(SessionKey).GetDataGroups().Items.Where(i => i.Name.Contains("Default")).FirstOrError().Id;
            var newName = StringUtils.RandomAlphaNumericString(8);
            var duplicateResponse = new ContactsListService(SessionKey).DuplicateList(response.Data.Id, DataGroupId,newName, "MediaContact");
            PropertyBucket.Remember(RESPONSE_DELETE_KEY, duplicateResponse);
        }

        [When(@"I retrieve the list information recently duplicated")]
        public void WhenIRetrieveTheListInformationRecentlyDuplicated()
        {
            var listDetails = new ContactsListService(SessionKey).PostEntityListsFilterBySort("MediaContact","CreationDate",1);
            PropertyBucket.Remember(LIST_DETAILS_KEY, listDetails);
        }

        [When(@"I upload a file (.*) to create a import process for contacts")]
        public void WhenIUploadAFileBulkPrivateContactImportSample_CsvToCreateAImportProcessForContacts(string filepath)
        {
            var contactService = new ContactsService(SessionKey);
            var response = contactService.Upload(TestData.GetTestFileAbsPath(filepath, Assembly.GetExecutingAssembly()));
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }
        #endregion

        #region Then Steps
        [Then(@"the response should contain the list")]
        public void ThenTheResponseShouldContainTheListName()
        {
            var list = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_CONTACTS_LIST_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<long>>(DELETE_LIST_RESPONSE_KEY);
            Assert.AreEqual(list.Data.Id, Int32.Parse(response.Content), "The list was NOT properly deleted.");
        }

        [Then(@"all returned Contacts should contain '(.*)' in their sortname")]
        public void ThenAllReturnedContactsShouldContainInTheirSortname(string contact)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "The Get returned no data!");
            List<ContactsItem> items = response.Data.Items;
            items.ForEach(i => Assert.That(i.SortName.ToLower(), Is.EqualTo(contact.ToLower()), $"Not all Contacts are '{contact}'; Found Contact: '{i.SortName}'")); ;
        }

        [Then(@"all returned Contacts should contain '(.*)' in their outlet name")]
        public void ThenAllReturnedContactsShouldContainInTheirOutletName(string outletName)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "The Get returned no data!");
            List<ContactsItem> items = response.Data.Items;
            items.ForEach(i => Assert.That(i.OutletName.ToLower(), Is.EqualTo(outletName.ToLower()), $"Not all Contacts are in the outlet '{outletName}'; Found Contact outletName: '{i.OutletName}'")); ;
        }

        [Then(@"all returned Contacts should have '(.*)' in their sortname")]
        public void ThenAllReturnedContactsShouldHaveInTheirSortname(string contact)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "The Get returned no data!");
            List<ContactsItem> items = response.Data.Items;
            items.ForEach(i => Assert.IsTrue(i.SortName.ToLower().Contains(contact.ToLower()), $"Not all Contact names contain '{contact}'; Found Contact: '{i.SortName}'")); ;
        }

        [Then(@"all returned Contacts should have '(.*)' in their outlet name")]
        public void ThenAllReturnedContactsShouldHaveInTheirOutletName(string outletName)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "The Get returned no data!");
            List<ContactsItem> items = response.Data.Items;
            items.ForEach(i => Assert.IsTrue(i.OutletName.ToLower().Contains(outletName.ToLower()), $"Not all Contacts are in the outlet '{outletName}'; Found Contact outletName: '{i.OutletName}'")); ;
        }

        [Then(@"all returned contact objects should contain '(.*)' in their subjects")]
        public void ThenAllReturnedContactObjectsShouldContainInTheirSubjects(string name)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            List<ContactsItem> items = response.Data.Items;
            items.ForEach(i => Assert.IsTrue(i.Subjects.Select(s => s.ToLower()).Contains(name.ToLower()), $"Not all results have '{name}' in their subjects; Found Contact Name: '{i.SortName}' Contact Subjects: {i.Subjects}"));
        }

        [Then(@"the filter should be applied  for '(.*)' value")]
        public void ThenTheFilterShouldBeAppliedForValue(string value)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_FACETS_RESPONSE_KEY);
            List<ContactsItem> items = response.Data.Items;
            switch (value.ToLower())
            {
                case ("twitter"):
                    items.ForEach(i => i.SocialProfiles.ForEach(sp => sp.Twitter.ForEach(s => Assert.That(s.Url, Does.Contain(value)))));
                    break;
                case ("linkedin"):
                    items.ForEach(i => i.SocialProfiles.ForEach(sp => sp.LinkedIn.ForEach(s => Assert.That(s.Url, Does.Contain(value)))));
                    break;
                case ("facebook"):
                    items.ForEach(i => i.SocialProfiles.ForEach(sp => sp.Facebook.ForEach(s => Assert.That(s.Url, Does.Contain(value)))));
                    break;
            }     
           
        }

        [Then(@"all contacts should contain '(.*)' as their Subject")]
        public void ThenAllContactsShouldContainAsTheir(string value)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            response.Data.Items.ForEach(contact => Assert.That(contact.Subjects.Contains(value), Is.True,
                $"Not all contacts contain '{value}' as a Subject"));
        }

        [Then(@"I should see Similarity Score property greater than (.*)")]
        public void ThenIShouldSeeSimilarityScorePropertyGreaterThan(double score)
        {
            IRestResponse<List<SimilarContacts>> response = PropertyBucket.GetProperty<IRestResponse<List<SimilarContacts>>>(GET_SIMILARCONTACTS_RESPONSE_KEY);         
            response.Data.ForEach(i => Assert.That(i.SimilarityScore, Is.GreaterThan(score)));
        }

        [Then(@"the list created should return a non-null response")]
        public void ThenTheListCreatedShouldReturnANon_NullResponse()
        {
            IRestResponse<Lists> response = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_CONTACTS_LIST_KEY);
            var listname = PropertyBucket.GetProperty(LIST_NAME_KEY);
            Assert.AreEqual(listname, response.Data.Name, "The list was not created properly");
        }

        [Then(@"the search should return contacts")]
        public void ThenTheSearchShouldReturnContacts()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No contacts found");
        }

        [Then(@"I perform a Post for creating a list with a random name using the top three ids")]
        public void ThenIPerformAPostForCreatingAListWithARandomNameUsingTheTopThreeIds()
        {
            var listName = StringUtils.RandomAlphaNumericString(8);
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            string key = response.Data.Key;
            int[] ids = new int[3];
            for (int i = 0; i < 3; i++)
            {
                ids[i] = response.Data.Items[i].Id;
            }
            var contactListService = new ContactsListService(SessionKey);
            IRestResponse<Lists> contactsListResponse = contactListService.PostContactsList(key, ids, listName);
            PropertyBucket.Remember(LIST_NAME_KEY, listName, true);
            PropertyBucket.Remember(POST_CREATE_CONTACTS_LIST_KEY, contactsListResponse, true);
        }

        [Then(@"the response will be an error message")]
        public void ThenTheResponseWillBeAnErrorMessage()
        {
            IRestResponse<Lists> response = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_CONTACTS_LIST_KEY);
            Assert.That(response.Content.Contains("A list name must be between 1 and 255 characters"), "The response did not display the correct error message!");
        }       

        [Then(@"all returned contact objects should contain '(.*)' in their IsProprietaryContact property")]
        public void ThenAllReturnedContactObjectsShouldContainInTheirIsProprietaryContactProperty(string value)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "The Get returned no data!");
            List<ContactsItem> items = response.Data.Items;
            items.ForEach(i => Assert.That(i.IsProprietaryContact.ToString().ToLower(), Is.EqualTo(value.ToLower()), $"Not all Contacts are '{value}'; Found Contact: '{i.IsProprietaryContact}'")); ;
        }

        [Then(@"I should get the '(.*)' status code")]
        public void ThenIShouldGetTheStatusCode(int statusCode)
        {
            IRestResponse<DeleteItem> response = PropertyBucket.GetProperty<IRestResponse<DeleteItem>>(POST_DELETE_PRIVATE_CONTACT);
            IRestResponse<ContactsItem> contactDetail = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(GET_CONTACTS_DETAILS);
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(response), "The delete process failed");
            Assert.AreEqual(404, Services.BaseApiService.GetNumericStatusCode(contactDetail), "The contact was not deleted");

        }

        [Then(@"I should get the list created in the contact details response")]
        public void ThenIShouldGetTheListCreatedInTheContactDetailsResponse()
        {            
            IRestResponse<ContactsItem> response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(GET_SINGLE_CONTACT);
            var text = PropertyBucket.GetProperty<string>("text");         
            Assert.That(response.Data.Lists.Any(i => i.ListName == text), "The filter was not applied properly");            
        }

        [Then(@"all filtered contacts returned should have '(.*)' as their Outlet Country")]
        public void ThenAllFilteredContactsReturnedShouldHaveAsTheirOutletCountry(string country)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet country
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.IsTrue(outlets.Data.Items.All(c => c.CountryName.Equals(country)), 
                $"Not all outlet countries are '{country}', results filtered incorrectly");
        }

        [Then(@"all filtered contacts returned should have '(.*)' as their Outlet City")]
        public void ThenAllFilteredContactsReturnedShouldHaveAsTheirOutletCity(string city)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet city
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.IsTrue(outlets.Data.Items.All(c => c.City.Equals(city)),
                $"Not all outlet cities are '{city}', results filtered incorrectly");
        }

        [Then(@"all filtered contacts returned should have '(.*)' as their Outlet Type")]
        public void ThenAllFilteredContactsReturnedShouldHaveAsTheirOutletType(string type)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet type
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.IsTrue(outlets.Data.Items.All(c => c.TypeName.Equals(type)),
                $"Not all outlet types are '{type}', results filtered incorrectly");
        }

        [Then(@"all filtered contacts returned should have '(.*)' as their DMA")]
        public void ThenAllFilteredContactsReturnedShouldHaveAsTheirDMA(string dma)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet type
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.IsTrue(outlets.Data.Items.All(c => c.DMAName.Equals(dma)),
                $"Not all outlet DMAs are '{dma}', results filtered incorrectly");
        }

        [Then(@"I should find the recent searched saved")]
        public void ThenIShouldFindTheRecentSearchedSaved()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_RECENT_SEARCH_KEY);
            var responseGet = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_RECENT_SEARCH_KEY);
            Assert.That(responseGet.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var idPost = response.Data.Id;
            Assert.That(responseGet.Data.Items.Any(i => i.Id.Equals(idPost)), "The search was not saved");
        }

        [Then(@"all returned contacts should have '(.*)' as ther country and '(.*)' as their Outlet Type")]
        public void ThenAllReturnedContactsShouldHaveAsTherCountryAndAsTheirOutletType(string country, string mediatype)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_SECOND_FILTER);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet type
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.IsTrue(outlets.Data.Items.All(c => c.TypeName.Equals(mediatype)),
                $"Not all outlet types are '{mediatype}', results filtered incorrectly");
            Assert.IsTrue(outlets.Data.Items.All(c => c.CountryName.Equals(country)),
                $"Not all outlet countries are '{country}', results filtered incorrectly");
        }

        [Then(@"I should see '(.*)' in contact name as a suggestion")]
        public void ThenIShouldSeeInContactNameAsASuggestion(string contact)
        {
            IRestResponse<ContactSuggestion> response = PropertyBucket.GetProperty<IRestResponse<ContactSuggestion>>(GET_SUGGESTION_RESPONSE_KEY);
            List<ContactChildren> child = response.Data.Suggestions;
            Assert.That(child.Count(), Is.GreaterThan(0), "No contacts returned by search");
            Assert.Multiple(() =>
            {
                child.ForEach(i => Assert.IsTrue(i.Name.ToLower().Contains(contact.ToLower()), $"The suggested children have not {contact} in their name"));
            });
        }     

        [Then(@"I should find the saved searched")]
        public void ThenIShouldFindTheSavedSearched()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);
            var responseGet = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_SAVED_SEARCH_KEY);
            Assert.That(responseGet.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var idPost = response.Data.Id;
            Assert.That(responseGet.Data.Items.Any(i => i.Id.Equals(idPost)), "The search was not saved");
            Assert.That(responseGet.Data.Items.Any(i => i.Name.Equals(response.Data.Name)), "The name does not match");
        }

        [Then(@"all filtered contacts returned should have FALSE as their IsProprietary value")]
        public void ThenAllFilteredContactsReturnedShouldHaveFalseeAsTheirIsProprietaryValue()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.IsTrue(response.Data.Items.All(c => c.IsProprietaryContact.Equals(false)),
                $"Not all IsPropietaryContact are False, results filtered incorrectly");
        }

        [Then(@"the saved search should be deleted properly")]
        public void ThenTheSavedSearchShouldBeDeletedProperly()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);
            var responseGet = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_SAVED_SEARCH_KEY);
            var id = response.Data.Id;          
            Assert.IsFalse(responseGet.Data.Items.Any(i => i.Id == id), "The saved search was not deleted");
        }


        [Then(@"I should find the recent searched saved with the proper Datagroup id")]
        public void ThenIShouldFindTheRecentSearchedSavedWithTheProperDatagroupId()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_RECENT_SEARCH_KEY);
            var Datagroup = PropertyBucket.GetProperty<int>("id");
            var responseGet = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_RECENT_SEARCH_KEY);
            Assert.That(responseGet.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var idPost = response.Data.Id;            
            Assert.That(responseGet.Data.Items.Any(i => i.Id.Equals(idPost) && i.DataGroupId.Equals(Datagroup)), "The search was not saved");
        }

        [Then(@"I should find the saved searched with the proper Datagroup id")]
        public void ThenIShouldFindTheSavedSearchedWithTheProperDatagroupId()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);
            var Datagroup = PropertyBucket.GetProperty<int>("id");
            var responseGet = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_SAVED_SEARCH_KEY);
            Assert.That(responseGet.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var idPost = response.Data.Id;
            Assert.That(responseGet.Data.Items.Any(i => i.Id.Equals(idPost) && i.DataGroupId.Equals(Datagroup)), "The search was not saved");
            Assert.That(responseGet.Data.Items.Any(i => i.Name.Equals(response.Data.Name)), "The name does not match");
        }
        [Then(@"I should see the name edited of the contact saved search")]
        public void ThenIShouldSeeTheNameEditedOfTheContactSavedSearch()
        {
            var responseGet = new ContactsService(SessionKey).GetSavedSearches();
            Assert.That(responseGet.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(EDIT_SAVED_SEARCH_KEY);
            var name = response.Data.Name;
            Assert.IsTrue(responseGet.Data.Items.Any(i => i.Name.Equals(name)), "The name of saved search was not edited");
        }       

        [Then(@"I should get consolidated contacts when the parameter is on true")]
        public void ThenIShouldGetConsolidatedContactsWhenTheParameterIsOnTrue()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            IRestResponse<Contacts> responseConsolidated = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_CONSOLIDATED_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(responseConsolidated.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.IsTrue(response.Data.ActiveCount > responseConsolidated.Data.ActiveCount, "The consolidated parameter is not returning proper results");
        }

        [Then(@"I should get a response indicating the job failed")]
        public void ThenIShouldGetAResponseIndicatingTheJobFailed()
        {
            var response = PropertyBucket.GetProperty<RestResponse<Dictionary<string, string>>>(POST_CREATE_CONTACTS_LIST_KEY);
            var incident = JsonConvert.DeserializeObject<Incident>(response.Content);
            Assert.IsTrue(Regex.IsMatch(incident.Message, @"job.*to\ssave\sthe\slist.*failed", RegexOptions.IgnoreCase), "No results returned");
        }
        
        [Then(@"I should get a response indicating the request was invalid")]
        public void ThenIShouldGetAResponseIndicatingTheRequestWasInvalid()
        {
            var response = PropertyBucket.GetProperty<RestResponse<Dictionary<string, string>>>(POST_CREATE_CONTACT_LIST_KEY);
            var invalidRequestResponse = JsonConvert.DeserializeObject<InvalidRequestResponse>(response.Content);
            Assert.IsTrue(invalidRequestResponse.Message.Equals("The request is invalid."), "No results returned");
        }

        [Then(@"I should get all '(.*)' on proprietary information")]
        public void ThenIShouldGetAllOnProprietaryInformation(bool value)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.IsTrue(response.Data.Items.All(c => c.IsProprietaryContact.Equals(value)),"Not all IsPropietaryContact are False");
        }        

        [Then(@"all items returned should be sorted by '(.*)'")]
        public void ThenAllItemsReturnedShouldBeSortedBy(ContactsService.SortDirection direction)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_SORTED_KEY);
            List<ContactsItem> items = response.Data.Items;
            var listSort = direction == ContactsService.SortDirection.Ascending ?
                items.TakeWhile(c => !c.HasMultipleOutlets).Concat(items.SkipWhile(c => !c.HasMultipleOutlets)) :
                items.TakeWhile(c => c.HasMultipleOutlets).Concat(items.SkipWhile(c => c.HasMultipleOutlets));
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.IsTrue(items.SequenceEqual(listSort), "Not all items are sorted");
        }

        [Then(@"all items returned should have the '(.*)' in their outlet")]
        public void ThenAllItemsReturnedShouldHaveTheInTheirOutlet(string focus)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet type
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.IsTrue(outlets.Data.Items.All(c => c.RegionalFocus.Equals(focus)),
                $"Not all outlet types are '{focus}', results filtered incorrectly");
        }
        [Then(@"I should see the contacts that have the '(.*)' in their outlet information")]
        public void ThenIShouldSeeTheContactsThatHaveTheInTheirOutletInformation(string subject)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            //Must extract outlet from contact response and verify the outlet type
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.That(outlets.Data.Items.Count, Is.GreaterThan(0), "GetOutlets endpoint returned nothing.");
            Assert.IsTrue(outlets.Data.Items.All(c => c.Subjects.Contains(subject)),
                $"Not all outlets have '{subject}', results incorrect");
        }

        [Then(@"all filtered contacts returned should have '(.*)' as their language")]
        public void ThenAllFilteredContactsReturnedShouldHaveAsTheirLanguage(string language)
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var items = response.Data.Items;
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            items.ForEach(i => i.WorkingLanguages.ForEach(l => Assert.That(l.Name, Is.EqualTo(language), $"Not all contacts have '{language}', results incorrect")));         
        }

        [Then(@"I should see the Opted Out property updated")]
        public void ThenIShouldSeeTheOptedOutPropertyUpdated()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(GET_CONTACTS_DETAILS_EDITED);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), "the request was not executed properly");
            Assert.IsTrue(response.Data.IsOptedOut, "the contact was not edited");
        }

        [Then(@"I should see the contacts that have the '(.*)' in their Opted out property")]
        public void ThenIShouldSeeTheContactsThatHaveTheInTheirOptedOutProperty(string p0)
        {
           var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
           Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
           Assert.IsTrue(response.Data.Items.All(c => c.IsOptedOut), "the contacts are not opted out in the results");
        }

        [Then(@"I should contain any results with the '(.*)'")]
        public void ThenIShouldProperResultsForThatTermOfSearch(string term)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.Any(c => c.SortName.ToLower().Contains(term.ToLower())), "the term doesn't appear on results");
        }

        [Then(@"I should see notes on response properly")]
        public void ThenIShouldSeeNotesOnResponseProperly()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<EntityList>>(PATCH_NOTES_KEY);
            var note = PropertyBucket.GetProperty<string>(LIST_NOTE_KEY);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), "the request was not executed properly");
            Assert.That(response.Data.Supplement.Notes,Is.EqualTo(note), "The notes were not saved properly");
        }       

        [Then(@"I should see non null values on Customactivities and EntityId")]
        public void ThenIShouldSeeNonNullValuesOnCustomactivitiesAndEntityId()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<ContactHistory>>>(GET_CONTACTS_DETAILS);
            Assert.That(response.Data.Where(c => c.Category.Equals("Activity")).All(o => !o.EntityId.Equals(null)), "The entity id is null");
            Assert.That(response.Data.Where(c => c.Category.Equals("Activity")).All(o => !o.IsCustomActivity.Equals(null)), "The custom activity content is null");
        }

        [Then(@"I should see the contacts that have '(.*)' in their title")]
        public void ThenIShouldSeeTheContactsThatHaveKeywordInTheirTitle(string keyword)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.Title.ToLower().Contains(keyword.ToLower())), "Keyword does not exist in the title");
        }

        [Then(@"I should see the contacts that have '(.*)' in their email address")]
        public void ThenIShouldSeeTheContactsThatHaveInTheirEmailAddress(string keyword)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "HTTP Request unsuccessful");
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.Email.ToLower().Contains(keyword.ToLower())), string.Join(",", response.Data.Items.Select(i => i.Id + ": " + i.Email)));            
        }

        [Then(@"the results should match with the '(.*)' location")]
        public void ThenTheResultsShouldMatchWithTheLocation(string location)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");            
            //Must extract outlet from contact response and verify the outlet type
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.That(outlets.Data.Items.Count, Is.GreaterThan(0),"GetOutlets endpoint returned nothing.");
            Assert.IsTrue(outlets.Data.Items.All(c => c.Location.Contains(location)),string.Join(",", outlets.Data.Items.Select(i => i.Id + ": " + i.Location)));
        }

        [Then(@"I should see the contacts recent tweets in the response")]
        public void ThenIShouldSeeTheContactsRecentTweetsInTheResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Tweets>>(GET_RECENT_TWEETS_RESPONSE_KEY);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "HTTP request was no successful");
            Assert.That(response.Data.Items.Count(), Is.GreaterThan(0), "No recent tweets returned");
            Assert.That(response.Data.Items.All(c => c.GetType() == typeof(Tweet)),"The recent tweet doesn't have the proper type");
        }

        [Then(@"all Contacts item '(.*)' are sorted '(.*)'")]
        public void ThenAllContactsItemAreSorted(string field, ContactsService.SortDirection sortDirection)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            var items = response.Data.Items;
            var sortedResponse = sortDirection == ContactsService.SortDirection.Ascending ?
                items.OrderBy(c => c.GetType().GetProperty(field).GetValue(c, null)) :
                items.OrderByDescending(c => c.GetType().GetProperty(field).GetValue(c, null));

            Assert.AreEqual(sortedResponse.ToList(), items.ToList(),"The contacts are not sorted as expected");
        }

        [Then(@"I should get an error message")]
        public void ThenIShouldGetAnErrorMessage()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(AddContactSteps.CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Assert.AreEqual(content.Message.Value, "Security error.", "The response did not return a security error message");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Forbidden,"The response status code does not match");
        }


        [Then(@"I should see the contact name in the opt in request response")]
        public void ThenIShouldSeeInTheOptInRequestResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OptinRequest>>(GET_SINGLE_CONTACT);
            var responseContact = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(AddContactSteps.CREATE_PRIVATE_CONTACT_RESPONSE_KEY);
            var sender = PropertyBucket.GetProperty<string>(SENDER_NAME);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "HTTP request was no successful");
            Assert.That(response.Data.EntityName.ToLower().Contains(responseContact.Data.LastName.ToLower()), "The contact was not added to request");
            Assert.That(response.Data.Key, Is.Not.Null, "The key is null");
            Assert.That(response.Data.SenderName.Equals(sender),"The sender is not the same");
        }

        [Then(@"I should see the property already updated")]
        public void ThenIShouldSeeThePropertyAlreadyUpdated()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OptinRequest>>(PATCH_OPTIN);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"),"the request was not executed properly");
            Assert.IsTrue(response.Data.HasOptedIn, "The contact was not already updated");
        }

        [Then(@"The saved searched is deleted")]
        public void ThenTheSavedSearchedIsDeleted()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);          
            var idPost = response.Data.Id;
            new ContactsService(SessionKey).DeleteSavedSearch(idPost);
        }


        [Then(@"I should see the proper '(.*)' on the contacts response")]
        public void ThenIShouldSeeTheProperOnTheContactsResponse(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.That(outlets.Data.Items.Count, Is.GreaterThan(0),"GetOutlets endpoint returned nothing.");
            Assert.IsTrue(outlets.Data.Items.All(c => c.CountyName.Contains(value)),string.Join(",", outlets.Data.Items.Select(i => i.Id + ": " + i.Location)));            
        }

        [Then(@"I should see results with the given contacts names")]
        public void ThenIShouldSeeResultsWithTheGivenContactsNames()
        {
            var ids = PropertyBucket.GetProperty<List<int>>(CONTACT_IDS);
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(CONTACT_KEY_IDS);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");
            var list = response.Data.Items.Select(i => i.Id).ToArray();
            Assert.That(ids.SequenceEqual(list), "No contacts with that name getted");
        }

        [Then(@"I should see results with '(.*)' as their county for contacts results")]
        public void ThenIShouldSeeResultsWithAsTheirCountyForContactsResults(string county)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.That(outlets.Data.Items.Count, Is.GreaterThan(0), "GetOutlets endpoint returned nothing.");            
            Assert.That(outlets.Data.Items.All(o => o.CountyName.ToLower().Equals(county.ToLower())), $"Not all results have {county} , results filtered incorrectly");
        }       

        [Then(@"I should get the '(.*)' status code for the item selected")]
        public void ThenIShouldGetTheStatusCodeForTheItemSelected(int statusCode)
        {
            IRestResponse<DeleteItem> response = PropertyBucket.GetProperty<IRestResponse<DeleteItem>>(RESPONSE_DELETE_KEY);
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(response), "The item was deleted");
        }

        [Then(@"I should see '(.*)' for contact response on target area location")]
        public void ThenIShouldSeeForContactResponseOnTargetAreaLocation(string location)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(GET_CONTACTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var ids = response.Data.Items.Select(i => i.OutletId);
            var outlets = new OutletsService(SessionKey).GetOutlets(ids.ToArray());
            Assert.That(outlets.Data.Items.Count, Is.GreaterThan(0), "GetOutlets endpoint returned nothing.");
            Assert.That(outlets.Data.Items.Any(i => i.City.Contains(location)), Err.Msg(string.Join(",", outlets.Data.Items.Select(i => i.Id + " : " + i.Location))));
        }

        [Then(@"I should see the same results that the original one")]
        public void ThenIShouldSeeTheSameResultsThatTheOriginalOne()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<EntityListFilter>>(LIST_DETAILS_KEY);
            var responseDup = PropertyBucket.GetProperty<IRestResponse<EntityList>>(RESPONSE_DELETE_KEY);
            Assert.That(response.Data.Results.Count, Is.GreaterThan(0), "the list info is empty");
            Assert.That(responseDup.Data.Name, Is.Not.Empty, "the list name is empty");
        }

        [Then(@"The result should be (.*) for the import process")]
        public void ThenTheResultShouldBeOKForTheImportProcess(string status)
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(GET_RESPONSE_KEY);
            Assert.AreEqual(status.Replace("\"", ""), HttpStatusCode.OK.ToString(), "Wrong Status code on the response");
        }
        #endregion
    }
}