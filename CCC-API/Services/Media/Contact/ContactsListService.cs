using CCC_API.Data.PostData.Media.Contact;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Utils;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CCC_API.Services.Media.Contact
{
    public class ContactsListService : AuthApiService
    {
        public ContactsListService(string sessionKey) : base(sessionKey) { }

        public static string CreateContactsListEndpoint = "media/contacts/lists";
        public static string DeleteContactsListEndpoint = "entitylist";
        public static string DeleteContactsInListEnpoint = "media/contacts/lists";
        public static string RetrieveContactsListsEndpoint = "entitylist/filter";

        /// <summary>
        /// Post request for creating a new list
        /// </summary>
        /// <param name="key">The key from the search response</param>
        /// <param name="delta">A list of contacts to add to a list</param>
        /// <param name="name">The name of the new list</param>
        /// <returns>A response with the Id and Name of the new list</returns>
        public IRestResponse<Lists> PostContactsList(string key, int[] delta, string name)
        {
            var postData = new ContactsList()
            {
                Key = key,
                SelectAll = false,
                Delta = delta,
                Name = name
            };

            return Post<Lists>(CreateContactsListEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// Delete request that deletes a list by list id
        /// </summary>
        /// <param name="listId">The id of the list</param>
        /// <returns>A response with the name and id of the list that was deleted</returns>
        public IRestResponse<Int64> DeleteContactsList(int listId)
        {
            return Delete<Int64>(ApiEndpoints.UriBaseDomain, GetAuthorizationHeader(), DeleteContactsListEndpoint + "/" + listId);
        }

        /// <summary>
        /// Delete request for deleting a list
        /// </summary>
        /// <param name="key">The key from the search response</param>
        /// <param name="delta">The contacts to select</param>
        /// <param name="listName">The name of the list to delete</param>
        /// <param name="listId">The id of the list to delete</param>
        /// <returns>A response with the Id and Name of the list</returns>
        public IRestResponse<Lists> DeleteContactsInAList(string key, int[] delta, string listName, int listId)
        {
            var postData = new DeleteContactsInAList
            {
                Key = key,
                SelectAll = false,
                Delta = delta,
                Name = listName,
                ListId = listId
            };

            return Post<Lists>(DeleteContactsInListEnpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// Post request to retrieve all lists of a type (contact or outlet)
        /// </summary>
        /// <param name="entityType">The type of lists to retrieve</param>
        /// <returns>A response with a list of List objects</returns>
        public IRestResponse<EntityListFilter> PostEntityListsFilter(string entityType)
        {
            var postData = new EntityListFilterBody
            {
                UnlimitedResults = true,
                IncludePublicAndShared = true,
                FilterType = "0",
                EntityType = entityType
            };

            return Post<EntityListFilter>(RetrieveContactsListsEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// Post request to retrieve all lists of a type (contact or outlet)
        /// </summary>
        /// <param name="entityType">The type of lists to retrieve</param>
        /// <returns>A response with a list of List objects</returns>
        public IRestResponse<EntityListFilter> PostEntityListsFilterByUser(string entityType)
        {
            var postData = new EntityListFilterBody
            {
                UnlimitedResults = true,
                IncludePublicAndShared = true,
                FilterType = "User",
                EntityType = entityType
            };

            return Post<EntityListFilter>(RetrieveContactsListsEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// Post request to retrieve all lists of a type (contact or outlet) and
        /// the Entity ID 
        /// </summary>
        /// <param name="entityType">The type of lists to retrieve</param>
        /// <returns>A response with a list of List objects sorted descending as a default</returns>
        public IRestResponse<EntityListFilter> PostEntityListsFilterByEntityId(string entityType, int id)
        {
            var postData = new EntityListFilterBody
            {
                UnlimitedResults = true,
                IncludePublicAndShared = true,
                MembershipEntityId = id,
                FilterType = "User",
                SortField = "Name",
                EntityType = entityType
            };

            return Post<EntityListFilter>(RetrieveContactsListsEndpoint, GetAuthorizationHeader(), postData);
        }
        /// <summary>
        /// Delete request for multiple lists
        /// </summary>
        /// <param name="id1">the id of the first list</param>
        /// <param name="id2">the id of the second list</param>
        /// <returns></returns>
        public IRestResponse<List<int>> DeleteMultipleList(IEnumerable<int> ids)  
        {          
            var deleteData = new DeleteListData        
            {
                SelectAll = false,
                Delta = ids,
                Filter = new Filter
                {
                    EntityType = "MediaContact",
                    FilterType = "User"
                }
            };
            return Request().Delete().ToEndPoint($"{DeleteContactsListEndpoint}").Data(deleteData).Exec<List<int>>();

        }

        public IRestResponse<EntityList> DuplicateList(int originalId, int dataGroup, string newName, string entity)
        {
            var duplicateData = new DuplicateListData
            {
                OriginalListId = originalId,
                DestinationDataGroupId = dataGroup,
                Name = newName,
                EntityType = entity
            };
            return Request().Post().ToEndPoint($"{DeleteContactsListEndpoint}/duplicate").Data(duplicateData).Exec<EntityList>();
        }

        /// <summary>
        /// Post request to retrieve all lists of a type (contact or outlet) by sort direction
        /// </summary>
        /// <param name="entityType">The type of lists to retrieve</param>
        /// <returns>A response with a list of List objects</returns>
        public IRestResponse<EntityListFilter> PostEntityListsFilterBySort(string entityType, string field, int direction)
        {
            var postData = new EntityListFilterBody
            {
                UnlimitedResults = true,
                IncludePublicAndShared = true,
                FilterType = "0",
                EntityType = entityType,
                SortField = field,
                SortDirection = direction
            };

            return Post<EntityListFilter>(RetrieveContactsListsEndpoint, GetAuthorizationHeader(), postData);
        }
    }
}
