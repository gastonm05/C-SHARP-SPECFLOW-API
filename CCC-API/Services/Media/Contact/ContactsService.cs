using CCC_API.Data;
using CCC_API.Data.PostData.Media;
using CCC_API.Data.PostData.Media.Contact;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Contact;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Contact;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using CCC_API.Data.Responses.Streams;
using CCC_API.Data.TestDataObjects;
using System.Xml;
using System.Xml.Serialization;

namespace CCC_API.Services.Media.Contact
{
    public class ContactsService : AuthApiService
    {
        public ContactsService(string sessionKey) : base(sessionKey) { }

        public static string ContactsEndpoint = "media/contacts";
        public static string ContactsFacetsEndpoint = "media/contacts/facets";
        public static string ContactsListEndpoint = "media/contacts/lists";
        public static string ContactsExportEndpoint = "export";
        public static string ContactsInfluencerRankingEndpoint = "influencerrankings";
        public static string ContactsRelated = "related";
        public static string ContactsHistory = "history";
        public static string ContactRecentSearch = "/searches/recent";
        public static string ContactSavedSearch = "/searches";
        public static string RecentTweets = "/Twitter/RecentTweets";
        public static string OptinRequests = "media/optin/requests";
        public static string FileEndPoint = "media/contacts/import";

        public enum ContactsSearchCriteria
        {
            Contact_Name,
            Outlet_Name,
            Influencer_Keyword,
            NewsInfluencer_Keyword,
            Outlet_Type,
            Subject,
            Outlet_Location,
            Dma_Id,
            Contact_Location,
            Keyword_Profile,
            List_Name,
            Record_Type,
            Contact_List,
            Contact_Name_Consolidated,
            News_Focus,
            Outlet_Subjects,
            OptedOut,
            Keyword_Title,
            Keyword_Email,
            LocationRadius,
            OutletTarget_Area

        };

        public enum ContactSortField
        {
            SortName,
            OutletName,
            CirculationAudience,
            UniqueVisitorsPerMonth
        };

        public enum SortDirection { Ascending, Descending };

        private Dictionary<ContactsSearchCriteria, string> map = new Dictionary<ContactsSearchCriteria, string>() {
                        { ContactsSearchCriteria.Contact_Name,"name"},
                        { ContactsSearchCriteria.Outlet_Name,"outletName"},
                        { ContactsSearchCriteria.Influencer_Keyword,"influencerKeyword"},
                        { ContactsSearchCriteria.NewsInfluencer_Keyword,"newsInfluencerKeyword"},
                        { ContactsSearchCriteria.Outlet_Type,"outletTypeIds"},
                        { ContactsSearchCriteria.Subject,"subjects"},
                        { ContactsSearchCriteria.Outlet_Location,"outletLocation.placeId"},
                        { ContactsSearchCriteria.Dma_Id,"outletDMAIds"},
                        { ContactsSearchCriteria.Contact_Location,"contactlocation.placeid"},
                        { ContactsSearchCriteria.Keyword_Profile,"keyword.profile"},
                        { ContactsSearchCriteria.List_Name,"listname"},
                        { ContactsSearchCriteria.Record_Type,"recordtype"},
                        { ContactsSearchCriteria.News_Focus,"outletnewsfocus"},
                        { ContactsSearchCriteria.Outlet_Subjects,"outletSubjects"},
                        { ContactsSearchCriteria.OptedOut,"optedout"},
                        { ContactsSearchCriteria.Keyword_Title,"keyword.title"},
                        { ContactsSearchCriteria.Keyword_Email,"keyword.email"},
                        { ContactsSearchCriteria.LocationRadius,"outletLocation.radius"},
                        { ContactsSearchCriteria.OutletTarget_Area,"outletTargetCoveragePlaceIds"},};

        /// <summary>
        /// Edits the contact's information. Serializes the list of PatchData objects into valid json
        /// </summary>
        /// <param name="id">The id for the contacts</param>
        /// <param name="list">List of PatchData objects</param>
        /// <returns></returns>
        public IRestResponse<ContactsItem> EditContact(int id, List<PatchData> list)
        {
            var patchString = JsonConvert.SerializeObject(list);
            return Patch<ContactsItem>($"{ContactsEndpoint}/{id}", patchString);
        }

        /// <summary>
        /// Get request for Contacts by the passed in criteria. 
        /// This will throw an exception if the criteria argument doesn't match a valid criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<Contacts> GetContacts(ContactsSearchCriteria criteria, string parameter, string consolidatedProfile = null, string radius = null)
        {

            if (criteria == ContactsSearchCriteria.Contact_Name && !parameter.Contains(","))
            {
                string urlEncoded = parameter.Replace(" ", "%20");
                return Get<Contacts>($"{ContactsEndpoint}?name={urlEncoded}&fields=id,sortname");
            }
            else
            {
                return Get<Contacts>($"{ContactsEndpoint}?{map[criteria]}={parameter}");
            }
        }

        /// <summary>
        /// Get request for Contacts by the passed in criteria. 
        /// This will throw an exception if the criteria argument doesn't match a valid criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<Contacts> GetContactsMultipleCriteria(IDictionary<ContactsSearchCriteria, string> criteria)
        {
            var queryString = new List<string>();
            foreach (KeyValuePair<ContactsSearchCriteria, string> criterion in criteria)
            {
                if (criterion.Key == ContactsSearchCriteria.Contact_Name && !criterion.Value.Contains(","))
                {
                    string urlEncoded = criterion.Value.Replace(" ", "%20");
                    queryString.Add($"name={urlEncoded}&fields=id,sortname");
                }
                else
                {
                    queryString.Add($"{map[criterion.Key]}={criterion.Value}");
                }
            }
            return Get<Contacts>($"{ContactsEndpoint}?{string.Join("&", queryString)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="parameter"></param>
        /// <param name="consolidatedProfile"></param>
        /// <returns></returns>
        public IRestResponse<Contacts> GetContactsConsolidated(ContactsSearchCriteria criteria, string parameter, string consolidatedProfile = null)
        {
            switch (criteria)
            {
                case ContactsSearchCriteria.Contact_List:
                    return Get<Contacts>($"{ContactsEndpoint}?listName={parameter}&consolidateMultipleProfiles={consolidatedProfile}");

                case ContactsSearchCriteria.Contact_Name_Consolidated:
                    return Get<Contacts>($"{ContactsEndpoint}?name={parameter}&consolidateMultipleProfiles={consolidatedProfile}");
                default:
                    throw new ArgumentException(Err.Msg($"'{criteria}' is not a valid criteria to search for Contacts"));
            }
        }

        /// <summary>
        /// Gets a single contact by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRestResponse<ContactsItem> GetSingleContact(int id)
        {
            return Get<ContactsItem>($"{ContactsEndpoint}/{id}");
        }

        /// <summary>
        /// Returns outlets by criteria or otherwise throws AssertionError.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="parameter"></param>
        /// <returns>Contacts</returns>
        public Contacts FindContacts(ContactsSearchCriteria criteria, string parameter)
        {
            var resp = GetContacts(criteria, parameter);
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
            return resp.Data;
        }

        /// <summary>
        /// Get request for Contacts using an existing key and a facet value
        /// </summary>
        /// <param name="key">The key of an existing search.</param>
        /// <param name="facet">The facet to apply.</param>
        /// <returns></returns>        
        public IRestResponse<Contacts> GetFilterResults(string key, string facet)
        {
            return Get<Contacts>($"{ContactsEndpoint}?key={key}&facets={facet}");
        }

        /// <summary>
        /// Gets the facets for a given search key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IRestResponse<Facets> GetFacets(string key)
        {
            return Get<Facets>($"{ContactsFacetsEndpoint}?key={key}");
        }

        /// <summary>
        /// Get request for similar contacts
        /// </summary>
        /// <param name="id">The id of the contact to find similar.</param>        
        /// <returns></returns> 
        public IRestResponse<List<SimilarContacts>> GetSimilarContacts(int id)
        {
            return Request().Get().ToEndPoint($"{ContactsEndpoint}/{id}/similarContacts").Exec<List<SimilarContacts>>();
        }

        /// <summary>
        /// Post request for creating a new list
        /// </summary>
        /// <param name="key">The key from the search response</param>
        /// <param name="delta">A list of contacts to add to a list</param>
        /// <param name="name">The name of the new list</param>
        /// <returns>A response with the Id and Name of the new list</returns>
        //public IRestResponse<Lists> PostContactsList(string key, string name)
        //{
        //    var postData = new ContactsList()

        //    {
        //        Key = key,
        //        SelectAll = true,
        //        Name = name
        //    };
        //    return Post<Lists>(ContactsListEndpoint, GetAuthorizationHeader(), postData);
        //}

        /// <summary>
        /// post request to create a new list from search results with special characters in the list name which should fail
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRestResponse<Dictionary<string, string>> PostContactsList(string key, string name)
        {
            var postData = new ContactsList()

            {
                Key = key,
                SelectAll = true,
                Name = name
            };
            return Post<Dictionary<string, string>>(ContactsListEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// post to add a contact to a new list using contact id and new list name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        public IRestResponse<Dictionary<string, string>> PostContactToNewList(int id, string listName)
        {
            return Post<Dictionary<string, string>>($"{ContactsEndpoint}/{id}/lists?listname={listName}", GetAuthorizationHeader());
        }

        /// <summary>
        /// Gets recent tweets by list name
        /// </summary>
        /// <param name="listName">Name of the list.</param>
        /// <returns></returns>
        public IRestResponse<Tweets> GetRecentTweetsByListName(string listName)
        {
            return Request().Get().ToEndPoint($"{ContactsListEndpoint}/recenttweets?listname={listName}").Exec<Tweets>();
        }

        /// <summary>
        /// Gets recent tweets by list id
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns></returns>
        public IRestResponse<Tweets> GetRecentTweetsByListId(int listId)
        {
            return Request().Get().ToEndPoint($"{ContactsListEndpoint}/recenttweets?listId={listId}").Exec<Tweets>();
        }

        /// <summary>
        /// Posts an email containing a specific contact's details to the recipients provided. some fields are REQURIRED
        /// </summary>
        /// <param name="contactId">The id of the contact</param>
        /// <returns>200OK or 400BR with error message</returns>
        public IRestResponse<ContactExportResponse> PostSendContactDetailsViaEmail(int contactId)
        {
            var postData = new ContactExportBody()
            {
                DeliveryOptions = new DeliveryOptions()
                {
                    From = "anupam.gupta@cision.com",
                    Subject = "",
                    Message = "check this out",
                    Recipients = new string[] { "ankit.kavi@cision.com" }
                },
                DataOptions = new DataOptions()
                {
                    IncludedSections = new int[] { 0, 1, 2 },

                    AudienceGeographicAffinities = new AudienceGeographicAffinities()
                    {
                        Title = null,
                        LegendItems = null,
                        SVG = null
                    },
                    TopTerms = new TopTerms()
                    {
                        Title = null,
                        LegendItems = null,
                        SVG = null
                    }
                }
            };

            return Post<ContactExportResponse>($"{ContactsEndpoint}/{contactId}/{ContactsExportEndpoint}", postData);
        }

        public IRestResponse<InfluencerRankingsResponse> GetInfluencerRankings(int contactId)
        {
            return Get<InfluencerRankingsResponse>($"{ContactsEndpoint}/{contactId}/{ContactsInfluencerRankingEndpoint}");
        }

        /// <summary>
        /// Create a new private contact with random data
        /// </summary>
        /// <param name="first">The first name for the new private contact</param>
        /// <param name="last">The last name for the new private contact</param>
        /// <param name="outlet">The outlet id to the new contact associated</param>
        /// <returns>200OK or 400BR with error message</returns>
        public IRestResponse<ContactsItem> CreatePrivateContact(string outlet = null, string first = null, string last = null, string email = null, IEnumerable<ContactListData> lists = null, int CountryId = 0)
        {
            var postData = PrivateContact.CreateNewForOutlet(outlet, first: first, last: last, email: email, lists: lists, CountryId: CountryId);
            return Post<ContactsItem>(ContactsEndpoint, postData);
        }

        /// <summary>
        /// Delete a private contact
        /// </summary>
        /// <param name="idt">The id of the private contact to be deleted</param>        
        /// <returns>200OK or 400BR with error message</returns>
        public IRestResponse<DeleteItem> DeletePrivateContact(int id)
        {
            return Request().Delete().ToEndPoint($"{ContactsEndpoint}/{id}").Exec<DeleteItem>();
        }
        /// <summary>
        /// GEt a contact detail
        /// </summary>
        /// <param name="idt">The id of the private contact /param>        
        /// <returns>200OK or 400BR with error message</returns>
        public IRestResponse<ContactsItem> GetContactDetail(int id)
        {
            return Get<ContactsItem>($"{ContactsEndpoint}/{id}");
        }
        /// <summary>
        /// GET all contacts related
        /// </summary>
        /// <param name="id">the id of the contact to get their related</param>
        /// <returns>200OK or 400BR with error message</returns>
        public IRestResponse<List<ContactsRelated>> GetContactsRelated(int id)
        {
            return Request().Get().ToEndPoint($"{ContactsEndpoint}/{id}/{ContactsRelated}").Exec<List<ContactsRelated>>();
        }
        /// <summary>
        /// Get all history information
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200OK or 400BR with error message</returns>
        public IRestResponse<List<ContactHistory>> GetContactHistory(int id)
        {
            return Request().Get().ToEndPoint($"{ContactsEndpoint}/{id}/{ContactsHistory}").Exec<List<ContactHistory>>();
        }
        /// <summary>
        /// Post a new recent searches
        /// </summary>
        /// <param name="key">The key generated in the previous search</param>
        /// <returns>201 created</returns>
        public IRestResponse<MediaSavedSearchItem> PostRecentSearch(string key)
        {
            var postData = new MediaSavedSearchPostData() { Key = key };
            return Request().Post().ToEndPoint($"{ContactsEndpoint}{ContactRecentSearch}").Data(postData).Exec<MediaSavedSearchItem>();
        }
        /// <summary>
        /// Get all recent media contact searches
        /// </summary>
        /// <returns>200 OK</returns>
        public IRestResponse<MediaSavedSearch> GetRecentSearches()
        {
            return Request().Get().ToEndPoint($"{ContactsEndpoint}{ContactRecentSearch}").Exec<MediaSavedSearch>();
        }
        /// <summary>
        /// Edit the notes of a contact
        /// </summary>
        /// <param name="id">the id of the contact to be edited</param>
        /// <param name="list"></param>
        /// <returns>200 OK</returns>
        public IRestResponse<ContactsItem> PatchContactNotes(int id, List<PatchData> list)
        {
            var patchString = JsonConvert.SerializeObject(list);
            return Patch<ContactsItem>($"{ContactsEndpoint}/{id}", patchString);
        }
        /// <summary>
        /// Return the child when the contacts are consolidated
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IRestResponse<ContactSuggestion> GetSuggestion(string param)
        {
            return Get<ContactSuggestion>($"{ContactsEndpoint}/suggestions?name={param}");
        }
        /// <summary>
        /// Post a new saved searches
        /// </summary>
        /// <param name="key">The key generated in the previous search</param>
        /// <returns>201 created</returns>
        public IRestResponse<MediaSavedSearchItem> PostSavedSearch(string key, string name)
        {
            var postData = new MediaSavedSearchPostData() { Key = key, Name = name };
            return Request().Post().ToEndPoint($"{ContactsEndpoint}{ContactSavedSearch}").Data(postData).Exec<MediaSavedSearchItem>();
        }
        /// <summary>
        /// Get all saved media contact searches
        /// </summary>
        /// <returns>200 OK</returns>
        public IRestResponse<MediaSavedSearch> GetSavedSearches()
        {
            return Request().Get().ToEndPoint($"{ContactsEndpoint}{ContactSavedSearch}?page=1&limit=100").Exec<MediaSavedSearch>();
        }
        /// <summary>
        /// Delete a saved search by id 
        /// </summary>
        /// <param name="id">The id of the search to delete</param>
        /// <returns></returns>
        public IRestResponse<DeleteItem> DeleteSavedSearch(int id)
        {
            return Request().Delete().ToEndPoint($"{ContactsEndpoint}{ContactSavedSearch}/{id}").Exec<DeleteItem>(); ;
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
            return Request().Patch().ToEndPoint($"{ContactsEndpoint}{ContactSavedSearch}/{id}").Data(patchBody).Exec<MediaSavedSearchItem>();
        }
        /// <summary>
        /// Return the items sorted by the field given as parameter
        /// </summary>
        /// <param name="key">key of the search</param>
        /// <param name="fieldName">field to be sorted</param>
        /// <returns></returns>
        public IRestResponse<Contacts> SortContactsBy(string key, string fieldName)
        {
            return Get<Contacts>($"{ContactsEndpoint}?key={key}&sort={fieldName}");
        }

        /// <summary>
        /// export contacts with field givan as a parameter
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public IRestResponse<JobResponse> ExportContacts(string key, string field)
        {
            var postData = new ContactExportData()
            {
                Key = key,
                PresentationType = 0,
                SelectAll = true,
                Fields = new string[] { field },
            };
            return Post<JobResponse>($"{ContactsEndpoint}/export", postData);
        }

        /// <summary>
        /// Get recent tweets for a contact
        /// </summary>
        /// <param name="id">The Id of the contact</param>
        /// <returns>A collection of the contact's recent tweets</returns>
        public IRestResponse<Tweets> GetRecentTweets(int id)
        {
            return Request().Get().ToEndPoint($"{ContactsEndpoint}/{id}{RecentTweets}").Exec<Tweets>();
        }

        public IRestResponse<Contacts> SortContacts(string key, ContactSortField field, SortDirection sortDirection)
        {
            string direction = sortDirection == SortDirection.Ascending ? "" : "-";
            return Request().Get().ToEndPoint($"{ContactsEndpoint}?key={key}&sort={direction}{field}").Exec<Contacts>();
        }
        /// <summary>
        /// post a opt back in request
        /// </summary>
        /// <param name="id">the id of the entity</param>
        /// <param name="entityTypeId">the type of the entity</param>
        /// <param name="sender">the sender name</param>
        /// <returns></returns>
        public IRestResponse<OptinRequest> PostOptinRequest(int id, int entityTypeId, string sender)
        {
            var postData = new OptinRequestData()
            {
                EntityTypeId = entityTypeId,
                EntityId = id,
                LCID = 1033,
                SenderName = sender,
                Subject = "this contact should be opted in"
            };
            return Post<OptinRequest>($"{OptinRequests}", postData);
        }
        /// <summary>
        /// patch the value of opt in request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public IRestResponse<OptinRequest> PatchOptinRequest(string key, string path)
        {
            var patchBody = new PatchData[] { new PatchData("update", path, "true") };
            return Request().Patch().ToEndPoint($"{OptinRequests}").AddUrlQueryParam("key", key).Data(patchBody).Exec<OptinRequest>();
        }

        /// <summary>
        /// Gets multiple contacts
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IRestResponse<Contacts> GetContactsUsingIds(List<int> ids)
        {
            var idString = string.Join(",", ids);
            return Get<Contacts>($"{ContactsEndpoint}?contactids={idString}");
        }


        public IRestResponse Upload(string filePath)
        {
            string token = string.Empty;
            Parameter param = new Parameter();
            param.Name = "name";
            param.Type = ParameterType.GetOrPost;
            param.ContentType = "contactImport";
            IRestResponse<StreamResponse> res = Post<StreamResponse>(FileEndPoint + "/" + 0, filePath);            
            return res;
        }
    }
}    