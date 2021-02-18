using BoDi;
using CCC_API.Steps.Common;
using CCC_API.Data.Responses.News;
using RestSharp;
using CCC_API.Utils.Assertion;
using TechTalk.SpecFlow;
using CCC_API.Services.News;
using System.Linq;
using CCC_Infrastructure.Utils;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.News
{
    public class SavedSearchEndpointSteps : AuthApiSteps
    {
        private SavedSearchesService _savedSearchesService;
        private NewsViewService _newsViewService;
        private const string GET_SAVED_SEARCH_RESPONSE_KEY = "GetSavedSearchResponse";
        private const string POST_SAVED_SEARCH_RESPONSE_KEY = "PostSavedSearchResponse";
        private const string SAVED_SEARCH_NAME = "SavedSearchName";
        private const string GET_SEARCH_RESPONSE_KEY = "GetSearchResponseKey";
        private const string GET_SAVED_SEARCH_CRITERIA_RESPONSE = "GetSavedSearchCriteriaResponse";
        private const string PATCHED_SAVED_SEARCH_CRITERIA_RESPONSE = "PatchedSavedSearchCriteriaResponse";

        public SavedSearchEndpointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _savedSearchesService = new SavedSearchesService(SessionKey);
            _newsViewService = new NewsViewService(SessionKey);
        }

        #region When Steps
        [When(@"I perform a POST to Saved Searches endpoint with name '(.*)'")]
        public void WhenIPostToSavedSearchesEndpointWithGivenName(string name)
        {
            var response = _newsViewService.GetAllNews();
            var savedSearchesResponse = _savedSearchesService.PostSavedSearch(response.Data.Key, name);
            PropertyBucket.Remember(POST_SAVED_SEARCH_RESPONSE_KEY, savedSearchesResponse);
        }

        [When(@"I get all saved searches")]
        public void WhenIGetAllSavedSearches()
        {
            var response = _savedSearchesService.GetAllSavedSearches();
            PropertyBucket.Remember(GET_SAVED_SEARCH_RESPONSE_KEY, response);
        }

        [When(@"I search for a single saved search")]
        public void WhenISearchForASingleSavedSearch()
        {
            var savedSearches = PropertyBucket.GetProperty<SavedSearches>(GET_SAVED_SEARCH_RESPONSE_KEY);
            var search = savedSearches.Items.FirstOrError();
            var searchId = search.Id;
            var searchName = search.Name;
            var response = _savedSearchesService.GetSingleSavedSearch(searchId);
            PropertyBucket.Remember(GET_SAVED_SEARCH_RESPONSE_KEY, response, true);
            PropertyBucket.Remember(SAVED_SEARCH_NAME, searchName);
        }

        [When(@"I search for news by all news details criteria")]
        public void WhenISearchForNewsByAllNewsDetailsCriteria()
        {
            var response = _newsViewService.GetNewsByAllNewsDetails();
            PropertyBucket.Remember(GET_SEARCH_RESPONSE_KEY, response);
        }

        [When(@"I save the search with name '(.*)'")]
        public void WhenISaveTheSearchWithName(string name)
        {
            var responseKey = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_SEARCH_RESPONSE_KEY).Data.Key;
            var savedSearchResponse = _savedSearchesService.PostSavedSearch(responseKey, name);
            PropertyBucket.Remember(POST_SAVED_SEARCH_RESPONSE_KEY, savedSearchResponse);
        }

        [When(@"I perform a GET for recently created Saved Search")]
        public void WhenIPerformAGETForRecentlyCreatedSavedSearch()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(POST_SAVED_SEARCH_RESPONSE_KEY);
            var savedSearch = _savedSearchesService.GetSingleSavedSearch(response.Data.Item.Id);
            PropertyBucket.Remember(GET_SAVED_SEARCH_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for Saved Search criteria")]
        public void WhenIPerformAGETForSavedSearchCriteria()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(POST_SAVED_SEARCH_RESPONSE_KEY);
            var savedSearch = _savedSearchesService.GetSingleSavedSearchCriteria(response.Data.Item.Id);
            PropertyBucket.Remember(GET_SAVED_SEARCH_CRITERIA_RESPONSE, savedSearch);
        }

        [When(@"I perform a search for news by '(.*)' criteria with a value of '(.*)'")]
        public void WhenIPerformASearchForNewsByCriteriaWithAValueOf(NewsViewService.NewsSearchCriteria criteria, string parameters)
        {
            var response = _newsViewService.GetNewsWithParameters(criteria, parameters);
            PropertyBucket.Remember(GET_SEARCH_RESPONSE_KEY, response);
        }

        [When(@"I perform a PATCH to update Saved Search keywords criteria with a value of '(.*)'")]
        public void WhenIPerformAPATCHToUpdateSavedSearchKeywordsCriteriaWithAValueOf(string keyword)
        {
            var savedSearch = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(POST_SAVED_SEARCH_RESPONSE_KEY);
            var response = _newsViewService.GetNewsWithParameters(NewsViewService.NewsSearchCriteria.Keywords, keyword);
            var patchResponse = _savedSearchesService.PatchSavedSearch(savedSearch.Data.Item.Id, response.Data.Key);
            PropertyBucket.Remember(PATCHED_SAVED_SEARCH_CRITERIA_RESPONSE, patchResponse);
        }

        [When(@"I create a new saved search")]
        public void WhenICreateANewSavedSearch()
        {
            When("I perform a search for news by 'Keywords' criteria with a value of 'basketball'");
            When("I save the search with name 'Saved Search Update Criteria'");
            Then("the Saved Search endpoint should respond with a '201' for creating an item");
        }
        #endregion

        #region Then Steps
        [Then(@"the Saved Search endpoint should respond with a '(.*)' for creating an item")]
        public void ThenTheNewsForwardEndpointShouldRespondWithAGivenStatusCodeForCreatingAnItem(int statusCode)
        {
            IRestResponse<SingleSavedSearch> singleSavedSearch = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(POST_SAVED_SEARCH_RESPONSE_KEY);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(singleSavedSearch), singleSavedSearch.Content);
        }

        [Then(@"the Saved Search endpoint should respond with a '(.*)' for deleting an item")]
        public void ThenTheNewsForwardEndpointShouldRespondWithAGivenStatusCodeForDeletingAnItem(int statusCode)
        {
            IRestResponse<SingleSavedSearch> savedSearch = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(POST_SAVED_SEARCH_RESPONSE_KEY);
            var savedSearchesDeletionResponse = _savedSearchesService.DeleteSavedSearch(savedSearch.Data.Item.Id);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(savedSearchesDeletionResponse), Err.Line("Delete didn't work"));
        }
        
        [Then(@"all saved searches returned have a name and id")]
        public void ThenAllSavedSearchesReturnedHaveANameAndId()
        {
            var savedSearches = PropertyBucket.GetProperty<SavedSearches>(GET_SAVED_SEARCH_RESPONSE_KEY);
            var items = savedSearches.Items;
            Assert.Multiple(() =>
            {
                Assert.That(savedSearches.ItemCount, Is.GreaterThan(0), Err.Msg("No searches were returned"));
                Assert.IsFalse(items.Any(i => string.IsNullOrEmpty(i.Id.ToString())), Err.Msg("Not all searches have a valid id"));
                Assert.IsFalse(items.Any(i => i.Name == null), Err.Msg("Not all searches have a valid name"));
            });                       
        }

        [Then(@"the saved search is returned")]
        public void ThenTheSavedSearchIsReturned()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(GET_SAVED_SEARCH_RESPONSE_KEY);
            var responseName = response.Data.Item.Name;
            var name = PropertyBucket.GetProperty(SAVED_SEARCH_NAME);
            Assert.AreEqual(name, responseName);
        }

        [Then(@"I should see that the saved search includes the search criteria for news details")]
        public void ThenIShouldSeeThatTheSavedSearchIncludesTheSearchCriteriaForNewsDetails()
        {
            var singleSavedSearch = PropertyBucket.GetProperty<IRestResponse<SavedSearchItemCriteria>>(GET_SAVED_SEARCH_CRITERIA_RESPONSE);
            Assert.That(singleSavedSearch.Data.Q_Keywords, Is.Not.Null, Err.Msg("Keywords Criteria was not saved properly"));
            Assert.That(singleSavedSearch.Data.Q_StartDate, Is.Not.Null, Err.Msg("Start Date Criteria was not saved properly"));
            Assert.That(singleSavedSearch.Data.Q_EndDate, Is.Not.Null, Err.Msg("End Date Criteria was not saved properly"));
            Assert.That(singleSavedSearch.Data.Q_Tones, Is.Not.Null, Err.Msg("Tone Criteria was not saved properly"));
            Assert.That(singleSavedSearch.Data.Q_SmartTags.Ids, Is.Not.Null, Err.Msg("Smart Tag Criteria was not saved properly"));
            Assert.That(singleSavedSearch.Data.Q_Tags, Is.Not.Null, Err.Msg("Tag Criteria was not saved properly"));
        }

        [Then(@"I should see that the saved search includes the value '(.*)' for keywords criteria")]
        public void ThenIShouldSeeThatTheSavedSearchIncludesTheValueForKeywordsCriteria(string keyword)
        {
            var singleSavedSearch = PropertyBucket.GetProperty<IRestResponse<SavedSearchItemCriteria>>(GET_SAVED_SEARCH_CRITERIA_RESPONSE);
            Assert.That(singleSavedSearch.Data.Q_Keywords.ToUpper(), Is.EqualTo(keyword.ToUpper()), Err.Line("The value for Saved Search Keywords Criteria is not as expected"));
        }

        [Then(@"I should see that the Saved Search value for keywords criteria was updated to '(.*)'")]
        public void ThenIShouldSeeThatTheSavedSearchValueForKeywordsCriteriaWasUpdatedTo(string keyword)
        {
            var patchedSavedSearch = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(PATCHED_SAVED_SEARCH_CRITERIA_RESPONSE);
            Assert.That(patchedSavedSearch.Data.Item.Criteria.Q_Keywords.ToUpper(), Is.EqualTo(keyword.ToUpper()), Err.Line("The value for Saved Search Keywords Criteria is not as expected"));
        }
        #endregion
    }
}