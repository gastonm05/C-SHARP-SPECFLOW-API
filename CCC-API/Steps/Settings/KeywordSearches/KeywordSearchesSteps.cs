using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Data.Responses.Settings.KeywordSearches;
using CCC_API.Services.Common;
using CCC_API.Services.News;
using CCC_API.Services.Settings.KeywordSearches;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;
using CCC_API.Data.Responses.Common;

namespace CCC_API.Steps.Settings.KeywordSearches
{
    [Binding]
    public class KeywordSearchesSteps : AuthApiSteps

    {
        public enum KeywordSearchType { AND, OR, ANDandOR, ANDandNOT, ORandNOT, ANDORandNOT, NAMELIMIT, NAMELIMITBELOW, ADVANCED, REGULAR }
        public enum KeywordSearchNewType { NEW, SAMPLE, SAMPLELIMIT, NEWNAMELIMIT, SAMPLEINVALID }
        private const string KeywordSearches = "KeywordSearches";
        private const int CHAR_LIMIT_NAME_SEARCH = 200;
        private const string DELETE_KEYWORDSEARCHES_RESPONSE_KEY = "DeleteKeywordSearchesResponse";
        private const string GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DEFAULT_KEY = "GetKeywordSearchesEarnedMediaDefaultResponse";
        private const string GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DATAGROUP_2_KEY = "GetKeywordSearchesEarnedMediaDataGroup2Response";
        private const string GET_KEYWORDSEARCHES_RESPONSE_DEFAULT_KEY = "GetKeywordSearchesDefaultResponse";
        private const string GET_KEYWORDSEARCHES_RESPONSE_OTRO_KEY = "GetKeywordSearchesOtroResponse";
        private const string GET_NEWS_ARCHIVE_COUNTRIES_KEY = "GetNewsArchiveCountriesResponse";
        private const string GET_NEWS_ARCHIVE_LANGUAGES_KEY = "GetNewsArchiveLanguagesResponse";
        private const string GET_NEWS_ARCHIVE_SOURCES_KEY = "GetNewsArchiveSourcesResponse";
        private const string KEYWORDSEARCH_DATA_KEY = " KeywordSearchData";
        private const string POST_KEYWORDSEARCHES_CREATE_RESPONSE_KEY = "CreateKeywordSearchesResponse";
        private const string POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY = "CreateSampleKeywordSearchesResponse";
        private const string PUT_KEYWORDSEARCHES_UPDATE_RESPONSE_KEY = "UpdateKeywordSearchesResponse";
        private const string PUT_ADVANCED_BOOLEAN_KEYWORDSEARCHES_RENAME_RESPONSE_KEY = "PutRenameAdvancedResponse";
        private const string PUT_REGULAR_KEYWORDSEARCHES_RENAME_RESPONSE_KEY = "PutRenameRegularResponse";
        private const string TEST_DATA_ADVANCED_BOOLEAN_SEARCH_ID_KEY = "AdvancedBooleanSearchId";
        private const string TEST_DATA_COMPANY_ID_KEY = "CompanyId";
        private const string TEST_DATA_COUNTRIES_KEY = "Countries";
        private const string TEST_DATA_DATA_GROUP_ID_KEY = "DataGroupId";
        private const string TEST_DATA_DELETE_NON_USER_CREATED_EARNMEDIA_ID_KEY = "DeleteNonUserCreatedSearchEarnedMediaId";
        private const string TEST_DATA_DELETE_NON_USER_CREATED_SUPPORT_ID_KEY = "DeleteNonUserCreatedSearchSupportId";
        private const string TEST_DATA_EXPECTED_SEARCH_EARNEDMEDIA_ID_DEFAULT_KEY = "EarnedMediaDefaultDataGroupSearchId";
        private const string TEST_DATA_EXPECTED_SEARCH_EARNEDMEDIA_ID_DATA_GROUP_2_KEY = "EarnedMediaDataGroup2DataGroupSearchId";
        private const string TEST_DATA_EXPECTED_SEARCH_ID_DEFAULT_KEY = "defaultDataGroupSearchId";
        private const string TEST_DATA_EXPECTED_SEARCH_ID_OTRO_KEY = "otroDataGroupSearchId";
        private const string TEST_DATA_KEY_ID_KEY = "keyId";
        private const string TEST_DATA_KEYWORDSTEXT1_KEY = "KeywordsText1";
        private const string TEST_DATA_KEYWORDSTEXT2_KEY = "KeywordsText2";
        private const string TEST_DATA_KEYWORDSLIMITTEXT1_KEY = "LimitKeywordsText1";
        private const string TEST_DATA_KEYWORDSTLIMITEXT2_KEY = "LimitKeywordsText2";
        private const string TEST_DATA_LANGUAGES_KEY = "Languages";
        private const string TEST_DATA_MONITORING_ID_KEY = "TestMonitoringId";        
        private const string TEST_DATA_NAME_KEY = "TestName";
        private const string TEST_DATA_NEW_ANDKEYWORDS_KEY = "TestNewAndKeywords";
        private const string TEST_DATA_NEW_NAME_KEY = "TestNewName";
        private const string TEST_DATA_REGULAR_SEARCH_ID_KEY = "RegularSearchId";
        private const string TEST_DATA_NEW_SEARCH_CLAUSE_KEY = "TestNewSearchClause";
        private const string TEST_DATA_SEARCH_WINDOW_KEY = "TestSearchWindow";



        public KeywordSearchesSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I perform a POST on MediaMonitoring endpoint to create a '(.*)' Keyword Search with '(.*)' Keywords with a search name with '(.*)' characters and search term (.*)")]
        public void WhenIPerformAPOSTOnMediaMonitoringEndpointToCreateANewKeywordSearchWithKeywords(KeywordSearchNewType keywordSearchNewType, KeywordSearchType keywordSearchType, int characterAmount, string searchTerm)
        {
            bool isSample = true;
            bool isAdvancedSearch = false;
            bool isBelowThreshold = characterAmount == CHAR_LIMIT_NAME_SEARCH;
            string searchClause = "";
            string name = StringUtils.RandomString(StringUtils.AlphaChars, characterAmount);
            int companyId = Int32.Parse(PropertyBucket.GetProperty<string>(TEST_DATA_COMPANY_ID_KEY));
            int dataGroupId = Int32.Parse(PropertyBucket.GetProperty<string>(TEST_DATA_DATA_GROUP_ID_KEY)); ;
            string language = PropertyBucket.GetProperty<string>(TEST_DATA_LANGUAGES_KEY);
            int country = Int32.Parse(PropertyBucket.GetProperty<string>(TEST_DATA_COUNTRIES_KEY));
            int[] countries = { country };
            string[] languages = { language };
            string[] sources = { "1"};
            switch (keywordSearchNewType)
            {
                case KeywordSearchNewType.SAMPLELIMIT:
                case KeywordSearchNewType.SAMPLE:
                    {
                        
                        string keywordsText1 = PropertyBucket.GetProperty<string>(TEST_DATA_KEYWORDSTEXT1_KEY);
                        string keywordsText2 = PropertyBucket.GetProperty<string>(TEST_DATA_KEYWORDSTEXT2_KEY);
                        if (keywordSearchNewType.Equals(KeywordSearchNewType.SAMPLELIMIT))
                        {
                            keywordsText1 = PropertyBucket.GetProperty<string>(TEST_DATA_KEYWORDSLIMITTEXT1_KEY);
                            keywordsText2 = PropertyBucket.GetProperty<string>(TEST_DATA_KEYWORDSTLIMITEXT2_KEY);
                        }                        
                        string[] andKeywords = { };
                        string[] orKeywords = { };
                        string[] notKeywords = { };

                        switch (keywordSearchType)
                        {
                            case KeywordSearchType.AND:
                                {
                                    andKeywords = new string[] { keywordsText1, keywordsText2 };
                                    break;
                                }
                            case KeywordSearchType.OR:
                                {
                                    orKeywords = new string[] { keywordsText1, keywordsText2 };
                                    break;
                                }
                            case KeywordSearchType.ANDandOR:
                                {
                                    andKeywords = new string[] { keywordsText1, keywordsText2 };
                                    orKeywords = new string[] { keywordsText1, keywordsText2 };
                                    break;
                                }
                            case KeywordSearchType.ANDandNOT:
                                {
                                    andKeywords = new string[] { keywordsText1, keywordsText2 };
                                    notKeywords = new string[] { keywordsText1 + keywordsText2, keywordsText2 + keywordsText1 };
                                    break;
                                }
                            case KeywordSearchType.ORandNOT:
                                {
                                    orKeywords = new string[] { keywordsText1, keywordsText2 };
                                    notKeywords = new string[] { keywordsText1 + keywordsText2, keywordsText2 + keywordsText1 };
                                    break;
                                }
                            case KeywordSearchType.ANDORandNOT:
                                {
                                    andKeywords = new string[] { keywordsText1, keywordsText2 };
                                    orKeywords = new string[] { keywordsText1, keywordsText2 };
                                    notKeywords = new string[] { keywordsText1 + keywordsText2, keywordsText2 + keywordsText1 };
                                    break;
                                }
                            case KeywordSearchType.NAMELIMIT:
                                {
                                    andKeywords = new string[] { keywordsText1, keywordsText2 };
                                    isSample = false;
                                    break;
                                }
                            case KeywordSearchType.ADVANCED:
                                {
                                    isAdvancedSearch = true;
                                    searchClause = searchTerm;
                                    break;
                                }
                        }
                        var keywordSearchToCreate = new MediaMonitorUserSearch(name, companyId, dataGroupId, searchClause, andKeywords, orKeywords, notKeywords, languages, countries, sources, isAdvancedSearch);
                        PropertyBucket.Remember(KEYWORDSEARCH_DATA_KEY, keywordSearchToCreate);
                        if (isSample)
                        {
                            var createKeywordSearchSampleResponse = new MediaMonitoringService(SessionKey).CreateSampleKeywordSearch(keywordSearchToCreate);
                            PropertyBucket.Remember(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY, createKeywordSearchSampleResponse);
                            break;
                        }
                        break;
                    }
                case KeywordSearchNewType.NEW:
                    {
                        var keywordSearchToCreate = PropertyBucket.GetProperty<MediaMonitorUserSearch>(KEYWORDSEARCH_DATA_KEY);
                        var postSampleResponse = PropertyBucket.GetProperty<IRestResponse<SampleKeywordSearchResponse>>(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
                        var createKeywordSearchResponse = new MediaMonitoringService(SessionKey).CreateKeywordSearch(keywordSearchToCreate);
                        PropertyBucket.Remember(POST_KEYWORDSEARCHES_CREATE_RESPONSE_KEY, createKeywordSearchResponse);
                        string monitoringId = JsonConvert.DeserializeObject<MediaMonitorUserSearch>(createKeywordSearchResponse.Content).Id;
                        PropertyBucket.Remember(TEST_DATA_MONITORING_ID_KEY, monitoringId);
                        break;
                    }
                case KeywordSearchNewType.NEWNAMELIMIT:
                    {
                        var keywordSearchToCreate = PropertyBucket.GetProperty<MediaMonitorUserSearch>(KEYWORDSEARCH_DATA_KEY);
                        var createKeywordSearchResponse = new MediaMonitoringService(SessionKey).CreateKeywordSearch(keywordSearchToCreate);
                        PropertyBucket.Remember(POST_KEYWORDSEARCHES_CREATE_RESPONSE_KEY, createKeywordSearchResponse);
                        if (isBelowThreshold)
                        {
                            string monitoringId = JsonConvert.DeserializeObject<MediaMonitorUserSearch>(createKeywordSearchResponse.Content).Id;
                            PropertyBucket.Remember(TEST_DATA_MONITORING_ID_KEY, monitoringId);
                            break;
                        }
                        break;
                    }
                case KeywordSearchNewType.SAMPLEINVALID:
                    {
                        isAdvancedSearch = true;
                        searchClause = searchTerm;
                        var keywordSearchToCreate = new MediaMonitorUserSearch(name, companyId, dataGroupId, searchClause, languages, countries, sources, isAdvancedSearch);
                        var keywordSearchToCreatePost = new MediaMonitorUserSearchPost(keywordSearchToCreate);
                        PropertyBucket.Remember(KEYWORDSEARCH_DATA_KEY, keywordSearchToCreate);
                        var createKeywordSearchSampleResponse = new MediaMonitoringService(SessionKey).CreateSampleKeywordSearchInvalid(keywordSearchToCreatePost);
                        PropertyBucket.Remember(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY, createKeywordSearchSampleResponse);
                        break;
                    }
            }
        }
        [When(@"I perform a PUT on MediaMonitoring name endpoint to rename (.*) Keyword Search")]
        public void WhenIPerformAPUTOnMediaMonitoringNameEndpointToRenameKeywordSearch(KeywordSearchType keywordSearchType)
        {
            string searchId = "";
            switch (keywordSearchType)
            {
                case KeywordSearchType.ADVANCED:
                    {
                        searchId = PropertyBucket.GetProperty<string>(TEST_DATA_ADVANCED_BOOLEAN_SEARCH_ID_KEY);
                        break;
                    }
                case KeywordSearchType.REGULAR:
                    {
                        searchId = PropertyBucket.GetProperty<string>(TEST_DATA_REGULAR_SEARCH_ID_KEY);
                        break;
                    }
            }             
            var renameKeywordSearchResponse = new MediaMonitoringService(SessionKey).Rename(searchId,"\"Automation new name\"");
            switch (keywordSearchType)
            {
                case KeywordSearchType.ADVANCED:
                    {
                        PropertyBucket.Remember(PUT_ADVANCED_BOOLEAN_KEYWORDSEARCHES_RENAME_RESPONSE_KEY, renameKeywordSearchResponse);
                        break;
                    }
                case KeywordSearchType.REGULAR:
                    {
                        PropertyBucket.Remember(PUT_REGULAR_KEYWORDSEARCHES_RENAME_RESPONSE_KEY, renameKeywordSearchResponse);
                        break;
                    }
            }            
        }

        [Then(@"MediaMonitoring name endpoint PUT response code for Advanced Boolean Search should be (.*)")]
        public void ThenMediaMonitoringEndpointNamePutAdvancedResponseCodeShouldBe(int responseCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(PUT_ADVANCED_BOOLEAN_KEYWORDSEARCHES_RENAME_RESPONSE_KEY);
            Assert.NotNull(response, "Keyword Searches Rename PUT transaction failed", response.Content);
            Assert.AreEqual(responseCode, (int)response.StatusCode, Err.Line(response.Content));
        }
        [Then(@"MediaMonitoring name endpoint PUT response code for Regular Search should be (.*)")]
        public void ThenMediaMonitoringEndpointNamePutRegularResponseCodeShouldBe(int responseCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(PUT_REGULAR_KEYWORDSEARCHES_RENAME_RESPONSE_KEY);
            Assert.NotNull(response, "Keyword Searches Rename PUT transaction failed", response.Content);
            Assert.AreEqual(responseCode, (int)response.StatusCode, Err.Line(response.Content));
        }

        [Then(@"MediaMonitoring endpoint POST response code should be (.*)")]
        public void ThenMediaMonitoringEndpointPOSTResponseCodeShouldBe(int responseCode)
        {
                var response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(POST_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
                Assert.NotNull(response, "Keyword Searches POST transaction failed", response.Content);
                Assert.AreEqual(responseCode, (int)response.StatusCode, Err.Line(response.Content));            
        }
        [Then(@"Monitoring Sample POST endpoint response code should be (.*) and validation message should be (.*) and Error code (.*)")]
        public void ThenMonitoringSamplePOSTEndpointResponseCodeShouldBeAndMessageShouldBeSearchClauseCannotBeBlank_(int responseCode, string expectedMessage, int errorCode)
        {
            IRestResponse<Dictionary<string, string>> response = PropertyBucket.GetProperty<IRestResponse<Dictionary<string, string>>>(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);

            var incident = JsonConvert.DeserializeObject<Incident>(response.Content);
            string actualErrorCode = incident.ErrorCode;
            string actualMessage = incident.Message;
            string errorCodeToCompare = errorCode.ToString();
            if (errorCodeToCompare.Equals("0")) { errorCodeToCompare = null; }
            
            Assert.AreEqual(responseCode, (int)response.StatusCode, Err.Line (response.Content));
            Assert.AreEqual(errorCodeToCompare, actualErrorCode, Err.Line(response.Content));
            Assert.AreEqual(expectedMessage, actualMessage, Err.Line(response.Content));
        }

        [Then(@"MediaMonitoring endpoint PUT response code should be (.*)")]
        public void ThenMediaMonitoringEndpointPUTResponseCodeShouldBe(int responseCode)
        {
            {
                var response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(PUT_KEYWORDSEARCHES_UPDATE_RESPONSE_KEY);
                Assert.NotNull(response, "Keyword Searches PUT transaction failed", response.Content);
                Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
            }
        }

        [Then(@"MediaMonitoring endpoint GET response code should be (.*) for '(.*)' datagroup")]
        public void ThenMediaMonitoringEndpointGETResponseCodeShouldBeForDatagroup(int responseCode, string dataGroupName)
        {
            {
                IRestResponse<MediaMonitorUserSearch> response = null;
                switch (dataGroupName)
                {
                    case "(Default)":
                        {
                            response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_RESPONSE_DEFAULT_KEY);
                            break;
                        }
                    case "otro":
                        {
                            response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_RESPONSE_OTRO_KEY);
                            break;
                        }

                }
                Assert.NotNull(response, "Keyword Searches GET transaction failed", response.Content);
                Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
            }
        }
               
       [Then(@"Earned Media MediaMonitoring endpoint GET response code should be (.*) for '(.*)' datagroup")]
        public void ThenMediaMonitoringEndpointGETResponseCodeShouldBeForEarnedMediaDatagroup(int responseCode, string dataGroupName)
        {
            {
                IRestResponse<MediaMonitorUserSearch> response = null;
                switch (dataGroupName)
                {
                    case "(Default)":
                        {
                            response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DEFAULT_KEY);
                            break;
                        }
                    case "Data Group 2":
                        {
                            response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DATAGROUP_2_KEY);
                            break;
                        }

                }
                Assert.NotNull(response, "Keyword Searches GET transaction failed", response.Content);
                Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
            }
        }
        [Then(@"Verify that Name matchs with just created Keyword Search for '(.*)' datagroup")]
        public void ThenVerifyThatNameMatchsWithJustCreatedKeywordSearchForDatagroup(string dataGroupName)
        {
            {
                IRestResponse<MediaMonitorUserSearch> response = null;
                switch (dataGroupName)
                {
                    case "(Default)":
                        {
                            response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_RESPONSE_DEFAULT_KEY);
                            break;
                        }
                    case "otro":
                        {
                            response = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_RESPONSE_OTRO_KEY);
                            break;
                        }
                }
                Assert.NotNull(response, "Keyword Searches GET transaction failed", response.Content);
                MediaMonitorUserSearch mediaMonitorUserSearch = PropertyBucket.GetProperty<MediaMonitorUserSearch>(KEYWORDSEARCH_DATA_KEY);
                string expectedName = mediaMonitorUserSearch.Name;
                List<MediaMonitorUserSearch> listSearches = JsonConvert.DeserializeObject<List<MediaMonitorUserSearch>>(response.Content);
                Assert.True(listSearches.Any(a => a.Name.Equals(expectedName)), "Expected KeywordSearch not found");
            }
        }
        [When(@"I perform a GET on MediaMonitoring endpoint for '(.*)' datagroup")]
        public void WhenIPerformAGETOnMediaMonitoringEndpointForDatagroup(string dataGroupName)
        {
            var response = new MediaMonitoringService(SessionKey).GetKeywordSearches();
            switch (dataGroupName)
            {
                case "(Default)":
                    {

                        PropertyBucket.Remember(GET_KEYWORDSEARCHES_RESPONSE_DEFAULT_KEY, response);
                        break;
                    }
                case "otro":
                    {
                        PropertyBucket.Remember(GET_KEYWORDSEARCHES_RESPONSE_OTRO_KEY, response);
                        break;
                    }
            }
        }

        [When(@"I perform a GET on MediaMonitoring endpoint for Earned Media '(.*)' datagroup")]
        public void WhenIPerformAGETOnMediaMonitoringEndpointForEarnedMediaDatagroup(string dataGroupName)
        {
            var response = new MediaMonitoringService(SessionKey).GetKeywordSearches();
            switch (dataGroupName)
            {
                case "(Default)":
                    {

                        PropertyBucket.Remember(GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DEFAULT_KEY, response);
                        break;
                    }
                case "Data Group 2":
                    {
                        PropertyBucket.Remember(GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DATAGROUP_2_KEY, response);
                        break;
                    }
            }
        }
        [When(@"I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search\.")]
        public void WhenIPerformAPUTOnMediaMonitoringEndpointToUpdateJustCreatedKeywordSearch_()
        {
            var keywordSearchToCreate = PropertyBucket.GetProperty<MediaMonitorUserSearch>(KEYWORDSEARCH_DATA_KEY);
            var postSampleResponse = PropertyBucket.GetProperty<IRestResponse<SampleKeywordSearchResponse>>(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
            var keyId = JsonConvert.DeserializeObject<SampleKeywordSearchResponse>(postSampleResponse.Content).Key;
            var postResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(POST_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
            var id = JsonConvert.DeserializeObject<MediaMonitorUserSearch>(postResponse.Content).Id;
            var beModifiedKeywordSearch = keywordSearchToCreate;
            string newName = "ChangedName";
            string[] newAndKeywords = { "testA", "testB", "testC" };
            string newSearchClause = "\"testA\" AND \"testB\" AND \"testC\"";
            PropertyBucket.Remember(TEST_DATA_NEW_ANDKEYWORDS_KEY, newAndKeywords);
            PropertyBucket.Remember(TEST_DATA_NEW_SEARCH_CLAUSE_KEY, newSearchClause);
            PropertyBucket.Remember(TEST_DATA_NEW_NAME_KEY, newName);
            beModifiedKeywordSearch.Name = newName;
            beModifiedKeywordSearch.AndKeywords = newAndKeywords;
            beModifiedKeywordSearch.Id = id;
            beModifiedKeywordSearch.SearchClause = newSearchClause;
            var putResponse = new MediaMonitoringService(SessionKey).ModifyMediaMonitoringKeywordSearch(beModifiedKeywordSearch.Id, beModifiedKeywordSearch);
            PropertyBucket.Remember(PUT_KEYWORDSEARCHES_UPDATE_RESPONSE_KEY, putResponse);
        }
        
        [Then(@"Modified Name and Keyword matchs with just updated Keyword Search for a '(.*)' search")]
        public void ThenModifiedNameAndKeywordMatchsWithJustUpdatedKeywordSearchForASearch(KeywordSearchType keywordSearchType)        
        {

            var modifiedKeywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(PUT_KEYWORDSEARCHES_UPDATE_RESPONSE_KEY);
            var modifiedKeywordSearch = JsonConvert.DeserializeObject<MediaMonitorUserSearch>(modifiedKeywordSearchResponse.Content);
            MediaMonitorUserSearch mediaMonitroUserSearch = PropertyBucket.GetProperty<MediaMonitorUserSearch>(KEYWORDSEARCH_DATA_KEY);
            string expectedName = mediaMonitroUserSearch.Name;
            string expectedSearchClause = PropertyBucket.GetProperty<string>(TEST_DATA_NEW_SEARCH_CLAUSE_KEY);
            string[] expectedAndKeywords = PropertyBucket.GetProperty<string[]>(TEST_DATA_NEW_ANDKEYWORDS_KEY);
            Assert.AreEqual(expectedName, modifiedKeywordSearch.Name);
            Assert.IsTrue(modifiedKeywordSearch.SearchClause.Contains(expectedSearchClause), $"Search clause '{modifiedKeywordSearch.SearchClause}' did not contain '{expectedSearchClause}'");
            if (!keywordSearchType.Equals(KeywordSearchType.ADVANCED))
                Assert.AreEqual(expectedAndKeywords, modifiedKeywordSearch.AndKeywords);
        }
        
        [When(@"Change datagroup to '(.*)' datagroup")]
        public void WhenChangeDatagroupToDatagroup(string datagroupName)
        {
            var accountService = new AccountInfoService(SessionKey);
            var currentDG = accountService.Me.Profile.Id;
            accountService.ChangeDataGroup(datagroupName);
         }

        [Then(@"Returned Keyword Searches results should include '(.*)' datagroup result search\.")]
        public void ThenReturnedKeywordSearchesResultsShouldIncludeDatagroupResultSearch_(string dataGroupName)
        {
            IRestResponse<MediaMonitorUserSearch> keywordSearchResponse = null;
            List<MediaMonitorUserSearch> listSearches = null;
            string expectedSearchId = null;
            switch (dataGroupName)
            {
                case "(Default)":
                    {
                        keywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_RESPONSE_DEFAULT_KEY);
                        listSearches = JsonConvert.DeserializeObject<List<MediaMonitorUserSearch>>(keywordSearchResponse.Content);
                        expectedSearchId = PropertyBucket.GetProperty<string>(TEST_DATA_EXPECTED_SEARCH_ID_DEFAULT_KEY);
                        break;
                    }
                case "otro":
                    {
                        keywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_RESPONSE_OTRO_KEY);
                        listSearches = JsonConvert.DeserializeObject<List<MediaMonitorUserSearch>>(keywordSearchResponse.Content);
                        expectedSearchId = PropertyBucket.GetProperty<string>(TEST_DATA_EXPECTED_SEARCH_ID_OTRO_KEY);
                        break;
                    }
            }
            Assert.IsTrue(listSearches.Any(a => a.Id.Equals(expectedSearchId)), "Expected KeywordSearch not found");
        }

        [Then(@"Returned Earned Media Keyword Searches results should include '(.*)' datagroup result search\.")]
        public void ThenReturnedEarnedMediaKeywordSearchesResultsShouldIncludeDatagroupResultSearch_(string dataGroupName)
        {
            IRestResponse<MediaMonitorUserSearch> keywordSearchResponse = null;
            List<MediaMonitorUserSearch> listSearches = null;
            string expectedSearchId = null;
            switch (dataGroupName)
            {
                case "(Default)":
                    {
                        keywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DEFAULT_KEY);
                        listSearches = JsonConvert.DeserializeObject<List<MediaMonitorUserSearch>>(keywordSearchResponse.Content);
                        expectedSearchId = PropertyBucket.GetProperty<string>(TEST_DATA_EXPECTED_SEARCH_EARNEDMEDIA_ID_DEFAULT_KEY);
                        break;
                    }
                case "Data Group 2":
                    {
                        keywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(GET_KEYWORDSEARCHES_EARNEDMEDIA_RESPONSE_DATAGROUP_2_KEY);
                        listSearches = JsonConvert.DeserializeObject<List<MediaMonitorUserSearch>>(keywordSearchResponse.Content);
                        expectedSearchId = PropertyBucket.GetProperty<string>(TEST_DATA_EXPECTED_SEARCH_EARNEDMEDIA_ID_DATA_GROUP_2_KEY);
                        break;
                    }
            }
            Assert.IsTrue(listSearches.Any(a => a.Id.Equals(expectedSearchId)), "Expected KeywordSearch not found");
        }


        [Then(@"Delete a non user created \(both Support and EarnedM Media\) Keyword Search and response code should be (.*) and message '(.*)'")]
        public void ThenDeleteANonUserCreatedKeywordSearchAndResponseCodeShouldBe(int responseCode, string expectedMessage)
        {
            string monitoringIdSupport = PropertyBucket.GetProperty<string>(TEST_DATA_DELETE_NON_USER_CREATED_SUPPORT_ID_KEY);
            string monitoringIdEarnedMedia = PropertyBucket.GetProperty<string>(TEST_DATA_DELETE_NON_USER_CREATED_EARNMEDIA_ID_KEY);
            var responseSupport = new MediaMonitoringService(SessionKey).DeleteKeywordSearch(monitoringIdSupport);
            Assert.AreEqual(responseCode, (int)responseSupport.StatusCode, responseSupport.Content);
            Assert.IsTrue(responseSupport.Content.Contains(expectedMessage), $"Response '{responseSupport.Content}' did not contain '{expectedMessage}'");
            var responseEarnedMedia = new MediaMonitoringService(SessionKey).DeleteKeywordSearch(monitoringIdEarnedMedia);
            Assert.AreEqual(responseCode, (int)responseEarnedMedia.StatusCode, responseEarnedMedia.Content);
            Assert.IsTrue(responseEarnedMedia.Content.Contains(expectedMessage), $"Response '{responseEarnedMedia.Content}' did not contain '{expectedMessage}'");
        }
    

    /// <summary>
    /// Clean up for created Keyword searches.
    /// </summary>
    [AfterScenario, Scope(Feature = KeywordSearches, Tag = "NeedsCleanup")]
        public void CleanCreatedKeywordSearches()
        {
  
            string monitoringId = PropertyBucket.GetProperty<string>(TEST_DATA_MONITORING_ID_KEY);
            if (monitoringId != "none")
            {
                var response = new MediaMonitoringService(SessionKey).DeleteKeywordSearch(monitoringId);
                Assert.AreEqual(200, (int)response.StatusCode, response.Content);
            }
        }

        [When(@"I perform a GET on NewsArchiveCountries endpoint")]
        public void WhenIPerformAGETOnNewsArchiveCountriesEndpoint()
        {
            var response = new NewsArchiveService(SessionKey).GetAvailableCountries();
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_COUNTRIES_KEY, response);
        }

        [Then(@"NewsArchiveCountries endpoint GET response code should be (.*) and returned data")]
        public void ThenNewsArchiveCountriesEndpointGETResponseCodeShouldBeAndReturnedData(int responseCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsArchiveCountriesResponse>>(GET_NEWS_ARCHIVE_COUNTRIES_KEY);
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
            Assert.NotNull(response, Err.Line("News Archive Countries GET transaction failed"), response.Content);
            var listCountries = JsonConvert.DeserializeObject<NewsArchiveCountriesResponse>(response.Content);
            Assert.That(listCountries.ItemCount, Is.GreaterThan(0), "No countries found");
        }

        [When(@"I perform a GET on NewsArchiveLanguages endpoint")]
        public void WhenIPerformAGETOnNewsArchiveLanguagesEndpoint()
        {
            var response = new NewsArchiveService(SessionKey).GetAvailableLanguages();
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_LANGUAGES_KEY, response);
        }

        [Then(@"NewsArchiveLanguages endpoint GET response code should be (.*) and returned data")]
        public void ThenNewsArchiveLanguagesEndpointGETResponseCodeShouldBeAndReturnedData(int responseCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsArchiveLanguagesResponse>>(GET_NEWS_ARCHIVE_LANGUAGES_KEY);
            Assert.NotNull(response, "News Archive Languages GET transaction failed", response.Content);
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
            var listLanguages = JsonConvert.DeserializeObject<NewsArchiveLanguagesResponse>(response.Content);
            Assert.That(listLanguages.ItemCount, Is.GreaterThan(0), "No Languages found");
        }

        [When(@"I perform a GET on NewsArchiveSources endpoint")]
        public void WhenIPerformAGETOnNewsArchiveSourcesEndpoint()
        {
            var response = new NewsArchiveService(SessionKey).GetAvailableSources();
            PropertyBucket.Remember(GET_NEWS_ARCHIVE_SOURCES_KEY, response);
        }

        [Then(@"NewsArchiveSources endpoint GET response code should be (.*) and returned data")]
        public void ThenNewsArchiveSourcesEndpointGETResponseCodeShouldBeAndReturnedData(int responseCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsArchiveSourcesResponse>>(GET_NEWS_ARCHIVE_SOURCES_KEY);
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
            Assert.NotNull(response, "News Archive Sources GET transaction failed", response.Content);
            var listSources = JsonConvert.DeserializeObject<NewsArchiveSourcesResponse>(response.Content);
            Assert.That(listSources.ItemCount, Is.GreaterThan(0), "No Sources found");
        }        
          
        [Then(@"Sample MediaMonitoring Search endpoint POST response code should be (.*)")]
        public void ThenSampleMediaMonitoringSearchEndpointPOSTResponseCodeShouldBe(int responseCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<SampleKeywordSearchResponse>>(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
            Assert.NotNull(response, "Sample Keyword Searches POST transaction failed", response.Content);
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
        }

        [Then(@"Verify return results are correct for '(.*)'")]
        public void ThenVerifyReturnResultsAreCorrect(KeywordSearchNewType keywordSearchNewType)
        {


            var createdSampleKeywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<SampleKeywordSearchResponse>>(POST_SAMPLE_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
            var createdSampleKeywordSearch = JsonConvert.DeserializeObject<SampleKeywordSearchResponse>(createdSampleKeywordSearchResponse.Content);
            string expectedSearchWindow = PropertyBucket.GetProperty<string>(TEST_DATA_SEARCH_WINDOW_KEY);         ;
            Assert.AreEqual(expectedSearchWindow, createdSampleKeywordSearch.SearchWindow.ToString());
            Assert.That(createdSampleKeywordSearch.ItemCount, Is.GreaterThan(0), "Item count should be greater than 0");
            Assert.That(createdSampleKeywordSearch.TotalCount, Is.GreaterThan(0), "Total count should be greater than 0");
            Assert.That(createdSampleKeywordSearch.ActiveCount, Is.GreaterThan(0), "Active count should be greater than 0");
            if (keywordSearchNewType.Equals(KeywordSearchNewType.SAMPLELIMIT))
                {   
                    Assert.IsTrue(createdSampleKeywordSearch.ResultLimitReached, "This search should reach limit");
                }
                else {
                    Assert.IsFalse(createdSampleKeywordSearch.ResultLimitReached, "This search shouldn't reach limit");
            }
            Assert.IsNotNull(createdSampleKeywordSearch.Key, "Key shoudn't be null");
        }
        [Then(@"returned message is '(.*)'")]
        public void ThenReturnedMessageIs(string expectedMessage)
        {
            var createdSampleKeywordSearchResponse = PropertyBucket.GetProperty<IRestResponse<MediaMonitorUserSearch>>(POST_KEYWORDSEARCHES_CREATE_RESPONSE_KEY);
            Assert.IsTrue(createdSampleKeywordSearchResponse.Content.Contains(expectedMessage), "Validation message differ from expected");
        }
    }
}