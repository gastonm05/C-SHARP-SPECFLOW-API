using CCC_API.Data.PostData.Settings;
using CCC_API.Data.Responses.Settings.KeywordSearches;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Settings.KeywordSearches
{
    public class MediaMonitoringService : AuthApiService
    {
        public static string mediaMonitoringEndPoint = "management/monitoring";
        public static string SamplemediaMonitoringEndPoint = "management/monitoring/sample";

        public MediaMonitoringService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a POST to MediaMonitoring Keyword Searches endpoint
        /// </summary>
        /// <param name="MediaMonitorUserSearch"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse CreateKeywordSearch(MediaMonitorUserSearch mediaMonitorUserSearch)
        {
            return Post<MediaMonitorUserSearch>(mediaMonitoringEndPoint, GetAuthorizationHeader(), mediaMonitorUserSearch);
        }

        /// <summary>
        /// Performs a GET to MediaMonitoring Keyword Searches  endpoint to get all available searches for that user
        /// </summary>               
        /// <returns> IRestResponse<ExportTemplatePostData> </returns>
        public IRestResponse<MediaMonitorUserSearch> GetKeywordSearches()
        {
            return Get<MediaMonitorUserSearch>($"{mediaMonitoringEndPoint}");
        }

        /// <summary>
        /// Performs a DELETE to MediaMonitoring Keyword Searches endpoint to delete specified MediaMonitoring Keyword Search Id
        /// </summary>               
        /// <param name="mediaMonitoringKeywordSearchId"></param>        
        /// <returns> IRestResponse<AllCustomFieldsResponse> </returns>
        public IRestResponse<Dictionary<string, string>> DeleteKeywordSearch(string mediaMonitoringKeywordSearchId)
        {
            string resource = ($"{ mediaMonitoringEndPoint}/{mediaMonitoringKeywordSearchId}");

            return Delete<Dictionary<string, string>>(resource);
        }

        /// <summary>
        /// Performs a PUT to MediaMonitoring Keyword Searches to modify specified MediaMonitoring Keyword Search Id with provided data
        /// </summary>
        /// <param name="mediaMonitoringKeywordSearchId"></param>
        /// <param name="beModifiedKeywordSearchesData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse<MediaMonitorUserSearch> ModifyMediaMonitoringKeywordSearch(string mediaMonitoringKeywordSearchId, MediaMonitorUserSearch beModifiedKeywordSearchesData)
        {
            string resource = ($"{mediaMonitoringEndPoint}/{mediaMonitoringKeywordSearchId}");
            return Put<MediaMonitorUserSearch>(resource, beModifiedKeywordSearchesData);
        }

        /// <summary>
        /// Performs a PUT to MediaMonitoring Keyword Searches Name endpoint to modify specified MediaMonitoring Keyword Search Id with provided name
        /// </summary>
        /// <param name="mediaMonitoringKeywordSearchId"></param>
        /// <param name="newName"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse<List<string>> Rename(string mediaMonitoringKeywordSearchId, string newName)
        {
            string resource = ($"{mediaMonitoringEndPoint}/{mediaMonitoringKeywordSearchId}/name");
            return Put<List<string>>(resource, newName);
        }

        /// <summary>
        /// Performs a POST to Sample MediaMonitoring Keyword Searches endpoint
        /// </summary>
        /// <param name="mediaMonitorUserSearch"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse CreateSampleKeywordSearch(MediaMonitorUserSearch mediaMonitorUserSearch)
        {
            return Post<SampleKeywordSearchResponse>(SamplemediaMonitoringEndPoint, GetAuthorizationHeader(), mediaMonitorUserSearch);
        }
        /// <summary>
        /// Performs a POST to Sample MediaMonitoring Keyword Searches endpoint 
        /// </summary>
        /// <param name="mediaMonitorUserSearch"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse<Dictionary<string, string>> CreateSampleKeywordSearchInvalid(MediaMonitorUserSearchPost mediaMonitorUserSearch)
        {
            return Post <Dictionary<string, string>> (SamplemediaMonitoringEndPoint, GetAuthorizationHeader(), mediaMonitorUserSearch);
        }
    }
}
