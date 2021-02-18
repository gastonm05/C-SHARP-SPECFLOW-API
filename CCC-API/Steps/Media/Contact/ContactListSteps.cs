using BoDi;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Outlet;
using CCC_API.Services.Media;
using CCC_API.Services.Media.Contact;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.Contact
{
    [Binding]
    public class ContactListSteps : AuthApiSteps
    {
        public ContactListSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string POST_ENTITY_LISTS_FILTER_KEY = "PostEntityListsFilter";
        public const string OUTLET_ID = "Outlet ID";
        private const string DELETE_LIST_BY_ID = "DeleteListById";
        private const string GET_RECENT_TWEETS_RESPONSE = "Recent Tweets";
        private const string MULTIPLE_DELETE_LISTS = "MULTIPLE DELETE";

        #region When Steps

        [When(@"I perform a GET for recent tweets by list name '(.*)'")]
        public void WhenIPerformAGETForRecentTweetsByListName(string listName)
        {
            var response = new ContactsService(SessionKey).GetRecentTweetsByListName(listName);
            PropertyBucket.Remember(GET_RECENT_TWEETS_RESPONSE, response);
        }

        [When(@"I perform a GET for recent tweets by list id for the list '(.*)'")]
        public void WhenIPerformAGETForRecentTweetsByListIdForTheList(string listName)
        {
            var listService = new EntityListService(SessionKey);
            var lists = listService.GetAvaliableMediaContactsLists();
            var id = lists.Single(i => i.Name.ToLower().Equals(listName.ToLower())).Id;
            var response = new ContactsService(SessionKey).GetRecentTweetsByListId(id);
            PropertyBucket.Remember(GET_RECENT_TWEETS_RESPONSE, response);
        }

        [When(@"I perform a POST for searching all '(.*)' lists")]
        public void WhenIPerformAPOSTForSearchingForAllLists(string mediaType)
        {
            var response = new ContactsListService(SessionKey).PostEntityListsFilterByUser(mediaType);
            PropertyBucket.Remember(POST_ENTITY_LISTS_FILTER_KEY, response);
        }      

        [When(@"I perform a POST for searching all '(.*)' lists and using the '(.*)' id sorted by name")]
        public void WhenIPerformAPOSTForSearchingAllListsAndUsingTheChicagoOnlineIdSortedByName(string mediaType, string outlet)
        {
            int id;
            var outlets = TestData.DeserializedJson<List<OutletsItem>>("Outlets.json", Assembly.GetExecutingAssembly());
                       
            try
            {
                id = outlets.Find(o => o.FullName.ToLower().Equals(outlet.ToLower())).Id;
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException(Err.Msg($"'{outlets}' not found in Outlets.json file."));
            }
            PropertyBucket.Remember(OUTLET_ID, id);
            var response = new ContactsListService(SessionKey).PostEntityListsFilterByEntityId(mediaType, id);
            PropertyBucket.Remember(POST_ENTITY_LISTS_FILTER_KEY, response);
        }

        [When(@"I delete the first two list ids")]
        public void WhenIDeleteTheFirstTwoListIds()
        {
            var items = PropertyBucket.GetProperty<IRestResponse<EntityListFilter>>(POST_ENTITY_LISTS_FILTER_KEY);
            var ids = items.Data.Results.Select(p => p.Id).TakeExactly(2);
            var response = new ContactsListService(SessionKey).DeleteMultipleList(ids);
            PropertyBucket.Remember(MULTIPLE_DELETE_LISTS, response);                  
        }
        #endregion

        #region Then Steps
        [Then(@"the response should have recent tweets")]
        public void ThenTheResponseShouldHaveRecentTweets()
        {
            IRestResponse<Tweets> response = PropertyBucket.GetProperty<IRestResponse<Tweets>>(GET_RECENT_TWEETS_RESPONSE);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "Failed to retrieve recent tweets");
        }

        [Then(@"the response should not have recent tweets")]
        public void ThenTheResponseShouldNotHaveRecentTweets()
        {
            IRestResponse<Tweets> response = PropertyBucket.GetProperty<IRestResponse<Tweets>>(GET_RECENT_TWEETS_RESPONSE);
            Assert.That(response.Data.ItemCount, Is.EqualTo(0), "Request returned tweets and should not have");
        }


        [Then(@"the Contact Lists Endpoint response code should be '(.*)'")]
        public void ThenTheContactListsEndpointResponseCodeShouldBe(int code)
        {
            IRestResponse<Tweets> response = PropertyBucket.GetProperty<IRestResponse<Tweets>>(GET_RECENT_TWEETS_RESPONSE);
            Assert.That(Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(code), "Incorrect status returned for request");
        }

        [Then(@"all returned lists should contain not null values for the owner, modified date and creation date")]
        public void ThenAllReturnedListsShouldContainNotNullValuesForTheOwnerModifiedDateAndCreationDate()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<EntityListFilter>>(POST_ENTITY_LISTS_FILTER_KEY);
            Assert.That(response.Data.TotalCount, Is.GreaterThan(0), "The Post returned no data!");
            foreach (var lists in response.Data.Results)
            {
                Assert.That(lists.Id, Is.Not.Null, "The id are null");
                Assert.That(lists.LastModifiedDate, Is.Not.Null, "The Last Modified dates are null");
                Assert.That(lists.CreationDate, Is.Not.Null, "The Creation dates are null");
                Assert.That(lists.Owner, Is.Not.Null.Or.Empty, "The owner name was not valid");
            }
        }

        [Then(@"all returned lists should have the id of the outlet in the response")]
        public void ThenAllReturnedListsShouldHaveTheIdOfTheOutletInTheResponse()
        {
            var  response = PropertyBucket.GetProperty<IRestResponse<EntityListFilter>>(POST_ENTITY_LISTS_FILTER_KEY);          
            var id =  PropertyBucket.GetProperty(OUTLET_ID);                     
            Assert.That(response.Data.TotalCount, Is.GreaterThan(0), "The Post returned no data!");
            Assert.True(response.Data.Results.All(l => l.Membership.EntityId.Equals(id)), "The Ids are not the same");
        }

        [Then(@"I should not get null response after multiple list deleted")]
        public void ThenIShouldNotGetNullResponseAfterMultipleListDeleted()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<int>>>(MULTIPLE_DELETE_LISTS);
            var items = response.Data;
            Assert.That(items.Count, Is.GreaterThan(0), "No Ids returned in response");
            //int cannot be null, if so exception will be thrown. It may be possible for default value (0) to be returned.
            Assert.IsTrue(items.All(i => i != 0), "At least one id returned as zero"); 
        }
        #endregion
    }
}

