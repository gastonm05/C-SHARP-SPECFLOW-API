using CCC_API.Data;
using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.News;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CCC_API.Services.News
{
    public class NewsTagsService : AuthApiService
    {
        public NewsTagsService(string sessionKey) : base(sessionKey) { }

        public const string NewsTagsEndpoint = "news/tags";
        public const string TagsPath = "tags";

        /// <summary>
        /// Gets the tags for a customer
        /// </summary>
        /// <returns></returns>
        public NewsTags GetTags() => Request().Get().ToEndPoint(NewsTagsEndpoint).ExecCheck<NewsTags>();

        /// <summary>
        /// Gets a single Tag by ID
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public NewsTag GetSingleTag(int tagId) => Request().Get().ToEndPoint($"{NewsTagsEndpoint}/{tagId}").ExecCheck<NewsTag>();

        /// <summary>
        /// PATCH Op to update name on a single tag 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public IRestResponse<NewsTag> PatchNewsTag(int tagId)
        {
            var op = "Update";
            var path = "Name";
            var value = "Automation " + DateTime.Now.ToString("hh:mm:ss");
            var arrayPatchData = new[] { new PatchData(op, path, value) };
            return Patch<NewsTag>($"{NewsTagsEndpoint}/{tagId}", arrayPatchData);
        }

        /// <summary>
        /// Bulk tags news items
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="itemIds">The item ids.</param>
        /// <param name="selectAll">if set to <c>true</c> [select all].</param>
        /// <returns></returns>
        public IRestResponse BulkTagNewsItems(string key, int tagId, int[] itemIds, bool selectAll = false)
        {
            var patch = new[] { new PatchData("add", TagsPath, tagId.ToString()) };
            var bulkPatch = new BulkPatchDataDelta(key, selectAll, patch, itemIds);
            return Request().Patch().ToEndPoint(NewsViewService.NewsViewEndPoint).Data(bulkPatch).ExecCheck(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Creates a New Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public IRestResponse<NewsTagItem> CreateNewTag(string tagName)
        {
            string NewTagName = tagName;
            var TagData = new NewsTagPostData() { Name = NewTagName };
            var response = Post<NewsTagItem>(NewsTagsEndpoint, GetAuthorizationHeader(), TagData);
            return response;
        }

        /// <summary>
        /// Deletes a News Tag by Id using jsonBody
        /// We use a Delete Request with body because delete tags operation is desiged in that way
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public IRestResponse<NewsTagItem> DeleteNewsTags(List<int> tagId)
        {
            var TagData = new NewsTagDeleteData()
            {
                SelectAll = false,
                Delta = tagId,
            };

            var response = Delete<NewsTagItem>(NewsTagsEndpoint, GetAuthorizationHeader(), TagData);
            return response;
        }

        /// <summary>
        /// Deletes the news tags by Name
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        public IRestResponse<NewsTagItem> DeleteNewsTags(List<string> tagNames)
        {
            var ids = new List<int>();
            foreach (var name in tagNames)
            {
                ids.Add(GetTagIdByName(name));
            }
            return DeleteNewsTags(ids);
        }

        /// <summary>
        /// Gets a list of Tags Ids from Tags Names
        /// </summary>
        /// <param name="tagNames"></param>
        /// <returns></returns>
        public int GetTagIdByName(string tagName) =>
            GetTags().Items.Find(x => x.Name.ToUpper().Equals(tagName.ToUpper())).Id;

        /// <summary>
        /// Checks a News Item collection to see if Typed News are included
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool GetIsTypedNewsIncluded(List<NewsItem> list) =>
            list.Any(i => !i.Type.Equals(null) && i.Type.Name.Equals("Mention") || !i.Type.Equals(null) && i.Type.Name.Equals("Brief") || !i.Type.Equals(null) && i.Type.Name.Equals("Feature"));

        /// <summary>
        /// Returns TRUE if News Clip is Tagged with given Tag
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public bool GetIsNewsTagged(NewsItem item, string tagName) => item.Tags.Any(tag => tag.Name.Equals(tagName));

        /// <summary>
        /// Queries for all available News Tags
        /// </summary>
        public NewsTagsQuery GetAllTags() => Request().Get().ToEndPoint($"{NewsTagsEndpoint}/query?page=1&pageSize=50&sortDirection=ascending&sortField=Name").ExecCheck<NewsTagsQuery>();

        /// <summary>
        /// Performs a PATCH to append/replace/remove tags from News Items in bulk
        /// </summary>
        /// <param name="operation">PATCH Operation: Append, Replace, Remove</param>
        /// <param name="key">News Search Key</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="items">List of News Items Ids</param>
        /// <returns></returns>
        public IRestResponse PatchNewsWithTagsMultipleOperations(string operation, string key, string tagName, int[] delta, bool selectAll = false)
        {
            var patchData = new[] { new PatchData(operation, TagsPath, GetTagIdByName(tagName).ToString()) };
            var bulkPatch = new BulkPatchDataDelta(key, selectAll, patchData, delta);
            return Request().Patch().ToEndPoint(NewsViewService.NewsViewEndPoint).Data(bulkPatch).ExecCheck(HttpStatusCode.NoContent);
        }
    }
}