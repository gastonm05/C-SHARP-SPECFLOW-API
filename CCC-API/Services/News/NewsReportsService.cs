using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.News;
using CCC_API.Data.TestDataObjects;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CCC_API.Services.News
{
    public class NewsReportsService : AuthApiService
    {
        public static string SavedSearchesEndpoint = "news/searches";
        public static string NewsEndpoint = "news";
        public static string ReportEndpoint = "news/exports";

        public NewsReportsService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Grouping Values for Clipbooks
        /// </summary>
        public enum Grouping
        {
            Company = 1,
            Tags = 2,
            Product = 3,
            KeywordSearch = 4,
            Medium = 5
        };

        /// <summary>
        /// Sorting Values for Clipbooks
        /// </summary>
        public enum Sorting
        {
            Manual = 1,
            NewestToOldest = 2, 
            OldestToNewest = 3,
            PublicityValue = 4,
            AudienceReach = 5,
            UVPM = 6,
            OutletCountry = 7, 
            Medium = 8
        };

        /// <summary>
        /// Gets the Id of the last saved news search
        /// </summary>
        /// <returns></returns>
        public int GetLastSavedSearchId()
        {
            var response = Get<SavedSearches>(SavedSearchesEndpoint);
            return response.Data.Items.LastOrError().Id;
        }

        /// <summary>
        /// Gets the Id of the last piece of news of the specified saved search
        /// <param name="searchId">Id of the a saved search</param>
        /// </summary>
        /// <returns></returns>
        public int GetNewsByView(string key)
        {
            var response = Get<NewsView>($"{NewsEndpoint}?key={key}");
            return response.Data.Items.LastOrError().Id;
        }
        /// <summary>
        /// Sends a POST Request to create a clip report for News Searches
        /// <param name="keyImported">News search key needed for report generation</param>
        /// <param name="newsId">Id of the piece of news selected for the report</param>
        /// <param name="fields">List of the selected fields for the clip report</param>
        /// </summary>
        /// <returns></returns>
        public IRestResponse CreateClipReport(string keyImported, int newsId, List<int> fields)
        {
            List<int> newsList = new List<int>();
            newsList.Add(newsId);
            var postData = new ClipReport()
            {
                key = keyImported,
                delta = newsList,
                selectAll = false,
                format = "doc",
                fields = fields,
            };
            return Post<ClipReportResponse>(ReportEndpoint, GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// Get matching enum int values from list of strings
        /// <param name="fields">List of the selected fields for the clip report</param>
        /// </summary>
        /// <returns></returns>
        public List<int> GetFieldEnums(List<string> fields) => fields.Select(x => (int)Enum.Parse(typeof(Fields), x, true)).ToList();

        /// <summary>
        /// Returns a list with a given number of News Ids
        /// </summary>
        /// <param name="items"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public List<int> GetMultipleNewsIds(List<NewsItem> items, int amount)
        {
            List<int> newsIds = new List<int>();
            IEnumerable<NewsItem> news = items.Take(amount);
            foreach (NewsItem i in news)
            {
                newsIds.Add(i.Id);
            }
            return newsIds;
        }

        /// <summary>
        /// Performs a POST to News Reports endpoint in order to save a news Clipbook
        /// </summary>
        /// <param name="title"></param>
        /// <param name="items"></param>
        /// <param name="summary"></param>
        /// <param name="newsTemplateId"></param>
        /// <returns></returns>
        public IRestResponse<NewsSingleClipBook> PostSaveNewsClipbook(string title, List<NewsItem> items, string summary, string template, Sorting sortType, Grouping groupType, List<NewsClipbookGroup> groups)
        {
            List<NewsClipbookGroup> groupList = new List<NewsClipbookGroup>();
            List<string> deliveryTypes = new List<string>();
            List<string> recipients = new List<string>();
            //grouping
            NewsClipbookGroup defaultGroup = new NewsClipbookGroup()
            {
                Id = -1,
                Name = "Uncategorized",
                NewsIds = GetMultipleNewsIds(items, 10)
            };
            groupList.Add(defaultGroup);
            //deliveryOptions
            deliveryTypes.Add("Email");
            recipients.Add("CisionAutomation@mailinator.com");
            NewsClipbookDeliveryOptions deliveryOptions = new NewsClipbookDeliveryOptions()
            {
                DeliveryTypes = deliveryTypes,
                Recipients = recipients,
                Subject = "QA Automation Clipbook",
                SenderEmail = "noreply@cision.com",
                SenderName = "Cision Manager"
            };
            var postData = new NewsClipBookPostData()
            {
                Title = $"{title}_{DateTime.Now.ToString("yyyy-M-d hh:mm:ss")}",
                Summary = summary,
                Template = template,
                SortType = sortType,
                GroupType = groupType,
                Groups = groupList,
                DeliveryOptions = deliveryOptions
            };
            return Post<NewsSingleClipBook>($"{NewsEndpoint}/reports/clipbooks", GetAuthorizationHeader(), postData);
        }

        /// <summary>
        /// GET All News Clipbooks
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsClipBooks> GetAllNewsClipbooks() => Get<NewsClipBooks>($"{NewsEndpoint}/reports/clipbooks");

        /// <summary>
        /// Gets a Single News Clipbook by its Name/Title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public IRestResponse<NewsSingleClipBook> GetSingleNewsClipbookByTitle(string title, List<NewsSingleClipBook> items) =>
            Get<NewsSingleClipBook>($"{NewsEndpoint}/reports/clipbooks/{items.Find(i => i.Title.ToUpper().Equals(title.ToUpper())).Id}");

        /// <summary>
        /// Gets a Single News Clipbook by its ID
        /// </summary>
        /// <param name="clipBookId"></param>
        /// <returns></returns>
        public IRestResponse<NewsSingleClipBook> GetSingleNewsClipbookById(int clipBookId) => Get<NewsSingleClipBook>($"{NewsEndpoint}/reports/clipbooks/{clipBookId}");

        /// <summary>
        /// Delete a Single News Clipbook by its ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IRestResponse DeleteClipbookById(int id) => Request().Delete().ToEndPoint($"{NewsEndpoint}/reports/clipbooks/{id}").Exec();

        /// <summary>
        /// Performs a PUT to edit/update a single News CLipbook by its ID
        /// </summary>
        /// <param name="clipbookId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public IRestResponse<NewsSingleClipBook> EditClipbook(int clipbookId, List<NewsItem> items)
        {
            List<int> newsIds = GetMultipleNewsIds(items, 30);
            List<NewsClipbookGroup> groupList = new List<NewsClipbookGroup>();
            List<string> deliveryTypes = new List<string>();
            List<string> recipients = new List<string>();
            //grouping
            NewsClipbookGroup defaultGroup = new NewsClipbookGroup()
            {
                Id = -1,
                NewsIds = newsIds
            };
            groupList.Add(defaultGroup);
            //deliveryOptions
            deliveryTypes.Add("DOC");
            NewsClipbookDeliveryOptions deliveryOptions = new NewsClipbookDeliveryOptions()
            {
                DeliveryTypes = deliveryTypes
            };
            var putData = new NewsClipBookPostData()
            {
                Title = $"Edited_{DateTime.Now.ToString("yyyy-M-d hh:mm:ss")}",
                Summary = $"Edited Summary",
                Template = "NT-1",
                SortType = Sorting.OldestToNewest,
                GroupType = Grouping.Company,
                Groups = groupList,
                DeliveryOptions = deliveryOptions
            };
            return Put<NewsSingleClipBook>($"{NewsEndpoint}/reports/clipbooks/{clipbookId}", GetAuthorizationHeader(), putData);
        }

        /// <summary>
        /// Performs a GET to fetch News Clipbook Definitions
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsClipBookDefinitions> GetNewsClipbookDefinitions() => Get<NewsClipBookDefinitions>($"{NewsEndpoint}/reports/clipbooks/definitions");

        /// <summary>
        /// Gets a Clipbook by its Id
        /// </summary>
        /// <param name="clipbookId"></param>
        /// <returns></returns>
        public IRestResponse<NewsSingleClipBook> GetNewsClipbook(int clipbookId) => Get<NewsSingleClipBook>($"{NewsEndpoint}/reports/clipbooks/{clipbookId}");

        /// <summary>
        /// Returns a boolean value evluating if the given collection
        /// is ordered by the given direction
        /// </summary>
        /// <param name="items"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsClipbookSorted(List<ClipbookItem> items, Sorting sortField)
        {
            var expected = new List<ClipbookItem>();
            switch(sortField)
            { 
                case Sorting.NewestToOldest:
                    expected = items.OrderByDescending(i => i.NewsDate).ToList();
                    break;
                case Sorting.OldestToNewest:
                    expected = items.OrderBy(i => i.NewsDate).ToList();
                    break;
                case Sorting.Medium:
                    expected = items.OrderBy(i => i.Medium).ToList();
                    break;
            }
            return expected.SequenceEqual(items);
        }
    }
}