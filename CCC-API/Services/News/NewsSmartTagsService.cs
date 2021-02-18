using CCC_API.Data.Responses.News;
using CCC_API.Data.Responses.Settings.SmartTags;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Services.News
{
    public class NewsSmartTagsService : AuthApiService
    {
        public NewsSmartTagsService(string sessionKey) : base(sessionKey) { }

        public const string NewsSmartTagsEndpoint = "news/smarttags";
        public const string NewsSmartTagsConfigEndpoint = "news/smarttags/config";

        /// <summary>
        /// Gets all available News Types (aka Smart Tags)
        /// </summary>
        /// <returns></returns>
        public NewsTypes GetAllSmartTags() =>
            Request().Get().ToEndPoint(NewsSmartTagsEndpoint).ExecCheck<NewsTypes>();

        /// <summary>
        /// Gets a single News Type
        /// </summary>
        /// <param name="smartTags"></param>
        /// <returns></returns>
        public NewsType GetSingleSmartTag(List<NewsType> smartTags) => smartTags.FirstOrDefault();

        /// <summary>
        /// Returns TRUE if all News Clip from a search result have the expected Smart Tag
        /// </summary>
        /// <param name="items"></param>
        /// <param name="smartTagName"></param>
        /// <returns></returns>
        public bool AreAllNewsItemsTaggedWithGivenNewsType(List<NewsItem> items, string smartTagName) =>
            items.All(i => i.Type.Name.ToLower().Equals(smartTagName.ToLower()));
        
        /// <summary>
        /// Gets Smart Tags Current Configuarion
        /// </summary>
        /// <returns></returns>
        public IRestResponse<SmartTagsConfig> GetSmartTagsConfig() { 
            return Get<SmartTagsConfig>(NewsSmartTagsConfigEndpoint);
        }

        /// <summary>
        /// Edit Smart Tags Configuarion
        /// </summary>
        /// <param name="newConfig"></param>
        /// <returns></returns>
        public IRestResponse EditSmartTagsConfig(SmartTagsConfig newConfig)
        {
            return Put<SmartTagsConfig>(NewsSmartTagsConfigEndpoint, newConfig);
        }
    }
}
