using CCC_API.Data;
using CCC_API.Data.PostData.Media.Contact;
using CCC_API.Data.PostData.Media.Outlet;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Contact;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Data.Responses.Media.Outlet;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace CCC_API.Services.Media.Outlet
{
    public class OutletsService : AuthApiService
    {
        public static string OutletsEndpoint = "media/outlets";
        public static string OutletFacetsEndpoint = "media/outlets/facets";
        public static string OutletRecentSearch = "/searches/recent";
        public static string OutletSavedSearch = "/searches";
        public static string OutletReferralsEndpoint = "media/outlets/referrals";
        public static string OuletListEndpoint = "media/outlets/lists";
        public static string RetrieveOutletsListsEndpoint = "entitylist/filter";
        public static string RecentTweets = "/Twitter/RecentTweets";
        public static string DuplicateListEndpoint = "entitylist";



        public OutletsService(string sessionKey) : base(sessionKey)
        {
        }
        public enum OutletSearchCriteria
        {
            OutletName,
            OutletLocation,
            OutletType,
            Subject,
            OutletListName,
            DMA,
            Outlet_NewsFocus,
            Outlet_WorkingLanguage,
            Outlet_Frecuency,
            Outlet_UVPM,
            Outlet_AudienceReach,
            Outlet_RecordType,
            Outlet_NewsOnlyOutlets,
            Outlet_LocationRadius,
            Outlet_Ids,
            OutletTarget_Ids,
            OutletOptedOut

        };
        private Dictionary<OutletSearchCriteria, string> map = new Dictionary<OutletSearchCriteria, string>() {
                        { OutletSearchCriteria.OutletName,"name"},
                        { OutletSearchCriteria.OutletLocation,"location.placeid"},
                        { OutletSearchCriteria.OutletType,"outletTypeIds"},
                        { OutletSearchCriteria.Subject,"subjects"},
                        { OutletSearchCriteria.DMA,"OutletDMAIds"},
                        { OutletSearchCriteria.OutletListName,"listname"},
                        { OutletSearchCriteria.Outlet_NewsFocus,"outletnewsfocus" },
                        { OutletSearchCriteria.Outlet_WorkingLanguage,"workingLanguageIds" },
                        { OutletSearchCriteria.Outlet_Frecuency,"outletFrequencyIds" },
                        { OutletSearchCriteria.Outlet_RecordType,"recordType" },
                        { OutletSearchCriteria.Outlet_NewsOnlyOutlets,"includeNODOnlyOutlets"},
                        { OutletSearchCriteria.Outlet_LocationRadius,"location.radius"},
                        { OutletSearchCriteria.Outlet_Ids,"ids"},
                        { OutletSearchCriteria.OutletTarget_Ids,"TargetCoveragePlaceIds"},
                        { OutletSearchCriteria.OutletOptedOut,"optedOut"}
                    };

        /// <summary>
        /// Get request for Outlets by the passed in criteria. 
        /// This will throw an exception if the criteria argument doesn't match a valid criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<Outlets> GetOutlets(OutletSearchCriteria criteria, string parameter)
        {
            return Get<Outlets>($"{OutletsEndpoint}?{map[criteria]}={parameter}");
        }

        /// <summary>
        /// Search Outlets by Audience Reach
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IRestResponse<Outlets> GetOutletsReach(OutletSearchCriteria criteria, int start, int end)
        {
            if (criteria.Equals(OutletSearchCriteria.Outlet_UVPM))
            {
                return Get<Outlets>($"{OutletsEndpoint}?UniqueVisitorsPerMonth.start={start}&UniqueVisitorsPerMonth.end={end}");
            }
            else if (criteria.Equals(OutletSearchCriteria.Outlet_AudienceReach))
            {
                return Get<Outlets>($"{OutletsEndpoint}?AudienceReach.start={start}&AudienceReach.end={end}");
            }

            throw new ArgumentException(Err.Msg($"'{criteria}' is not a valid criteria for Outlet Reach Search"));
        }

        /// <summary>
        /// Get Outlet by id. 
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>=
        public IRestResponse<OutletsItem> GetOutlet(int id)
        {
            return Get<OutletsItem>($"{OutletsEndpoint}/{id}");
        }

        /// <summary>
        /// Gets multiple outlets
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IRestResponse<Outlets> GetOutletsUsingIds(List<int> ids)
        {
            var formattedIds = new StringBuilder();
            foreach (var id in ids)
            {
                formattedIds.Append($"{id.ToString()},");
            }
            var idString = formattedIds.ToString().TrimEnd(',');
            return Get<Outlets>($"{OutletsEndpoint}?ids={idString}");
        }
        /// <summary>
        /// Gets multiple outlets
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IRestResponse<Outlets> GetOutlets(int[] ids)
        {
            var formattedIds = new StringBuilder();
            foreach (var id in ids)
            {
                formattedIds.Append($"{id.ToString()},");
            }
            var idString = formattedIds.ToString().TrimEnd(',');
            return Get<Outlets>($"{OutletsEndpoint}?ids={idString}&disableFaceting=true");
        }

        /// <summary>
        /// Gets outlets by criteria and throws Assertion error if something goes wrong.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="parameter"></param>
        /// <returns>Outlets</returns>
        public Outlets FindOutlets(OutletSearchCriteria criteria, string parameter)
        {
            var resp = GetOutlets(criteria, parameter);
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode, "status not OK");
            return resp.Data;
        }

        public IRestResponse<OutletSuggestion> GetSuggestion(string param)
        {
            return Get<OutletSuggestion>($"{OutletsEndpoint}/suggestions?name={param}");
        }

        /// <summary>
        /// Gets the facets for a given search key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IRestResponse<Facets> GetFacets(string key)
        {
            return Get<Facets>($"{OutletFacetsEndpoint}?key={key}");
        }

        /// <summary>
        /// Get request for Outlets using an existing key and a facet value
        /// </summary>
        /// <param name="key">The key of an existing search.</param>
        /// <param name="facet">The facet to apply.</param>
        /// <returns></returns>        
        public IRestResponse<Outlets> GetFilterResults(string key, string facet)
        {
            return Get<Outlets>($"{OutletsEndpoint}?key={key}&facets={facet}");
        }

        /// <summary>
        /// Post a new recent searches
        /// </summary>
        /// <param name="key">The key generated in the previous search</param>
        /// <returns>201 created</returns>
        public IRestResponse<MediaSavedSearchItem> PostRecentSearch(string key)
        {
            var postData = new MediaSavedSearchPostData() { Key = key };
            return Request().Post().ToEndPoint($"{OutletsEndpoint}{OutletRecentSearch}").Data(postData).Exec<MediaSavedSearchItem>();
        }
        /// <summary>
        /// Get all recent media contact searches
        /// </summary>
        /// <returns>200 OK</returns>
        public IRestResponse<MediaSavedSearch> GetRecentSearches()
        {
            return Request().Get().ToEndPoint($"{OutletsEndpoint}{OutletRecentSearch}").Exec<MediaSavedSearch>();
        }

        /// <summary>
        /// Post a new saved searches
        /// </summary>
        /// <param name="key">The key generated in the previous search</param>
        /// <returns>201 created</returns>
        public IRestResponse<MediaSavedSearchItem> PostSaveSearch(string key, string name)
        {
            var postData = new MediaSavedSearchPostData() { Key = key, Name = name };
            return Request().Post().ToEndPoint($"{OutletsEndpoint}{OutletSavedSearch}").Data(postData).Exec<MediaSavedSearchItem>();
        }
        /// <summary>
        /// Get all saved media contact searches
        /// </summary>
        /// <returns>200 OK</returns>
        public IRestResponse<MediaSavedSearch> GetSavedSearches()
        {
            return Request().Get().ToEndPoint($"{OutletsEndpoint}{OutletSavedSearch}?page=1&limit=100").Exec<MediaSavedSearch>();
        }

        /// <summary>
        /// Delete a saved search by id 
        /// </summary>
        /// <param name="id">The id of the search to delete</param>
        /// <returns></returns>
        public IRestResponse<DeleteItem> DeleteSavedSearch(int id)
        {
            return Request().Delete().ToEndPoint($"{OutletsEndpoint}{OutletSavedSearch}/{id}").Exec<DeleteItem>(); ;
        }

        /// <summary>
        /// Gets the referrals for a given source
        /// </summary>
        /// <param name="limit">Limit</param>
        /// <param name="source">Source</param>
        /// <returns></returns>
        public IRestResponse<Referrals> GetReferrals(int limit, string source)
        {
            return Get<Referrals>($"{OutletReferralsEndpoint}?limit={limit}&source={source}");
        }

        /// <summary>
        /// Edit the name of a saved search
        /// </summary>
        /// <param name="id">the id of the search to edit</param>
        /// <param name="name">the name to change</param>
        /// <returns></returns>
        public IRestResponse<MediaSavedSearchItem> EditSavedSearch(int id, string name)
        {
            var patchBody = new PatchData[] { new PatchData("update", "/Name", name) };
            return Request().Patch().ToEndPoint($"{OutletsEndpoint}{OutletSavedSearch}/{id}").Data(patchBody).Exec<MediaSavedSearchItem>();
        }
        /// <summary>
        /// Create an outlet list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRestResponse<Lists> PostOutletLists(string key, int[] delta, string name)
        {
            var postData = new OutletList()
            {
                Key = key,
                SelectAll = true,
                Delta = delta,
                Name = name
            };
            return Post<Lists>(OuletListEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// POST to create a new private outlet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public IRestResponse<OutletsItem> PostPrivateOutlet(string name, int countryId, string email = null)
        {
            var postData = new OutletItem()
            {
                FullName = name,
                CountryId = countryId,
                IsProprietaryOutlet = true,
                Email = email
            };
            return Post<OutletsItem>(OutletsEndpoint, GetAuthorizationHeader(), postData);
        }
        /// <summary>
        /// Edit the private outlet information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRestResponse<OutletsItem> EditPrivateOutlet(int id, string path, string name)
        {
            var patchBody = new PatchData[] { new PatchData("update", path, name) };
            return Request().Patch().ToEndPoint($"{OutletsEndpoint}/{id}").Data(patchBody).Exec<OutletsItem>();
        }
        /// <summary>
        /// Delete a private outlet
        /// </summary>
        /// <param name="id">the id of the outlet to be deleted</param>
        /// <returns></returns>
        public IRestResponse<DeleteItem> DeletePrivateOutlet(int id)
        {
            return Request().Delete().ToEndPoint($"{OutletsEndpoint}/{id}").Exec<DeleteItem>();
        }
        /// <summary>
        /// export outlets with field givan as a parameter
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public IRestResponse<JobResponse> ExportOutlets(string key, string field)
        {
            var postData = new OutletsExportData()
            {
                Key = key,
                PresentationType = 0,
                SelectAll = true,
                Fields = new string[] { field },
            };
            return Post<JobResponse>($"{OutletsEndpoint}/export", postData);
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

            return Post<EntityListFilter>(RetrieveOutletsListsEndpoint, GetAuthorizationHeader(), postData);
        }
        /// <summary>
        /// method to get recent tweets for an outlet
        /// </summary>
        /// <param name="id">the id of the outlet</param>
        /// <returns></returns>
        public IRestResponse<Tweets> GetRecentOutletTweets(int id)
        {
            return Request().Get().ToEndPoint($"{OutletsEndpoint}/{id}{RecentTweets}").Exec<Tweets>();
        }

        /// <summary>
        /// Get request for Outlets by the passed in criteria. 
        /// This will throw an exception if the criteria argument doesn't match a valid criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<Outlets> GetOutletsMultipleCriteria(IDictionary<OutletSearchCriteria, string> criteria)
        {
            var queryString = new List<string>();
            foreach (KeyValuePair<OutletSearchCriteria, string> criterion in criteria)
            {
                if (criterion.Key == OutletSearchCriteria.OutletName && !criterion.Value.Contains(","))
                {
                    string urlEncoded = criterion.Value.Replace(" ", "%20");
                    queryString.Add($"name={urlEncoded}&fields=id,sortname");
                }
                else
                {
                    queryString.Add($"{map[criterion.Key]}={criterion.Value}");
                }
            }
            return Get<Outlets>($"{OutletsEndpoint}?{string.Join("&", queryString)}");
        }
        /// <summary>
        /// Duplicate outlet list
        /// </summary>
        /// <param name="originalId"></param>
        /// <param name="dataGroup"></param>
        /// <param name="newName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IRestResponse<EntityList> DuplicateList(int originalId, int dataGroup, string newName, string entity)
        {
            var duplicateData = new DuplicateListData
            {
                OriginalListId = originalId,
                DestinationDataGroupId = dataGroup,
                Name = newName,
                EntityType = entity
            };
            return Request().Post().ToEndPoint($"{DuplicateListEndpoint}/duplicate").Data(duplicateData).Exec<EntityList>();
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

            return Post<EntityListFilter>(RetrieveOutletsListsEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// Edits the outlets's information. Serializes the list of PatchData objects into valid json
        /// </summary>
        /// <param name="id">The id for the outlet</param>
        /// <param name="list">List of PatchData objects</param>
        /// <returns></returns>
        public IRestResponse<OutletsItem> EditOutlet(int id, List<PatchData> list)
        {
            var patchString = JsonConvert.SerializeObject(list);
            return Patch<OutletsItem>($"{OutletsEndpoint}/{id}", patchString);
        }
    }
    }
