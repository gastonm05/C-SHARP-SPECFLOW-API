using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.News
{
    public class NewsTagsSteps : AuthApiSteps
    {
        private NewsTagsService _newsTagService;
        private NewsViewService _newsViewService;
        private Teardown _td;
        private const string GET_TAG_RESPONSE_KEY = "GetSingleTagResponse";
        private const string GET_UPDATED_TAG_RESPONSE_KEY = "GetUpdatedSingleTagResponse";
        private const string GET_CREATE_NEW_TAG_KEY = "GetCreateNewTagKey";
        private const string GET_DELETE_TAG_KEY = "GetDeleteTagKey";
        private const string GET_TYPED_NEWS_RESPONSE = "GetTypedNewsResponse";
        private const string GET_CREATE_DUPLICATE_NEW_TAG_KEY = "GetCreateDuplicateNewTagKey";
        private const string NEWS_ITEMS_KEY = "NewsItemsKey";
        private const string PATCH_BULK_TAGS_REPLACE_RESPONSE = "PatchBulkTagsReplaceResponse";
        private const string PATCH_BULK_TAGS_REMOVE_RESPONSE = "PatchBulkTagsRemoveResponse";
        private const string PATCH_BULK_TAGS_APPEND_RESPONSE = "PatchBulkTagsAppendResponse";

        public NewsTagsSteps(IObjectContainer objectContainer, Teardown teardown) : base(objectContainer)
        {
            _newsTagService = new NewsTagsService(SessionKey);
            _newsViewService = new NewsViewService(SessionKey);
            _td = teardown;
        }

        #region When Steps
        [When(@"I perform a GET to the tags endpoint")]
        public void WhenIPerformAGETToTheTagsEndpoint()
        {
            var tags = _newsTagService.GetTags();
            PropertyBucket.Remember("tags", tags);
        }

        [When(@"I perform a PATCH to update tag name")]
        public void WhenIPerformAPatchToUpdateTagName()
        {
            //GET the previously created tag item's id
            var id = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_CREATE_NEW_TAG_KEY).Data.Item.Id;
            //PATCH the previously created tag item
            var response = _newsTagService.PatchNewsTag(id);
            PropertyBucket.Remember(GET_UPDATED_TAG_RESPONSE_KEY, response);
        }

        [When(@"I perform a POST to create a new tag with name '(.*)'")]
        public void WhenIPerformAPostToCreateNewTag(string tagName)
        {
            var response = _newsTagService.CreateNewTag(tagName);
            PropertyBucket.Remember(GET_CREATE_NEW_TAG_KEY, response, true);
        }

        [When(@"I create the tag '(.*)'")]
        public void WhenICreateTheTag(string tagName)
        {
            var response = _newsTagService.CreateNewTag(tagName);
            PropertyBucket.Remember(GET_CREATE_NEW_TAG_KEY, response);
            var tagNames = new List<string>();
            tagNames.Add(tagName);
            _td.AddTeardown(() => _newsTagService.DeleteNewsTags(tagNames));
        }

        [When(@"I search for typed news by '(.*)' with a value of '(.*)'")]
        public void WhenISearchForTypedNewsByAGivenCriteriaWithAGivenValue(NewsViewService.NewsSearchCriteria searchCriteria, string value)
        {
            var response = _newsViewService.GetNewsWithParameters(searchCriteria, value);
            PropertyBucket.Remember(GET_TYPED_NEWS_RESPONSE, response);
        }

        [When(@"I perform a POST to create a duplicate tag with name '(.*)'")]
        public void WhenIPerformAPOSTToCreateADuplicateTagWithName(string tagName)
        {
            var response = _newsTagService.CreateNewTag(tagName);
            PropertyBucket.Remember(GET_CREATE_DUPLICATE_NEW_TAG_KEY, response);
        }

        [When(@"I bulk add the tag '(.*)' to the first '(.*)' news items")]
        public void WhenIBulkAddTheTagToTheFirstNewsItems(string tag, int count)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(NewsViewEndpointSteps.GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items.TakeExactly(count);
            var ids = items.Select(i => i.Id).ToArray();
            var tagId = _newsTagService.GetTagIdByName(tag);
            PropertyBucket.Remember(PATCH_BULK_TAGS_APPEND_RESPONSE, _newsTagService.BulkTagNewsItems(response.Data.Key, tagId, ids));
        }

        [When(@"I perform GET for all available tags")]
        public void WhenIPerformGETForAllAvailableTags()
        {
            var response = _newsTagService.GetAllTags();
            PropertyBucket.Remember(GET_TAG_RESPONSE_KEY, response);
        }

        [When(@"I perform a PATCH to bulk replace with tag named '(.*)'")]
        public void WhenIPerformAPATCHToBulkReplaceWithTagNamed(string tagName)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(NewsViewEndpointSteps.GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items.TakeExactly(5);
            var ids = items.Select(i => i.Id).ToArray();
            PropertyBucket.Remember(NEWS_ITEMS_KEY, items);
            PropertyBucket.Remember(PATCH_BULK_TAGS_REPLACE_RESPONSE, _newsTagService.PatchNewsWithTagsMultipleOperations("update", response.Data.Key, tagName, ids));
        }

        [When(@"I perform a PATCH to bulk remove tag with name '(.*)'")]
        public void WhenIPerformAPATCHToBulkRemoveTagWithName(string tagName)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(NewsViewEndpointSteps.GET_NEWS_RESPONSE_KEY);
            var items = PropertyBucket.GetProperty<IEnumerable<NewsItem>>(NEWS_ITEMS_KEY);
            var ids = items.Select(i => i.Id).ToArray();
            PropertyBucket.Remember(PATCH_BULK_TAGS_REMOVE_RESPONSE, _newsTagService.PatchNewsWithTagsMultipleOperations("remove", response.Data.Key, tagName, ids));
        }
        #endregion

        #region Then Steps
        [Then(@"there should be tags returned")]
        public void ThenThereShouldBeTagsReturned()
        {
            var tags = PropertyBucket.GetProperty<NewsTags>("tags");
            Assert.That(tags.ItemCount, Is.GreaterThan(0), "No tags were returned");
        }

        [Then(@"the Single Tag Endpoint response should be '(.*)'")]
        public void ThenTheSingleTagEndpointResponseShouldBeAGivenCode(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsTag>>(GET_UPDATED_TAG_RESPONSE_KEY);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see that the tag name was updated")]
        public void ThenISHouldSeeThatTheTagNameWasUpdated()
        {
            var singleTag = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_CREATE_NEW_TAG_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<NewsTag>>(GET_UPDATED_TAG_RESPONSE_KEY);
            Assert.AreNotEqual(singleTag.Data.Item.Name, response.Data.Name, "Tag Name was not updated");
        }

        [Then(@"I should see the tags endpoint has the correct response for new tag")]
        public void ThenIShouldSeeTheTagsEndpointHasTheCorrectResponseForNewTag()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_CREATE_NEW_TAG_KEY);
            NewsTagItem newTag = response.Data;
            Assert.NotNull(newTag.Item.Id);
            Assert.NotNull(newTag.Item.Name);
            Assert.AreEqual(201, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see that the delete tag endpoint has the correct response")]
        public void ThenIShouldSeeThatTheDeleteTagEndpointhasTheCorrectResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_DELETE_TAG_KEY);
            Assert.AreEqual(204, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see the news endpoint has the correct response and it is including typed news")]
        public void ThenIShouldSeeTheNewsEndpointHasTheCorrectResponseForSmartTags()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_TYPED_NEWS_RESPONSE);
            List<NewsItem> items = response.Data.Items;
            Assert.True(_newsTagService.GetIsTypedNewsIncluded(items));
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the Create News Tags endpoint response status code should be '(.*)'")]
        public void ThenTheCreateNewsTagsEndpointResponseStatusCodeShouldBe(int status)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_CREATE_NEW_TAG_KEY);
            Assert.That(CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(status), "News Tag endpoint response incorrect");
        }

        [Then(@"I should see the tags endpoint has the correct response for creating duplicate tags")]
        public void ThenIShouldSeeTheTagsEndpointHasTheCorrectResponseForCreatingDuplicateTags()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_CREATE_DUPLICATE_NEW_TAG_KEY);
            Assert.That(CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(400), "News Tag endpoint response was incorrect for duplicate tag name");
        }
        
        [Then(@"I perform a DELETE to eliminate recently created tag")]
        public void WhenIPerformADeleteToEliminateRecentlyCreatedTag()
        {
            var id = PropertyBucket.GetProperty<IRestResponse<NewsTagItem>>(GET_CREATE_NEW_TAG_KEY).Data.Item.Id;
            var ids = new List<int>
            {
                id
            };
            var response = _newsTagService.DeleteNewsTags(ids);
            PropertyBucket.Remember(GET_DELETE_TAG_KEY, response);
        }

        [Then(@"I should see the max length for a tag name is '(.*)' characters")]
        public void ThenIShouldSeeTheMaxLengthForATagNameIsCharacters(int maxLength)
        {
            var response = PropertyBucket.GetProperty<NewsTagsQuery>(GET_TAG_RESPONSE_KEY);
             Assert.That(response._meta.MaxLength.Equals(maxLength), Is.True, $"Max Length {response._meta.MaxLength} does not match the expected value of {maxLength}");
        }

        [Then(@"I should see that the news endpoint has the correct response for replacing tags")]
        public void ThenIShouldSeeThatTheNewsEndpointHasTheCorrectResponseForReplacingTags()
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(PATCH_BULK_TAGS_REPLACE_RESPONSE);
            Assert.AreEqual(204, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see that the news endpoint has the correct response for removing tags")]
        public void ThenIShouldSeeThatTheNewsEndpointHasTheCorrectResponseForRemovingTags()
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(PATCH_BULK_TAGS_REMOVE_RESPONSE);
            Assert.AreEqual(204, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see that the news endpoint has the correct response for appending tags")]
        public void ThenIShouldSeeThatTheNewsEndpointHasTheCorrectResponseForAppendingTags()
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(PATCH_BULK_TAGS_APPEND_RESPONSE);
            Assert.AreEqual(204, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }
        #endregion
    }
}
