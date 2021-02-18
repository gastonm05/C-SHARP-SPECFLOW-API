using CCC_API.Data;
using CCC_API.Data.PostData.Analytics;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Common;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CCC_API.Services.Analytics
{
    public class SearchesService : AuthApiService
    {
        public static string AnalyticsSearchesEndPoint = "news/analytics/searches/";
        public static string AnalyticsSearchesGroupEndPoint = "news/analytics/searches/group/";
        public static string AnalyticsSearchesGroupsEndPoint = "news/analytics/searches/groups/";

        private readonly string _sessionKey;

        public SearchesService(string sessionKey) : base(sessionKey)
        {
            _sessionKey = sessionKey;
        }

        /// <summary>
        /// Gets the searches.
        /// </summary>
        /// <returns></returns>
        public SearchesResponse GetSearches()
        {
            try
            {
                return Request().Get().ToEndPoint(AnalyticsSearchesEndPoint).ExecContentCheck<SearchesResponse>();
            }
            catch (Exception e) // operation has timed out
            {
                // try again for timeout only
                if (e.Message.Contains("The operation has timed out"))
                {
                    return Request().Get().ToEndPoint(AnalyticsSearchesEndPoint).ExecContentCheck<SearchesResponse>();
                }
                else
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the search.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AnalyticsSearch GetSearch(int id)
        {
            return Request().Get().ToEndPoint($"{AnalyticsSearchesEndPoint}{id}").ExecCheck<AnalyticsSearch>();
        }

        /// <summary>
        /// Creates the search.
        /// </summary>
        /// <param name="newSearch">The new search.</param>
        public void CreateSearch(AnalyticsSearch newSearch)
        {
            Request().Post().ToEndPoint(AnalyticsSearchesEndPoint).Data(newSearch).ExecCheck();
        }

        /// <summary>
        /// Deletes the search.
        /// </summary>
        /// <param name="id">The search id.</param>
        public void DeleteSearch(int id)
        {
            Request().Delete().ToEndPoint($"{AnalyticsSearchesEndPoint}{id}").ExecCheck();
        }

        /// <summary>
        /// Given an AnalyticsSearch test object, adds the search to the group via the api and updates the AnalyticsSearch test object
        /// </summary>
        /// <param name="search">The analytics search.</param>
        /// <param name="group">The analytics search group.</param>
        public void AddSearchToGroup(AnalyticsSearch search, AnalyticsSearchGroup group)
        {
            var patchBody = new PatchData[] { new PatchData("add", "groups", group.Id.ToString()) };
            Request().Patch().ToEndPoint($"{AnalyticsSearchesEndPoint}{search.SearchId}").Data(patchBody).ExecCheck();
            if (search.GroupIds == null)
            {
                search.GroupIds = new List<int>();
            }
            if (!search.GroupIds.Contains(group.Id))
            {
                search.GroupIds.Add(group.Id);
            }
        }

        /// <summary>
        /// Given an AnalyticsSearch test object, removes the search from the group via the api and updates the AnalyticsSearch test object
        /// </summary>
        /// <param name="search">The analytics search.</param>
        /// <param name="group">The analytics search group.</param>
        public void RemoveSearchFromGroup(AnalyticsSearch search, AnalyticsSearchGroup group)
        {
            var patchBody = new PatchData[] { new PatchData("remove", "groups", group.Id.ToString()) };
            Request().Patch().ToEndPoint($"{AnalyticsSearchesEndPoint}{search.SearchId}").Data(patchBody).ExecCheck();
            if (search.GroupIds != null)
            {
                search.GroupIds.Remove(group.Id);
            }
        }

        /// <summary>
        /// Gets the search groups.
        /// </summary>
        /// <returns></returns>
        public List<AnalyticsSearchGroup> GetSearchGroups()
        {
            return Request().Get().ToEndPoint(AnalyticsSearchesGroupsEndPoint).ExecContentCheck<List<AnalyticsSearchGroup>>();
        }

        /// <summary>
        /// Creates the search group.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="name">The name.</param>
        /// <param name="groupType">Type of the group.</param>
        /// <returns></returns>
        public AnalyticsSearchGroupPostBody CreateSearchGroup(string name, string groupType)
        {
            var accountInfoService = new AccountInfoService(_sessionKey);
            AnalyticsSearchGroupPostBody body = new AnalyticsSearchGroupPostBody()
            {
                Name = name,
                CategoryId = -4
            };
            Request().Post().ToEndPoint(AnalyticsSearchesGroupEndPoint).Data(body).ExecCheck();
            return body;
        }

        /// <summary>
        /// Deletes the search group.
        /// </summary>
        /// <param name="id">The search group id.</param>
        public void DeleteSearchGroup(int id)
        {
            try
            {
                Request().Delete().ToEndPoint($"{AnalyticsSearchesGroupEndPoint}{id}").ExecCheck();
            }
            catch (Exception e) // operation has timed out
            {
                if (e.Message.Contains("The operation has timed out"))
                {
                    // try again for timeout only
                    Request().Delete().ToEndPoint($"{AnalyticsSearchesGroupEndPoint}{id}").ExecCheck();
                }
                else
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Creates the search from a readonly user session
        /// </summary>
        /// <param name="newSearch">403/Unauthorized</param>
        public IRestResponse<AnalyticsSearch> CreateSearchReadOnly(string name) 
        {
            var postData = new AnalyticsSearch()
            {
                SearchName = name,
                SearchType = "C"
            };
            return Post<AnalyticsSearch>(AnalyticsSearchesEndPoint, GetAuthorizationHeader(), postData);
        }
    }
}
