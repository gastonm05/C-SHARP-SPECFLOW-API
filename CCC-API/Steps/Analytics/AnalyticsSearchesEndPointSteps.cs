using BoDi;
using CCC_API.Data.PostData.Analytics;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Analytics;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Analytics
{
    [Binding]
    public class AnalyticsSearchesEndPointSteps : AuthApiSteps
    {
        public const string GET_SEARCHES_KEY = "GetSearches";
        public const string GET_SEARCH_GROUPS_KEY = "GetSearchGroups";
        public const string EXPECTED_ANALYTICS_SEARCH_KEY = "ExpectedAnalyticsSearch";
        public const string EXPECTED_ANALYTICS_SEARCH_GROUP_FROM_POST_BODY_KEY = "ExpectedAnalyticsSearchGroupFromPostBody";
        public const string ANALYTICS_SEARCH_READONLY = "AnalyticsSearchReadonly";

        private SearchesService _searchesService;

        public AnalyticsSearchesEndPointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _searchesService = new SearchesService(SessionKey);
        }

        [When(@"I perform a GET for analytics searches")]
        public void WhenIPerformAGETForAnalyticsSearches()
        {
            var searchesResponse = _searchesService.GetSearches();
            PropertyBucket.Remember(GET_SEARCHES_KEY, searchesResponse);
        }

        [When(@"I create a new analytics search '(.*)'")]
        public void WhenICreateANewAnalyticsSearchNumber(string name)
        {
            var search = new AnalyticsSearch().Initialize($"{PropertyBucket.TestId}#{name}", "C", new[] { "test", "testtoo" });
            PropertyBucket.Remember($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}", search);
            _searchesService.CreateSearch(search);
        }

        [When(@"I perform a GET for analytics search groups")]
        public void WhenIPerformAGETForAnalyticsSearchGroups()
        {
            var searchGroups = _searchesService.GetSearchGroups();
            PropertyBucket.Remember(GET_SEARCH_GROUPS_KEY, searchGroups);
        }

        [When(@"I create a new analytics search group")]
        public void WhenICreateANewAnalyticsSearchGroup()
        {
            var group = _searchesService.CreateSearchGroup(TestId, "Company");
            PropertyBucket.Remember(EXPECTED_ANALYTICS_SEARCH_GROUP_FROM_POST_BODY_KEY, group);
        }

        [When(@"I add the analytics search '(.*)' to the search group")]
        public void WhenIAddTheAnalyticsSearchToTheSearchGroup(string name)
        {
            var search = GetSearchForTest(name);
            var group = GetGroupForTest();
            _searchesService.AddSearchToGroup(search, group);
            PropertyBucket.Remember($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}", search, true);
        }

        [When(@"I remove the analytics search '(.*)' from the search group")]
        public void WhenIRemoveTheAnalyticsSearchFromTheSearchGroup(string name)
        {
            var search = GetSearchForTest(name);
            var group = GetGroupForTest();
            _searchesService.RemoveSearchFromGroup(search, group);
            PropertyBucket.Remember($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}", search, true);
        }

        [When(@"I perform a POST to create a new analytics search '(.*)'")]
        public void WhenIPerformAPOSTToCreateANewAnalyticsSearch(string name)
        {
            var response = _searchesService.CreateSearchReadOnly(name);
            PropertyBucket.Remember(ANALYTICS_SEARCH_READONLY, response);
        }

        private AnalyticsSearch GetSearchForTest(string name)
        {
            var expectedSearch = PropertyBucket.GetProperty<AnalyticsSearch>($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}");
            var searches = _searchesService.GetSearches().Items.FirstOrDefault(s => s.SearchName == expectedSearch.SearchName);
            Assert.IsNotNull(searches, $"No search found matching '{expectedSearch.SearchName}'");
            return searches;
        }

        private AnalyticsSearchGroup GetGroupForTest()
        {
            var expectedGroup = PropertyBucket.GetProperty<AnalyticsSearchGroupPostBody>(EXPECTED_ANALYTICS_SEARCH_GROUP_FROM_POST_BODY_KEY);
            var group = _searchesService.GetSearchGroups().FirstOrDefault(g => g.Name == expectedGroup.Name);
            Assert.IsNotNull(group, $"No search found matching '{expectedGroup.Name}'");
            return group;
        }

        [Then(@"I can GET the analytics search '(.*)' by id")]
        public void ThenICanGetTheAnalyticsSearchById(string name)
        {
            var expectedSearch = PropertyBucket.GetProperty<AnalyticsSearch>($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}");
            var search = _searchesService.GetSearches().Items.FirstOrDefault(s => s.SearchName == expectedSearch.SearchName);
            Assert.That(search, Is.Not.Null, "Did not find Analytics Search in Searches");
            var searchById = _searchesService.GetSearch(search.SearchId);
            Assert.That(searchById, Is.Not.Null, "Did not find Analytics Search in Searches");
            expectedSearch.VerifyAgainst(search);
        }

        [Then(@"the new analytics search '(.*)' exists")]
        public void ThenTheNewAnalyticsSearchExists(string name)
        {
            var expectedSearch = PropertyBucket.GetProperty<AnalyticsSearch>($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}");
            var search = _searchesService.GetSearches().Items.FirstOrDefault(s => s.SearchName == expectedSearch.SearchName);
            Assert.That(search, Is.Not.Null, "Did not find Analytics Search in Searches");
            expectedSearch.VerifyAgainst(search);
        }

        [Then(@"I delete the analytics search '(.*)'")]
        public void ThenIDeleteTheAnalyticsSearch(string name)
        {
            var expectedSearch = PropertyBucket.GetProperty<AnalyticsSearch>($"{EXPECTED_ANALYTICS_SEARCH_KEY}#{name}");
            var search = _searchesService.GetSearches().Items.FirstOrDefault(s => s.SearchName == expectedSearch.SearchName);
            _searchesService.DeleteSearch(search.SearchId);
        }

        [Then(@"the new analytics search group exists")]
        public void ThenTheNewAnalyticsSearchGroupExists()
        {
            var expectedGroup = PropertyBucket.GetProperty<AnalyticsSearchGroupPostBody>(EXPECTED_ANALYTICS_SEARCH_GROUP_FROM_POST_BODY_KEY);
            var group = _searchesService.GetSearchGroups().FirstOrDefault(g => g.Name == expectedGroup.Name);
            Assert.That(group, Is.Not.Null, "Did not find Analytics Search Group in Groups");
            Assert.That(group.Id, Is.GreaterThan(0), "GroupId invalid");
            Assert.That(group.Name, Is.EqualTo(expectedGroup.Name), "GroupName did not match");
            Assert.That(group.CategoryId, Is.EqualTo(expectedGroup.CategoryId), "CategoryId did not match");
        }

        [Then(@"I cannot create a duplicate analytics search group")]
        public void ThenICannotCreateADuplicateAnalyticsSearchGroup()
        {
            try
            {
                _searchesService.CreateSearchGroup(TestId, "Company");
                Assert.Fail("No exception when creating a duplicate analytics search group");
            }
            catch (Exception e)
            {
                Assert.That(e.Message.Contains("BadRequest"), Is.True, "Wrong exception when creating a duplicate analytics search group");
            }
        }

        [Then(@"I delete the analytics search group")]
        public void ThenIDeleteTheAnalyticsSearchGroup()
        {
            var expectedGroup = PropertyBucket.GetProperty<AnalyticsSearchGroupPostBody>(EXPECTED_ANALYTICS_SEARCH_GROUP_FROM_POST_BODY_KEY);
            var group = _searchesService.GetSearchGroups().FirstOrDefault(g => g.Name == expectedGroup.Name);
            _searchesService.DeleteSearchGroup(group.Id);
        }

        [Then(@"there are analytics searches")]
        public void ThenThereAreAnalyticsSearches()
        {
            var searchesResponse = PropertyBucket.GetProperty<SearchesResponse>(GET_SEARCHES_KEY);
            Assert.That(searchesResponse, Is.Not.Null, "SearchesResponse was null");
        }

        [Then(@"there are analytics group searches")]
        public void ThenThereAreAnalyticsGroupSearches()
        {
            var searchGroups = PropertyBucket.GetProperty<List<AnalyticsSearchGroup>>(GET_SEARCH_GROUPS_KEY);
            Assert.That(searchGroups.Count, Is.GreaterThan(0), "No Analytics Search Groups found");
            foreach (var group in searchGroups)
            {
                Assert.That(group.Id, Is.GreaterThan(0), "GroupId invalid");
                Assert.That(string.IsNullOrEmpty(group.Name), Is.False, "GroupName invalid");
            }
        }

        [Given(@"analytics profile '(company|product|message)' searches present: '(.*)'")]
        public void GivenAnalyticsProfileCompanySearchesPresent(string type, string companySearches)
        {
            var searchType = 
                type == "company" ? "C" : 
                type == "product" ? "P" 
                : "M";

            var expSearches = companySearches?.Split(',').Select(_ => _.Trim()).ToList();
            Assert.IsNotEmpty(expSearches, "No searches to test");

            // Check if saved searches present
            var analyticsSearches = _searchesService.GetSearches().Items;
            var searches = analyticsSearches.Select(s => s.SearchName).ToList();
            var missing = expSearches?.Where(s => !searches.Contains(s)).ToList();
            missing?.ForEach(s =>
                    _searchesService.CreateSearch(new AnalyticsSearch().Initialize(s, searchType, new[] { s })));

            if (missing != null && missing.Any()) // Some searches created ?
            {
                var updatedSearches = _searchesService.GetSearches().Items;
                PropertyBucket.Remember(GET_SEARCHES_KEY, updatedSearches, true);
            }
            else
            {
                PropertyBucket.Remember(GET_SEARCHES_KEY, analyticsSearches, true);
            }
        }

        [Then(@"the news analytics endpoint should respond with unauthorized access")]
        public void ThenTheNewsAnalyticsEndpointShouldRespondWithUnauthorizedAccess()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<AnalyticsSearch>>(ANALYTICS_SEARCH_READONLY);
            Assert.AreEqual(403, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }
    }
}