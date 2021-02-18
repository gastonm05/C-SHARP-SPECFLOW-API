using CCC_API.Data;
using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.News;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CCC_API.Services.News
{
    public class SavedSearchesService : AuthApiService
    {
        public static string SavedSearchesEndpoint = "news/searches";

        public SavedSearchesService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets all saved searches.
        /// </summary>
        /// <returns></returns>
        public SavedSearches GetAllSavedSearches() =>
            Request().Get().ToEndPoint(SavedSearchesEndpoint).ExecContentCheck<SavedSearches>();

        /// <summary>
        /// Gets a single saved search.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRestResponse GetSingleSavedSearch(int id) => 
            Get<SingleSavedSearch>($"{SavedSearchesEndpoint}/{id}");

        /// <summary>
        /// Creates a Saved Search
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRestResponse PostSavedSearch(string key, string name)
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMdd-HHMMss");
            var postData = new SavedSearchPostData()
            {
                Key = key,
                Name = name + "_" + timeStamp,
                Visibility = "company"
            };

            var response = Post<SingleSavedSearch>(SavedSearchesEndpoint, GetAuthorizationHeader(), postData);
            return response;
        }

        /// <summary>
        /// Deletes a Single Saved Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteSavedSearch(int id) =>
            Request().Delete().ToEndPoint($"{SavedSearchesEndpoint}/{id}").Exec();

        /// <summary>
        /// Gets a single saved search criteria.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRestResponse GetSingleSavedSearchCriteria(int id) =>
            Get<SavedSearchItemCriteria>($"{SavedSearchesEndpoint}/{id}/criteria");

        /// <summary>
        /// Patches a Saved Search replacing all existing criteria 
        /// values with newly given criteria 
        /// </summary>
        /// <param name="savedSearchId"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IRestResponse PatchSavedSearch(int savedSearchId, string searchKey)
        {
            List<PatchData> list = new List<PatchData>();
            var patchData = new PatchData()
            {
                Op = "Update",
                Path = "criteria",
                Value = searchKey
            };
            list.Add(patchData);
            var response = Patch<SingleSavedSearch>($"{SavedSearchesEndpoint}/{savedSearchId}", list);
            return response;
        }
    }
}