using System.Collections.Generic;
using CCC_API.Data.Responses.News;
using RestSharp;
using CCC_API.Data.PostData.News;
using System.Linq;

namespace CCC_API.Services.News
{
    public class NewsArchiveService : AuthApiService
    {
        public static string newsArchiveCountriesEndPoint = "news/archive/countries";
        public static string newsArchiveLanguagesEndPoint = "news/archive/languages";
        public static string newsArchiveSourcesEndPoint = "news/archive/sources";
        public static string newsArchiveSearchByKeywordsEndPoint = "news/archive?q_keywords";
        public static string newsArchiveSourcesCriteria = "q_sources";
        public static string newsArchiveImportEndPoint = "news/archive/imports";
        public static string newsPreferencesEndPoint = "news/preferences";


        public NewsArchiveService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a GET to News Archive Countries endpoint to get all available countries for that account in Visible side
        /// </summary>               
        /// <returns> IRestResponse<ExportTemplatePostData> </returns>
        public IRestResponse<NewsArchiveCountriesResponse> GetAvailableCountries() => 
            Get<NewsArchiveCountriesResponse>($"{newsArchiveCountriesEndPoint}");
        
        
        /// <summary>
        /// Performs a GET to News Archive Languages endpoint to get all available languages for that account in Visible side
        /// </summary>               
        /// <returns> IRestResponse<ExportTemplatePostData> </returns>
        public IRestResponse<NewsArchiveLanguagesResponse> GetAvailableLanguages() => 
            Get<NewsArchiveLanguagesResponse>($"{newsArchiveLanguagesEndPoint}");
        

        /// <summary>
        /// Performs a GET to News Archive Sources endpoint to get all available sources for that account in Visible side
        /// </summary>               
        /// <returns> IRestResponse<ExportTemplatePostData> </returns>
        public IRestResponse<NewsArchiveSourcesResponse> GetAvailableSources() =>
            Get<NewsArchiveSourcesResponse>($"{newsArchiveSourcesEndPoint}");
        

        /// <summary>
        /// Performs a GET to News Archive Search Endpoint by keywords
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns> IRestResponse<NewsViewArchive> </returns>
        public IRestResponse<NewsViewArchive> GetNewsArchiveByKeywords(string keywords) => 
            Get<NewsViewArchive>($"{newsArchiveSearchByKeywordsEndPoint}={keywords}");

        /// <summary>
        /// Performs a GET to News Archive Search Endpoint by keywords and sources
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="source"></param>
        /// <returns> IRestResponse<NewsViewArchive> </returns>
        public IRestResponse<NewsViewArchive> GetNewsArchiveByKeywordsAndSource(string keywords, string source) =>
            Get<NewsViewArchive>($"{newsArchiveSearchByKeywordsEndPoint}={keywords}&{newsArchiveSourcesCriteria}={source}");

        /// <summary>
        /// Gets a Source Id by its Name
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public int GetSourceIdBySourceName(string sourceName) =>
            GetAvailableSources().Data.Items.Find(x => x.Description.ToUpper().Equals(sourceName.ToUpper())).SourceId;

        /// <summary>
        /// Performs a GET to News Archive Search Endpoint by keywords and Source Name
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="sourceName"></param>
        /// <returns> IRestResponse<NewsViewArchive> </returns>
        public IRestResponse<NewsViewArchive> GetNewsArchiveByKeywordsAndSourceName(string keywords, string sourceName) =>
            Get<NewsViewArchive>($"{newsArchiveSearchByKeywordsEndPoint}={keywords}&{newsArchiveSourcesCriteria}={GetSourceIdBySourceName(sourceName)}");

        /// <summary>
        /// Adds News Archive Items to My Coverage
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public IRestResponse<NewsArchiveImport> AddNewsArchiveItemsToMyCoverage(List<NewsItemArchiveSearch> items, string key, int limit)
        {
            List<string> delta = new List<string>();
            foreach (NewsItemArchiveSearch i in items.Take(limit)) delta.Add(i.Id);
            NewsArchiveImportPostData postData = new NewsArchiveImportPostData {
                Key = key,
                Delta = delta,
                SelectAll = false
            };
            return Post<NewsArchiveImport>($"{newsArchiveImportEndPoint}", postData);
        }

        /// <summary>
        /// Performs a GET to News Preferences
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsArchivePreferences> GetNewsPreferences() => Get<NewsArchivePreferences>($"{newsPreferencesEndPoint}");
    }
}
