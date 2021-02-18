using BoDi;
using CCC_API.Data.Responses.Contact;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Data.Responses.Media.Outlet;
using CCC_API.Services.Common;
using CCC_API.Services.Media;
using CCC_API.Services.Media.Outlet;
using CCC_API.Steps.Common;
using CCC_API.Steps.Media.Contact;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using ZukiniWrap;
using static CCC_API.Services.Media.Outlet.OutletsService;
using Is = NUnit.Framework.Is;
using Zukini;
using CCC_API.Data;
using CCC_API.Data.PostData.Media.Outlet;

namespace CCC_API.Steps.Media.Outlet
{
    [Binding]
    public class OutletsSteps : AuthApiSteps
    {
        public Key GET_OUTLETS_RESPONSE_KEY;
        public Key GET_OUTLET_RESPONSE_KEY;
        public Key GET_SUGGESTION_RESPONSE_KEY;
        public Key POST_RECENT_SEARCH_KEY;
        public Key GET_RECENT_SEARCH_KEY;
        public Key POST_SAVED_SEARCH_KEY;
        public Key GET_SAVED_SEARCH_KEY;
        public Key DELETE_RESPONSE_KEY;
        public Key GET_OUTLETS_REFERRALS_KEY;
        public Key EDIT_SAVED_SEARCH_KEY;
        public Key POST_CREATE_OUTLET_LIST_KEY;
        public Key LIST_NAME_KEY;
        public Key GET_NEW_OUTLETS_RESPONSE_KEY;
        public Key POST_PRIVATE_OUTLET_KEY;
        public Key PRIVATE_OUTLET_ID_KEY;
        public Key PATCH_PRIVATE_OUTLET_KEY;
        public Key POST_DELETE_PRIVATE_OUTLET_KEY;
        public Key GET_OUTLET_DETAILS_KEY;
        public Key GET_OUTLET_EXPORT_RESPONSE_KEY; // also in JobSteps
        public Key POST_ENTITY_LISTS_FILTER_KEY;
        public Key GET_RECENT_TWEETS_RESPONSE_KEY;
        public Key PATCH_NOTES_KEY;
        public Key LIST_NOTE_KEY;
        public Key OUTLET_IDS_RESPONSE_KEY;
        public Key OUTLET_IDS_KEY;
        public Key RESPONSE_DUPLICATED_KEY;
        public Key LIST_DETAILS_KEY;
        public Key RESPONSE_DELETE_KEY;

        public OutletsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"I perform a GET for Outlets by '(.*)' '(.*)'")]
        public void WhenIPerformAGETForOutletsBy(OutletsService.OutletSearchCriteria criteria, string parameter)
        {
            var response = new OutletsService(SessionKey).GetOutlets(criteria, parameter);
            PropertyBucket.Remember(GET_OUTLETS_RESPONSE_KEY, response);
        }
        
        [When(@"I perform a GET for an Outlet by Id using the first contact's outlet id from the previous search")]
        public void WhenIPerformAGetForOutletById()
        {
            IRestResponse<Contacts> response = PropertyBucket.GetProperty<IRestResponse<Contacts>>(ContactSearchSteps.GET_CONTACTS_RESPONSE_KEY);
            var responseOutlet = new OutletsService(SessionKey).GetOutlet(response.Data.Items[0].OutletId);
            PropertyBucket.Remember(GET_OUTLET_RESPONSE_KEY, responseOutlet);
        }

        [When(@"I perform a GET for Outlet suggestion using the key '(.*)'")]
        public void WhenIPerformAGETForOutletSuggestionUsingTheKey(string param){           
            var responseSuggestion = new OutletsService(SessionKey).GetSuggestion(param);
            PropertyBucket.Remember(GET_SUGGESTION_RESPONSE_KEY, responseSuggestion);
        }

        [When(@"I perform a GET for Outlets with the filter '(.*)' in the filter category '(.*)'")]
        public void WhenIPerformAGETForOutletsWithTheFilterInTheFilterCategory(string filter, string category)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            var outletService = new OutletsService(SessionKey);
            IRestResponse<Facets> facets = outletService.GetFacets(response.Data.Key);
            var facetId = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.Text.ToLower().Equals(filter.ToLower())).Id;
            var facetText = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.Text.ToLower().Equals(filter.ToLower())).Text;
            IRestResponse<Outlets> outletResponse = outletService.GetFilterResults(response.Data.Key, facetId.ToString());
            PropertyBucket.Remember(GET_OUTLETS_RESPONSE_KEY, outletResponse, true);
            PropertyBucket.Remember("text", facetText);
        }

        [When(@"I perform a POST to save the outlet search using the key generated before")]
        public void WhenIPerformAPOSTToSaveTheOutletSearchUsingTheKeyGeneratedBefore()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            IRestResponse<MediaSavedSearchItem> responseSearch = new OutletsService(SessionKey).PostRecentSearch(response.Data.Key);
            PropertyBucket.Remember(POST_RECENT_SEARCH_KEY, responseSearch);
        }

        [When(@"I perform a get for recent outlet searches endpoint")]
        public void WhenIPerformAGetForRecentOutletSearchesEndpoint()
        {            
            IRestResponse<MediaSavedSearch> responseSavedSearch = new OutletsService(SessionKey).GetRecentSearches();
            PropertyBucket.Remember(GET_RECENT_SEARCH_KEY, responseSavedSearch);
        }
        
        [When(@"I perform a POST to keep the outlet search using the key generated before with a random name")]
        public void WhenIPerformAPOSTToKeepTheOutletSearchUsingTheKeyGeneratedBeforeWithARandomName()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            var name = StringUtils.RandomAlphaNumericString(8);
            IRestResponse<MediaSavedSearchItem> responseSearch = new OutletsService(SessionKey).PostSaveSearch(response.Data.Key, name);
            PropertyBucket.Remember(POST_SAVED_SEARCH_KEY, responseSearch);
        }


        [When(@"I perform a get for saved outlet searches endpoint")]
        public void WhenIPerformAGetForSavedOutletSearchesEndpoint()
        {            
            IRestResponse<MediaSavedSearch> responseSavedSearch = new OutletsService(SessionKey).GetSavedSearches();
            PropertyBucket.Remember(GET_SAVED_SEARCH_KEY, responseSavedSearch);
        }

        [When(@"I perform a DELETE for the outlet saved search created")]
        public void WhenIPerformADELETEForTheOutletSavedSearchCreated()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);
            var id = response.Data.Id;
            var responseDeleted = new OutletsService(SessionKey).DeleteSavedSearch(id);
            PropertyBucket.Remember(DELETE_RESPONSE_KEY, responseDeleted);
        }

        [When(@"I edit the name of the first outlet saved search listed")]
        public void WhenIEditTheNameOfTheFirstOutletSavedSearchListed()
        {
            var newName = StringUtils.RandomAlphaNumericString(10);
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_SAVED_SEARCH_KEY);
            var id = response.Data.Items.FirstOrError().Id;
            var responsePatch = new OutletsService(SessionKey).EditSavedSearch(id, newName);
            PropertyBucket.Remember(EDIT_SAVED_SEARCH_KEY, responsePatch);
        }

        [When(@"I perform a Post for creating an outlet list with a random name using the top three ids")]
        public void WhenIPerformAPostForCreatingAnOutletListWithARandomNameUsingTheTopThreeIds()
        {
            var newName = StringUtils.RandomAlphaNumericString(8);
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            string key = response.Data.Key;
            int[] ids = new int[3];
            for (int i = 0; i < 3; i++)
            {
                ids[i] = response.Data.Items[i].Id;
            }           
            IRestResponse<Lists> outletListResponse = new OutletsService(SessionKey).PostOutletLists(key, ids, newName);
            PropertyBucket.Remember(POST_CREATE_OUTLET_LIST_KEY, outletListResponse, true);
            PropertyBucket.Remember(LIST_NAME_KEY, newName);
        }

        [When(@"I perform a GET for Outlets by '(.*)' with the list created before")]
        public void WhenIPerformAGETForOutletsByWithTheListCreatedBefore(OutletsService.OutletSearchCriteria criteria)
        {
            string parameter = PropertyBucket.GetProperty<string>(LIST_NAME_KEY);
            var response = new OutletsService(SessionKey).GetOutlets(criteria, parameter);
            PropertyBucket.Remember(GET_NEW_OUTLETS_RESPONSE_KEY, response);
        }

        [When(@"I perform a POST to create a new outlet using a random name and the '(.*)'")]
        public void WhenIPerformAPOSTToCreateANewOutletUsingARandomNameAndThe(int id)
        {
            string randomName = StringUtils.RandomAlphaNumericString(8);
            string randomMail = StringUtils.RandomEmail(8);
            var response = new OutletsService(SessionKey).PostPrivateOutlet(randomName, id, randomMail);
            PropertyBucket.Remember(POST_PRIVATE_OUTLET_KEY, response);
        }


        [When(@"I perform a POST to export Outlets including '(.*)' field")]
        public void WhenIPerformAPOSTToExportOutletsIncludingField(string field)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);               
            var postResponse = new OutletsService(SessionKey).ExportOutlets(response.Data.Key,field);
            PropertyBucket.Remember(GET_OUTLET_EXPORT_RESPONSE_KEY, postResponse);
        }

        [When(@"I save the fist id of the response getted before")]
        public void WhenISaveTheFistIdOfTheResponseGettedBefore()
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            var responseDetail = new OutletsService(SessionKey).GetOutlet(response.Data.Items[0].Id);
            PropertyBucket.Remember(GET_OUTLET_DETAILS_KEY, responseDetail);
        }

        [When(@"I perform a post to get all '(.*)' lists")]
        public void WhenIPerformAPostToGetAllListsTakingTheFirstId(string mediaType)
        {
            var response = new OutletsService(SessionKey).PostEntityListsFilter(mediaType);
            PropertyBucket.Remember(POST_ENTITY_LISTS_FILTER_KEY, response);
        }

        [When(@"I perform a PATCH to add a list for that outlet taking the first id and name for the list")]
        public void WhenIPerformAPATCHToAddAListForThatOutlet()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY); 
            var responseList = PropertyBucket.GetProperty<IRestResponse<EntityListFilter>>(POST_ENTITY_LISTS_FILTER_KEY);
            var listData = "[{'ListId':" + responseList.Data.Results[0].Id + ",'ListName': '" + responseList.Data.Results[0].Name + "'}]";          
            var responseEdited = new OutletsService(SessionKey).EditPrivateOutlet(response.Data.Id, "/Lists", listData);
            PropertyBucket.Remember(PATCH_PRIVATE_OUTLET_KEY, responseEdited);
            PropertyBucket.Remember(LIST_NAME_KEY, responseList.Data.Results[0].Name);
        }

        [When(@"I delete the private  outlet created previously")]
        public void WhenIDeleteThePrivateOutletCreatedPrevoiosly()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY);
            IRestResponse<DeleteItem> responseDelete = new OutletsService(SessionKey).DeletePrivateOutlet(response.Data.Id);
            PropertyBucket.Remember(POST_DELETE_PRIVATE_OUTLET_KEY, responseDelete);
            //save the outlet detail to verify if it was deleted properly
            IRestResponse<OutletsItem> outletDetail = new OutletsService(SessionKey).GetOutlet(response.Data.Id);
            PropertyBucket.Remember(GET_OUTLET_DETAILS_KEY, outletDetail);
        }

        [When(@"I perform a GET for Outlets '(.*)' between '(.*)' and '(.*)' values")]
        public void WhenIPerformAGETForOutletsBetweenValues(OutletSearchCriteria criteria, int start, int end)
        {
            var response = new OutletsService(SessionKey).GetOutletsReach(criteria, start, end);
            PropertyBucket.Remember(GET_OUTLETS_RESPONSE_KEY, response);
        }

        [When(@"I perform a PATCH to edit the name of the outlet created")]
        public void WhenIPerformAPATCHToEditTheNameOfTheOutletCreated()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY);
            int id = response.Data.Id;
            string nameEdited = StringUtils.RandomAlphaNumericString(8);
            var responsePatch = new OutletsService(SessionKey).EditPrivateOutlet(id, "/FullName", nameEdited);
            PropertyBucket.Remember(PATCH_PRIVATE_OUTLET_KEY, responsePatch);
        }

        [When(@"I perform a GET for the recent twitter endpoint")]
        public void WhenIPerformAGETForTheRecentTwitterEndpoint()
        {
            var outletDetails = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(GET_OUTLET_DETAILS_KEY);
            var response = new OutletsService(SessionKey).GetRecentOutletTweets(outletDetails.Data.Id);
            PropertyBucket.Remember(GET_RECENT_TWEETS_RESPONSE_KEY, response);
        }

        [When(@"I perform a Patch for the outlet list created before adding some ramdom notes")]
        public void WhenIPerformAPatchForTheOutletListCreatedBeforeAddingSomeRamdomNotes()
        {
            var note = StringUtils.RandomAlphaNumericString(6);
            var response = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_OUTLET_LIST_KEY);
            var patchNotesResponse = new EntityListService(SessionKey).PatchNotes(response.Data.Id, note);
            PropertyBucket.Remember(PATCH_NOTES_KEY, patchNotesResponse);
            PropertyBucket.Remember(LIST_NOTE_KEY, note);
        }

        [When(@"I perform a second GET for Outlets by '(.*)' '(.*)'")]
        public void WhenIPerformASecondGETForOutletsBy(OutletSearchCriteria criteria, string parameter)
        {
            var response = new OutletsService(SessionKey).GetOutlets(criteria, parameter);
            PropertyBucket.Remember(GET_NEW_OUTLETS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for Outlets by the following criteria:")]
        public void WhenIPerformAGETForOutletsByTheFollowingCriteria(Table criteriaTable)
        {
            var criteria = new Dictionary<OutletSearchCriteria, string>();

            criteriaTable.Rows.ToList().ForEach(r =>
            {
                criteria.Add(r["criteriaName"].ParseEnum<OutletSearchCriteria>(), r["criteriaValue"]);
            });

            var response = new OutletsService(SessionKey).GetOutletsMultipleCriteria(criteria);
            PropertyBucket.Remember(GET_OUTLETS_RESPONSE_KEY, response);            
        }


        [When(@"I duplicate the oulet list for the same datagroup changing the name")]
        public void WhenIDuplicateTheOuletListForTheSameDatagroupChangingTheName()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Lists>>(POST_CREATE_OUTLET_LIST_KEY);
            var DataGroupId = new AccountInfoService(SessionKey).GetDataGroups().Items.Where(i => i.Name.Contains("Default")).FirstOrError().Id;
            var newName = StringUtils.RandomAlphaNumericString(8);
            var duplicateResponse = new OutletsService(SessionKey).DuplicateList(response.Data.Id, DataGroupId, newName, "MediaOutlet");
            PropertyBucket.Remember(RESPONSE_DUPLICATED_KEY, duplicateResponse);
        }

        [When(@"I retrieve the outlet list information recently duplicated")]
        public void WhenIRetrieveTheOutletListInformationRecentlyDuplicated()
        {
            var listDetails = new OutletsService(SessionKey).PostEntityListsFilterBySort("MediaOutlet", "CreationDate", 1);
            PropertyBucket.Remember(LIST_DETAILS_KEY, listDetails);
        }
        [When(@"I perform a PATCH for opted out the private outlet created setting as '(.*)'")]
        public void WhenIPerformAPATCHForOptedOutThePrivateOutletCreatedSettingAs(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY);
            var patch = new List<PatchData>();
            patch.Add(new PatchData("update", "IsOptedOut", value));
            var responseEdited = new OutletsService(SessionKey).EditOutlet(response.Data.Id, patch);
            PropertyBucket.Remember(GET_OUTLET_DETAILS_KEY, responseEdited);
        }     

        [Then(@"all returned outlet objects should contain '(.*)' in their name")]
        public void ThenAllReturnedOutletObjectsShouldContainInTheirName(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.IsTrue(i.FullName.ToLower().Contains(name.ToLower()), $"Not all results contain '{name}'; Found Outlet Name: '{i.FullName}'"));
        }

        [Then(@"all returned outlet objects should have '(.*)' as their city")]
        public void ThenAllReturnedOutletObjectsShouldHaveAsTheirCity(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.AreEqual(i.City.ToLower(), name.ToLower(), $"Not all results have '{name}' as their city; Found Outlet Name: '{i.FullName}' Outlet City: {i.City}"));
        }

        [Then(@"all returned outlet objects should have '(.*)' as their state")]
        public void ThenAllReturnedOutletObjectsShouldHaveAsTheirState(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.AreEqual(i.State.ToLower(), name.ToLower(), $"Not all results have '{name}' as their state; Found Outlet Name: '{i.FullName}' Outlet State: {i.State}"));
        }

        [Then(@"all returned outlet objects should have '(.*)' as their country")]
        public void ThenAllReturnedOutletObjectsShouldHaveAsTheirCountry(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.AreEqual(i.CountryName.ToLower(), name.ToLower(), $"Not all results have '{name}' as their country; Found Outlet Name: '{i.FullName}' Outlet Country: {i.CountryName}"));
        }

        [Then(@"all returned outlet objects should have '(.*)' as their outlet type")]
        public void ThenAllReturnedOutletObjectsShouldHaveAsTheirOutletType(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.AreEqual(i.TypeName.ToLower(), name.ToLower(), $"Not all results have '{name}' as their outlet type; Found Outlet Name: '{i.FullName}' Outlet Type: {i.TypeName}"));
        }

        [Then(@"all returned outlet objects should contain '(.*)' in their subjects")]
        public void ThenAllReturnedOutletObjectsShouldContainInTheirSubjects(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.IsTrue(i.Subjects.Select(s => s.ToLower()).Contains(name.ToLower()), $"Not all results have '{name}' in their subjects; Found Outlet Name: '{i.FullName}' Outlet Subjects: {i.Subjects}"));
        }

        [Then(@"all returned outlet objects should have '(.*)' as their DMA")]
        public void ThenAllReturnedOutletObjectsShouldHaveAsTheirDMA(string name)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            List<OutletsItem> items = response.Data.Items;
            items.ForEach(i => Assert.AreEqual(i.DMAName.ToLower(), name.ToLower(), $"Not all results have '{name}' as their DMA; Found Outlet Name: '{i.FullName}' Outlet DMA: {i.DMAName}"));
        }
        
        [Then(@"the returned outlet object should have '(.*)' as their city")]
        public void ThenTheReturnedOutletObjectShouldHaveAsTheirCity(string name)
        {
            IRestResponse<OutletsItem> response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(GET_OUTLET_RESPONSE_KEY);
            OutletsItem outletsItem = response.Data;
            Assert.AreEqual(outletsItem.City.ToLower(), name.ToLower(), $"Returned outlet does not have '{name}' as their city; Found Outlet Name: '{outletsItem.FullName}' Outlet City: {outletsItem.City}");
        }

        [Then(@"all returned outlet objects should have a UVPM value equal to or greater than zero")]
        public void ThenAllReturnedOutletObjectsShouldHaveAUVPMValueEqualToOrGreaterThanZero()
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No outlets returned by search");
            Assert.True(response.Data.Items.TrueForAll(o => o.UniqueVisitorsPerMonth >= 0), "Not all UVPM values are greater than or equal to zero");
        }

        [Then(@"all returned outlet objects should have a Medium value")]
        public void ThenAllReturnedOutletObjectsShouldHaveAMediumValue()
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No outlets returned by search");
            Assert.False(response.Data.Items.Any(i => string.IsNullOrWhiteSpace(i.Medium)), "Not all Outlets have a Medium value");
        }

        [Then(@"I should see '(.*)' in outlet name as a suggestion")]
        public void ThenIShouldSeeInOutletNameAsASuggestion(string outlet)
        {
            IRestResponse<OutletSuggestion> response = PropertyBucket.GetProperty<IRestResponse<OutletSuggestion>>(GET_SUGGESTION_RESPONSE_KEY);
            List<OutletChildren> child = response.Data.Suggestions;
            Assert.That(child.Count(), Is.GreaterThan(0), "No outlets returned by search");
            child.ForEach(i => Assert.IsTrue(i.Name.ToLower().Contains(outlet.ToLower()), $"The suggested children have not {outlet} in their name"));                  
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their Outlet Type")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirOutletType(string value)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.TypeName.ToLower().Equals(value.ToLower())), $"Not all outlets contain '{value}' as a Outlet Type");            
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their Outlet DMA Name")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirOutletDMAName(string value)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.DMAName.ToLower().Equals(value.ToLower())), $"Not all outlets contain '{value}' as a DMA Name");
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their subject")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirSubject(string value)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.Subjects.Contains(value)), $"Not all outlets contain '{value}' as a Subject");
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their state")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirState(string value)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.State.ToLower().Equals(value.ToLower())), $"Not all contacts contain '{value}' as a state");
        }

        [Then(@"all returned outlets have a non-null Regional Focus")]
        public void ThenAllReturnedOutletsHaveANon_NullRegionalFocus()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned for search");
            Assert.IsFalse(response.Data.Items.Any(o => o.RegionalFocus == null), "Expected all regional focus fields to be non-null");
        }

        [Then(@"the outlet saved search should be deleted properly")]
        public void ThenTheOutletSavedSearchShouldBeDeletedProperly()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_SAVED_SEARCH_KEY);
            var responseGet = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearch>>(GET_SAVED_SEARCH_KEY);
            var id = response.Data.Id;
            Assert.IsFalse(responseGet.Data.Items.Any(i => i.Id == id), "The saved search was not deleted");
        }

        [When(@"I perform a GET for referrals using '(.*)' as limit and '(.*)' as source")]
        public void WhenIPerformAGETForReferralsUsingAsLimitAndAsSource(int limit, string source)
        {
            var outletService = new OutletsService(SessionKey);
            var response = new OutletsService(SessionKey).GetReferrals(limit, source);
            PropertyBucket.Remember(GET_OUTLETS_REFERRALS_KEY, response);
        }

        [When(@"I perform a GET for outlets ids for the following outlets '(.*)'")]
        public void WhenIPerformAGETForOutletsIdsForTheFollowingOutlets(string outlets)
        {
            string[] outlet = outlets.Split(new string[] { "," }, StringSplitOptions.None);
            int id;
            List<int> ids = new List<int>();
            foreach (var item in outlet)
            {

                var outletJson = TestData.DeserializedJson<List<OutletsItem>>("Outlets.json", Assembly.GetExecutingAssembly());
                try
                {
                    id = outletJson.Find(c => c.FullName.ToLower().Equals(item.ToLower())).Id;
                    ids.Add(id);
                }
                catch (NullReferenceException)
                {
                    throw new ArgumentNullException(Err.Msg($"'{item}' not found in Outlets.json file."));
                }
            }            
            var response = new OutletsService(SessionKey).GetOutletsUsingIds(ids);
            PropertyBucket.Remember(OUTLET_IDS_RESPONSE_KEY, response);
            PropertyBucket.Remember(OUTLET_IDS_KEY, ids);       
        }

        [When(@"I perform a GET for Outlets with the group '(.*)' in the filter category '(.*)'")]
        public void WhenIPerformAGETForOutletsWithTheGroupInTheFilterCategory(string group, string category)
        {
            IRestResponse<Outlets> response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            var outletService = new OutletsService(SessionKey);
            IRestResponse<Facets> facets = outletService.GetFacets(response.Data.Key);
            var facetId = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.CategoryText.ToLower().Equals(group.ToLower())).Id;
            var facetText = facets.Data.Available.Find(f => f.Category.ToLower().Equals(category.ToLower())
            && f.CategoryText.ToLower().Equals(group.ToLower())).Text;
            IRestResponse<Outlets> outletsResponse = outletService.GetFilterResults(response.Data.Key, facetId.ToString());
            PropertyBucket.Remember(GET_OUTLETS_RESPONSE_KEY, outletsResponse, true);
            PropertyBucket.Remember("text", facetText);
        }

        [When(@"I perform a GET for the first outlet list listed")]
        public void WhenIPerformAGETForTheFirstOutletListListed()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            int id = response.Data.Items.Select(i => i.Id).FirstOrError();
            IRestResponse<OutletsItem> responseDetail = new OutletsService(SessionKey).GetOutlet(id);
            PropertyBucket.Remember(GET_OUTLET_DETAILS_KEY, responseDetail);
        }



        [Then(@"the enpoint returns a list of referrals from '(.*)' with new metrics")]
        public void ThenTheEnpointReturnsAListOfReferralsFromWithNewMetrics(string source)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Referrals>>(GET_OUTLETS_REFERRALS_KEY);

            if (response.Data.ItemCount == 0) Assert.Ignore("There are not items to verify");

            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No referrals returned by " + source);
            Assert.That(response.Data.Items.All(i => i.MediaOutletId != 0), $"Not all Media Outlet are valid");
            Assert.That(response.Data.Items.All(i => i.Conversions >= 0), $"Not all Conversions are valid");
            Assert.That(response.Data.Items.All(i => i.AverageSecondsOnPage >= 0), $"Not all AverageSecondsOnPage are valid");
            Assert.That(response.Data.Items.All(i => i.UniqueVisitors >= 0), $"Not all Visitors are valid");
            Assert.That(response.Data.Items.All(i => i.PageViews >= 0), $"Not all page views are valid");
            Assert.That(response.Data.Items.All(i => i.TotalValue >= 0), $"Not all Total Values are valid");
            Assert.That(response.Data.Items.All(i => i.AverageSessionSeconds >= 0), $"Not all sessions are valid");
            Assert.That(response.Data.Items.All(i => i.Checkouts >= 0), $"Not all Checkouts are valid");
            Assert.That(response.Data.Items.All(i => i.OrderRevenue >= 0), $"Not all OrderRevenue are valid");
            Assert.That(response.Data.Items.All(i => i.Orders >= 0), $"Not all Orders are valid");
            Assert.That(response.Data.Items.All(i => i.Bounces >= 0), $"Not all bounces are valid");
            Assert.That(response.Data.Items.All(i => i.NewUsers >= 0), $"Not all new users are valid");
        }

        [Then(@"I should see the name edited of the outlet saved search")]
        public void ThenIShouldSeeTheNameEditedOfTheOutletSavedSearch()
        {
            var responseGet = new OutletsService(SessionKey).GetSavedSearches();
            Assert.That(responseGet.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            var response = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(EDIT_SAVED_SEARCH_KEY);
            var name = response.Data.Name;
            Assert.IsTrue(responseGet.Data.Items.Any(i => i.Name.Equals(name)), "The name of saved search was not edited");
        }

        [Then(@"I should see the outlets returned properly")]
        public void ThenIShouldSeeTheOutletsReturnedProperly()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_NEW_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned for search");
            Assert.That(response.Data.Items.All(o => o.FullName.ToLower().Contains("chicago")), "The results are not proper");
        }

        [Then(@"I should contain any outlets results with the '(.*)'")]
        public void ThenIShouldContainAnyOutletsResultsWithThe(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned for search");
            Assert.IsTrue(response.Data.Items.Any(o => o.FullName.Contains(value)), "The search resultes were not the proper");
        }

        [Then(@"all items returned should have the '(.*)' in their Regional Focus field")]
        public void ThenAllItemsReturnedShouldHaveTheInTheirRegionalFocusField(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned for search");
            Assert.IsTrue(response.Data.Items.All(o => o.RegionalFocus.Equals(value)), "The results does not contain the proper value");
        }      

        [Then(@"I should see the results and the '(.*)' in the recent searches")]
        public void ThenIShouldSeeTheResultsAndTheInTheRecentSearches(string id)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned for search"));
            var responsePost = PropertyBucket.GetProperty<IRestResponse<MediaSavedSearchItem>>(POST_RECENT_SEARCH_KEY);
            Assert.That(responsePost.Data.Query.WorkingLanguageIds.Equals(id), Err.Line("The data for the search was not saved"));
        }

        [Then(@"I should see the '(.*)' for frecuency property on outlets response")]
        public void ThenIShouldSeeTheForFrecuencyPropertyOnOutletsResponse(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned for search");
            Assert.That(response.Data.Items.All(o => o.Frequency.Equals(value)), "The search does not return proper results");
        }

        [Then(@"I should see the outlet created with the proper country '(.*)'")]
        public void ThenIShouldSeeTheOutletCreatedWithTheProperCountry(string country)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), Err.Line("The request was not executed properly"));
            Assert.That(response.Data.CountryName.Equals(country), Err.Line("The country was not added to the new private outlet"));
        }

        [Then(@"I should see the private outlet with the data edited")]
        public void ThenIShouldSeeThePrivateOutletWithTheDataEdited()
        {
            var oldName = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(PATCH_PRIVATE_OUTLET_KEY);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), Err.Line("The request was not executed properly"));
            Assert.That(response.Data.FullName, Is.Not.EqualTo(oldName.Data.FullName), Err.Line("The name was not editd"));
        }        
        [Then(@"I should see results returned between '(.*)' and '(.*)' values on the range")]
        public void ThenIShouldSeeResultsReturnedBetweenAndValuesOnTheRange(int start, int end)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned for search");
            var criteria = response.ResponseUri.Query;
            if (criteria.Contains("UniqueVisitorsPerMonth"))
            {
                Assert.That(response.Data.Items.All(o => o.UniqueVisitorsPerMonth >= start && o.UniqueVisitorsPerMonth <= end), Err.Msg("The values are out of the range"));
            }
            else
            {
                Assert.That(response.Data.Items.All(o => o.CirculationAudience >= start && o.CirculationAudience <= end), Err.Msg("The values are out of the range"));
            }
        }

        [Then(@"I should get the '(.*)' status code returned")]
        public void ThenIShouldGetTheStatusCodeReturned(int statusCode)
        {
            IRestResponse<DeleteItem> response = PropertyBucket.GetProperty<IRestResponse<DeleteItem>>(POST_DELETE_PRIVATE_OUTLET_KEY);
            IRestResponse<OutletsItem> outletDetail = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(GET_OUTLET_DETAILS_KEY);
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(response),Err.Line( "The delete process failed"));
            Assert.AreEqual(404, Services.BaseApiService.GetNumericStatusCode(outletDetail), Err.Line("The contact was not deleted"));
        }

        [Then(@"all results returned should have '(.*)' in ther IsProprietaryOutlet value")]
        public void ThenAllResultsReturnedShouldHavaInTherIsProprietaryOutletValue(bool Isproprietary)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0),Err.Msg( "No results returned for search"));
            Assert.That(response.Data.Items.All(o => o.IsProprietaryOutlet.Equals(Isproprietary)),Err.Line("The search does not return proper results"));
        }

        [Then(@"I should see the list added properly on response")]
        public void ThenIShouldSeeTheListAddedProperlyOnResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(PATCH_PRIVATE_OUTLET_KEY);
            var listname = PropertyBucket.GetProperty<string>(LIST_NAME_KEY);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), "The request was not executed properly");
            Assert.That(response.Data.Lists.Any(i => i.ListName.Contains(listname)), "The name was not edited");           
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their proprietary data")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirProprietaryData(bool recordType)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.IsTrue(response.Data.Items.All(c => c.IsProprietaryOutlet.Equals(recordType)),
               Err.Line($"Not all IsPropietaryContact are False, results filtered incorrectly"));
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their Outlet city")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirOutletCity(string city)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.That(response.Data.Items.All(o => o.City.Equals(city)), Err.Line($"Not all results have {city} , results filtered incorrectly"));
        }

        [Then(@"I should see proper data on response")]
        public void ThenIShouldSeeProperDataOnResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Tweets>>(GET_RECENT_TWEETS_RESPONSE_KEY);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), Err.Line("The request was not executed properly"));
            Assert.That(response.Data.Items.All(o => o.Type.Equals("tweet")),Err.Line("The recent tweets has no the proper type"));
        }

        [Then(@"all filtered outlets returned should have '(.*)' as their outlet language")]
        public void ThenAllFilteredOutletsReturnedShouldHaveAsTheirOutletLanguage(string language)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.That(response.Data.Items.All(o => o.WorkingLanguages.All(l => l.Name.Equals(language))), Err.Line($"Not all results have {language} , results filtered incorrectly"));            
        }

        [Then(@"I should see notes for outlet list on response properly")]
        public void ThenIShouldSeeNotesForOutletListOnResponseProperly()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<EntityList>>(PATCH_NOTES_KEY);
            var note = PropertyBucket.GetProperty<string>(LIST_NOTE_KEY);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), Err.Line("The request was not executed properly"));
            Assert.That(response.Data.Supplement.Notes, Is.EqualTo(note), Err.Line("The notes were not saved properly"));
        }

        [Then(@"I should get the same numbers of results")]
        public void ThenIShouldGetTheSameNumbersOfResults()
        {
            var firstResponse = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            var secondResponse = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_NEW_OUTLETS_RESPONSE_KEY);            
            Assert.That(firstResponse.Data.ItemCount, Is.GreaterThan(0), Err.Msg("The results are empty"));
            Assert.That(secondResponse.Data.ItemCount, Is.GreaterThan(0), Err.Msg("The results are empty"));
            Assert.That(firstResponse.Data.ItemCount, Is.EqualTo(secondResponse.Data.ItemCount), Err.Line("The results are not the same"));
        }

        [Then(@"I should see the outlets with non affiliated media")]
        public void ThenIShouldSeeTheOutletsWithNonAffiliatedMedia()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.False(response.Data.Items.All(o => o.IsAffiliatedMediaOutlet), Err.Msg(string.Join(",",response.Data.Items.Select(i => i.Id+": "+i.IsAffiliatedMediaOutlet))));
        }

        [Then(@"I should see any NOD only outlet returned on response")]
        public void ThenIShouldSeeAnyNODOnlyOutletReturnedOnResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.True(response.Data.Items.Any(i => i.IsNODOutlet), Err.Msg("Incorrect results returned"));
        }

        [Then(@"the results should match with the '(.*)' location on outlets response")]
        public void ThenTheResultsShouldMatchWithTheLocationOnOutletsResponse(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), Err.Msg("No results returned"));           
            Assert.That(response.Data.Items.All(o => o.Location.ToLower().Contains(value.ToLower())), Err.Msg(string.Join(",", response.Data.Items.Select(i => i.Id + " : " + i.Location))));
        }     

        [Then(@"all returned Outlets should have '(.*)' in their fullname and the false in the proprietary")]
        public void ThenAllReturnedOutletsShouldHaveInTheirFullnameAndTheInTheProprietary(string value1)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.That(response.Data.Items.All(i => i.SortName.ToLower().Contains(value1.ToLower())), Err.Msg(string.Join(",", response.Data.Items.Select(i => i.Id + " : " + i.SortName))));
            Assert.IsFalse(response.Data.Items.All(i => i.IsProprietaryOutlet), Err.Msg("The record is private"));
        }

        [Then(@"I should see the proper '(.*)' on the response")]
        public void ThenIShouldSeeTheProperOnTheResponse(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(i => i.CountyName.Equals(value)), "The county name is not the same");
        }

        [Then(@"I should see results with the given names")]
        public void ThenIShouldSeeResultsWithTheGivenNames()
        {
            var ids = PropertyBucket.GetProperty<List<int>>(OUTLET_IDS_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(OUTLET_IDS_RESPONSE_KEY);
            Assert.That(response.Data.Items.Count, Is.GreaterThan(0), "No results returned");
            var list = response.Data.Items.Select(i => i.Id);
            Assert.That(list.SequenceEqual(ids), "No outlets with that name getted");
        }

        [Then(@"I should see results with '(.*)' as their county")]
        public void ThenIShouldSeeResultsWithAsTheirCounty(string county)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.All(o => o.CountyName.ToLower().Equals(county.ToLower())), $"Not all results have {county} , results filtered incorrectly");
        }

        [Then(@"I should see '(.*)' as target area location")]
        public void ThenIShouldSeeAsTargetAreaLocation(string location)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.That(response.Data.Items.Any(i => i.City.Contains(location)), Err.Msg(string.Join(",", response.Data.Items.Select(i => i.Id + " : " + i.Location))));

        }

        [Then(@"I should see the same results that the original one on outlets lists")]
        public void ThenIShouldSeeTheSameResultsThatTheOriginalOneOnOutletsLists()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<EntityListFilter>>(LIST_DETAILS_KEY);
            var responseDup = PropertyBucket.GetProperty<IRestResponse<EntityList>>(RESPONSE_DUPLICATED_KEY);
            Assert.That(response.Data.Results.Count, Is.GreaterThan(0), "the list info is empty");
            Assert.That(responseDup.Data.Name, Is.Not.Empty, "the list name is empty");
        }

        [Then(@"I should see outlets results with the '(.*)' value")]
        public void ThenIShouldSeeOutletsResultsWithTheValue(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Outlets>>(GET_OUTLETS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned");
            Assert.IsTrue(response.Data.Items.All(i => i.IsOptedOut.ToString().Equals(value)), "No opted out {value} values returned");
        }

        [Then(@"I should see the '(.*)' value on outlet edited")]
        public void ThenIShouldSeeTheValueOnOutletEdited(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(GET_OUTLET_DETAILS_KEY);
            Assert.That(response.Data.IsOptedOut.ToString().ToLower().Equals(value.ToLower()), $"No opted out {value} values returned");
        }

        [Then(@"the outlet is deleted")]
        public void ThenTheOutletIsDeleted()
        {
            IRestResponse<OutletsItem> createResponse = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(POST_PRIVATE_OUTLET_KEY);
            int id = createResponse.Data.Id;
            var response = new OutletsService(SessionKey).DeletePrivateOutlet(id);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("OK"), $"The private outlet with {id} was not deleted");
        }

        [Then(@"I should get the list created in the outlet details response")]
        public void ThenIShouldGetTheListCreatedInTheOutletDetailsResponse()
        {
            IRestResponse<OutletsItem> response = PropertyBucket.GetProperty<IRestResponse<OutletsItem>>(GET_OUTLET_DETAILS_KEY);
            var text = PropertyBucket.GetProperty<string>("text");
            Assert.That(response.Data.Lists.Any(i => i.ListName == text), "The filter was not applied properly");
        }

    }
}
