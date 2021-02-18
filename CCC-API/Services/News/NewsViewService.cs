using CCC_API.Data;
using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.News;
using CCC_Infrastructure.Utils;
using NUnit.Framework;
using Org.BouncyCastle.Security;
using RestSharp;
using SimpleJson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;

namespace CCC_API.Services.News
{
    public class NewsViewService : AuthApiService
    {
        public static string NewsViewEndPoint = "news";
        public static string NewsFacetsEndpoint = "news/facets";
        public static string NewsClipReportEndPoint = "news/reports/clipreport/fields";
        public static string NewsCustomFields = "customFields";
        public static string NewsQueryEndpoint = "news/query";
        public static string NewsCustomFieldsEndpoint = "fielddefinitions";

        public NewsViewService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Represents the criterion by which News can be searched by
        /// Note: Outlet Location can be searched via ID or Name but there is only
        /// one Outlet Location option within the app
        /// </summary>
        public enum NewsSearchCriteria
        {
            Company,
            Product,
            Message,
            Keywords,
            Date_Range,
            Outlet_Location, //Use this to search via string value (Chicago)
            Outlet_Locations, //Use this to search via id(s) (3-5469)
            Outlet_Name,
            Tags,
            Outlet_List,
            Custom_Field,
            Invalid_Outlet_Name
        };

        /// <summary>
        /// Represents the operator on searches that allow INCLUDE/EXCLUDE operations
        /// </summary>
        public enum NewsSearchOperator
        {
            Include,
            Exclude
        };

        public enum NewsSortDirection { Ascending, Descending };

        /// <summary>
        /// GET All News
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsView> GetAllNews()
        {
            var postData = new NewsQueryKeywordsPostData()
            {
                q_keywords = ""
            };
            var postResponse = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            return Get<NewsView>($"{NewsViewEndPoint}?key={postResponse.Data.Key}&limit=1000");
        }

        /// <summary>
        /// GET All Available Facets for a given search result
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IRestResponse<Facets> GetAvailableFacets(string key) => Get<Facets>($"{NewsFacetsEndpoint}?key={key}");

        /// <summary>
        /// GET News with multiple facets applied given a search result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facets"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsWithMultipleFacets(string key, List<SingleFacet> facets)
        {
            var ids = facets.Select(i => i.Id).Take(3);
            var idsString = string.Join(",", ids);
            return Get<NewsView>($"{NewsViewEndPoint}?key={key}&facets={idsString}");
        }

        /// <summary>
        /// Gets News with parameters. Assumes parameters are correctly formatted
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="limit">Limit the amount of news returned, set to 0 or negative to get default amount.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<NewsView> GetNewsWithParameters(NewsSearchCriteria criteria, string parameter, int limit = -1)
        {
            var stopwatch = new Stopwatch();
            long responseTime = 0;
            IRestResponse<NewsQuery> response;
            IRestResponse<NewsView> firstPageResponse;
            switch (criteria)
            {
                case NewsSearchCriteria.Keywords:
                    var postDataKeywords = new NewsQueryKeywordsPostData()
                    {
                        q_keywords = parameter
                    };
                    // Initiate StopWatch
                    stopwatch.Start();
                    response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataKeywords);
                    // Finish StopWatch
                    stopwatch.Stop();
                    responseTime = stopwatch.ElapsedMilliseconds;
                    stopwatch.Reset();
                    // Assert expected performance for getting the Search Key
                    Assert.That(responseTime, Is.LessThan(10000), $"Response Time:{responseTime} exceeded expected performance for getting the Search Key");
                    break;
                    
                case NewsSearchCriteria.Date_Range:
                    var postDataDateRange = new NewsQueryDateRangePostData()
                    {
                        q_enddate = parameter
                    };
                    response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataDateRange);
                    break;

                case NewsSearchCriteria.Tags:
                    List<string> tagsNames = new List<string>(parameter.Split(new string[] { "," }, StringSplitOptions.None));
                    NewsTagsService newsTagsService = new NewsTagsService(SessionKey);
                    List<int> tags_ids = new List<int>();
                    foreach (var t in tagsNames) tags_ids.Add(newsTagsService.GetTagIdByName(t));
                    var postDataTags = new NewsQueryTagsIncludeExcludePostData()
                    {
                        q_tags = new NewsQueryMultiTypePostData()
                        {
                            ids = tags_ids,
                            @operator = "equals"
                        }
                    };
                    response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataTags);
                    break;

                default:
                    throw new ArgumentException(Err.Msg($"'{criteria}' is not a valid criteria to search for News"));
            }
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            // Initiate StopWatch
            stopwatch.Start();
            firstPageResponse = Get<NewsView>(uri);
            // Finish StopWatch
            stopwatch.Stop();
            responseTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            // Assert Expected Performance Time for getting the First Page of News Results
            Assert.That(responseTime, Is.LessThan(6000), $"Response Time:{responseTime} exceeded expected performance when getting the Search's First Page of results");
            return firstPageResponse;
        }

        /// <summary>
        /// Gets news by location.
        /// </summary>
        /// <param name="locationIds">The location ids.</param>
        /// <param name="oper">The operator [Equals, NotEquals]</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByLocation(NewsSearchCriteria criteria, List<string> locations, string oper = "equals", int limit = -1)
        {
            IRestResponse<NewsQuery> response;
            switch (criteria)
            {
                case NewsSearchCriteria.Outlet_Locations:
                    var postDataMulti = new NewsQueryMultiTypeStringPostData()
                    {
                        ids = locations,
                        @operator = oper
                    };
                    var postDataOutletLocationId = new NewsQueryOutletLocationsPostData()
                    {
                        q_outletlocations = postDataMulti
                    };
                    response = Request().Post().ToEndPoint(NewsQueryEndpoint).Data(postDataOutletLocationId).Exec<NewsQuery>();
                    break;

                case NewsSearchCriteria.Outlet_Location:
                    var postDataOutletLocation = new NewsQueryOutletLocationPostData()
                    {
                        q_outletlocation = locations.FirstOrError()
                    };
                    response = Request().Post().ToEndPoint(NewsQueryEndpoint).Data(postDataOutletLocation).Exec<NewsQuery>();
                    break;

                default:
                    throw new ArgumentException(Err.Msg($"'{criteria}' is not a valid criteria to search for News via location"));
            }

            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Gets the news by single date (Start or End).
        /// </summary>
        /// <param name="dateType">Type of the date.</param>
        /// <param name="date">The date.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsBySingleDate(string dateType, DateTime date, int limit = -1)
        {
            var criteria = dateType.ToLower().Equals("start") ? "q_startdate" : "q_enddate";
            var formattedDate = date.ToString("M-d-yyyy");
            var postDataDateRange = new NewsQueryPostData()
            {
                searchCriteria = criteria,
                searchValues = formattedDate
            };
            IRestResponse<NewsQuery> response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataDateRange);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Gets news by date range.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByDateRange(DateTime start, DateTime end, int limit = -1)
        {
            var formattedStartDate = start.ToString("M-d-yyyy");
            var formattedEndDate = end.ToString("M-d-yyyy");
            var postDataDateRange = new JsonObject();
            postDataDateRange.Add("q_startdate", formattedStartDate);
            postDataDateRange.Add("q_enddate", formattedEndDate);
            IRestResponse<NewsQuery> response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataDateRange);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// News Clips Bulk Edit
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> PatchBulkNewsCustomField(string searchKey, string customField)
        {
            int cfId;
            string cfType;
            var udfs = TestData.DeserializedJson<List<NewsCustomFieldPatchModel>>("NewsCustomFields.json", Assembly.Load("CCC-API"));
            cfId = udfs
                    .Where(f => f.Edition.ToLower().Equals(customField.ToLower()))
                    .FirstOrError($"'{customField}' not found in News Custom Fields JSON").Id;
            cfType = udfs
                    .Where(f => f.Edition.ToLower().Equals(customField.ToLower()))
                    .FirstOrError($"'{customField}' not found in News Custom Fields JSON").Type;
            bool selectAll = true;
            var op = "Update";
            var path = $"{NewsCustomFields}/{cfId}";
            var value = GetPatchValue(cfType);
            var patchData = new[] { new PatchData(op, path, value) };
            var bulkPatchData = new BulkPatchData(searchKey, selectAll, patchData);
            var response = Patch<NewsView>($"{NewsViewEndPoint}", bulkPatchData);
            return response;
        }

        /// <summary>
        /// Helper to get different values for each Custom Field type
        /// </summary>
        /// <param name="cfType"></param>
        /// <returns></returns>
        string GetPatchValue(string cfType)
        {
            string ret_val = null;
            Random rnd = new Random();
            switch (cfType)
            {
                case "String":
                    ret_val = "Automation_" + DateTime.Now.ToString("M/d/yyyy");
                    break;
                case "Decimal":
                    ret_val = rnd.Next(1000).ToString();
                    break;
                case "Boolean":
                    ret_val = GetRandomBooleanValue().ToString();
                    break;
                case "Date":
                    ret_val = DateTime.Now.ToString("M/d/yyyy");
                    break;
                default:
                    break;
            }
            return ret_val;
        }

        /// <summary>
        /// Helper to return random bool value
        /// </summary>
        /// <returns></returns>
        bool GetRandomBooleanValue()
        {
            Random rnd = new Random();
            bool b = false;
            b = rnd.Next(2) == 0 ? false : true;
            return b;
        }

        /// <summary>
        /// Gets a Single News Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse<NewsItem> GetSingleNews(int id) => Get<NewsItem>($"{NewsViewEndPoint}/{id.ToString()}");

        /// <summary>
        /// Gets a Single News Item and checks the return code is OK.
        /// </summary>
        /// <param name="id">valid id</param>
        /// <returns>NewsItem</returns>
        public NewsItem GetNewsItem(int id) => Request().ToEndPoint($"{NewsViewEndPoint}/{id}").ExecCheck<NewsItem>();

        /// <summary>
        /// Gets the Customizable Fields for a Clip Report
        /// </summary>
        /// <returns></returns>
        public IRestResponse<ClipReportFields> GetClipReportFields() => Get<ClipReportFields>(NewsClipReportEndPoint);

        /// <summary>
        /// Creates news article in My Coverage by given payload.
        /// </summary>
        /// <param name="item">payload</param>
        /// <returns>NewsItem</returns>
        public NewsItem PostNewsItem(NewsItemPayload item) => Request().Post().Data(item).ToEndPoint(NewsViewEndPoint).ExecCheck<NewsItem>(HttpStatusCode.Created);

        /// <summary>
        /// Patches a given news custom field
        /// </summary>
        /// <param name="cfId">News Custom Field ID</param>
        /// <param name="newsId">News Clip ID</param>
        /// <returns>News Item</returns>
        public IRestResponse<NewsItem> PatchNewsCustomField(string cfId, string newsId)
        {
            var op = "Update";
            var path = $"{NewsCustomFields}/{cfId}";
            var value = "Automation_" + DateTime.Now.ToString("M/d/yyyy");
            var arrayPatchData = new[] { new PatchData(op, path, value) };
            var response = Patch<NewsItem>($"{NewsViewEndPoint}/{newsId}", arrayPatchData);
            return response;
        }

        /// <summary>
        /// Patches a given news custom field with null to eliminate the previous value
        /// </summary>
        /// <param name="cfId">News Custom Field ID</param>
        /// <param name="newsId">News Clip ID</param>
        /// <returns>News Item</returns>
        public IRestResponse<NewsItem> PatchNullOutNewsCustomField(string cfId, string newsId)
        {
            var op = "Update";
            var path = $"{NewsCustomFields}/{cfId}";
            string value = null;
            var arrayPatchData = new[] { new PatchData(op, path, value) };
            var response = Patch<NewsItem>($"{NewsViewEndPoint}/{newsId}", arrayPatchData);
            return response;
        }

        /// <summary>
        /// GET a Cedrom clip through a specific endpoint
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsItemClip> GetNewsItemClip()
        {
            string channelId = "20";
            string date = "20170621";
            string hour = "171807";
            string length = "180";
            string format = "mp4";
            return Get<NewsItemClip>(
                $"{NewsViewEndPoint}/clip/cedrom?channelId={channelId}&date={date}&hour={hour}&length={length}&format={format}");
        }

        /// <summary>
        /// Gets all custom fields
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsCustomFieldsView> GetAllCustomFields() => Get<NewsCustomFieldsView>($"{NewsViewEndPoint}/{NewsCustomFieldsEndpoint}");

        /// <summary>
        /// Gets Nes ordered by allowed News Custom Fields
        /// </summary>
        /// <param name="key"></param>
        /// <param name="customFields"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewOrderedBySingleCustomField(string key, List<NewsCustomFields> customFields)
        {
            string cf_id = null;
            foreach (NewsCustomFields cf in customFields)
                if (cf.Type.Equals("Boolean") || cf.Type.Equals("Date") || cf.Type.Equals("Decimal"))
                {
                    cf_id = cf.Id.ToString();
                    break;
                }
            return Get<NewsView>($"{NewsViewEndPoint}?key={key}&sortfield={cf_id}&sortdirection=ascending");
        }

        /// <summary>
        /// Gets News by Single Facet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facets"></param>
        /// <param name="facetName"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsBySingleFacet(string key, List<SingleFacet> facets, string facetName)
        {
            int facet_id = facets.Where(facet => facet.CategoryText == facetName).FirstOrError($"No facet {facetName} found").Id;
            return Get<NewsView>($"{NewsViewEndPoint}?key={key}&facets={facet_id.ToString()}&limit=100");
        }

        /// <summary>
        /// Sorts by inexistent Custom Field exoecting a 404
        /// </summary>
        /// <param name="key"></param>
        /// <returns>404</returns>
        public IRestResponse<NewsView> GetNewOrderedByInexistentCustomField(string key) => Get<NewsView>($"{NewsViewEndPoint}?key={key}&sortfield=000000&sortdirection=ascending");

        /// <summary>
        /// Creates test news item with link and potential pathes.
        /// </summary>
        /// <param name="settings">news function</param>
        /// <param name="patches">patches for this news</param>
        /// <returns></returns>
        public NewsItem CreateTestNewsItem(Func<NewsItemPayload, NewsItemPayload> settings = null, params PatchData[] patches)
        {
            var item = new NewsItemPayload
            {
                Text = "Test " + StringUtils.RandomSentence(20),
                Headline = "Auto news " + StringUtils.RandomAlphaNumericString(10),
                ArticleUrl = "https://www.very-fake-news-domain.com"
            };

            item.SetNewsDate(DateTime.Now);
            if (settings != null)
            {
                item = settings(item);
            }

            var newsItem = PostNewsItem(item);

            if (patches.Any())
            {
                var patched = Request().Patch()
                    .ToEndPoint($"{NewsViewEndPoint}/{newsItem.Id}")
                    .Data(patches).ExecCheck<NewsItem>();
                return patched;
            }
            return newsItem;
        }

        /// <summary>
        /// Creates news item for given date.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public NewsItem CreateTestNewsItemForDate(DateTime dateTime)
        {
            return CreateTestNewsItem(news =>
            {
                news.SetNewsDate(dateTime);
                return news;
            });
        }

        /// <summary>
        /// Creates toned news item for given date.
        /// </summary>
        /// <param name="tone"></param>
        /// <param name="newsDate"></param>
        /// <returns></returns>
        public NewsItem CreateTestTonedNews(Analytics.Common.ToneId tone, DateTime newsDate)
        {
            return CreateTestNewsItem(news =>
            {
                news.SetNewsDate(newsDate);
                return news;
            },
                new PatchData
                {
                    Op = "Update",
                    Path = "Tone",
                    Value = ((int)tone).ToString()
                });
        }

        /// <summary>
        /// Creates news item with Reach and Circulation value.
        /// </summary>
        /// <param name="toneId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public NewsItem CreateTestTonedNewsWithReachPubValue(Analytics.Common.ToneId toneId, DateTime dateTime)
        {
            return CreateTestNewsItem(news =>
            {
                news.SetNewsDate(dateTime);
                return news;
            },
                new PatchData // Tone
                {
                    Op = "Update",
                    Path = "Tone",
                    Value = ((int)toneId).ToString()
                },
                new PatchData // Reach
                {
                    Op = "Update",
                    Path = "circulationAudience",
                    Value = new SecureRandom().Next(10000, 99999).ToString()
                },
                new PatchData // Pub value
                {
                    Op = "Update",
                    Path = "publicityValue",
                    Value = new SecureRandom().Next(10000, 99999).ToString()
                });
        }

        /// <summary>
        /// GET News Clips by Including OR Excluding Outlets Lists
        /// </summary>
        /// <param name="criteria">Search Criteria</param>
        /// <param name="operation">Include OR Exclude operation</param>
        /// <returns>IRestResponse<NewsView></returns>
        public IRestResponse<NewsView> GetNewsByIncludeExclude(NewsSearchCriteria criteria, NewsSearchOperator operation, int limit = -1)
        {
            IRestResponse<NewsQuery> response;
            switch (criteria)
            {
                case NewsSearchCriteria.Outlet_Name:
                    var arrOutletNameData = new List<int>();
                    var otlts = TestData.DeserializedJson<List<Outlet>>("NewsOutlet.json", Assembly.Load("CCC-API"));
                    arrOutletNameData.Add(otlts.FirstOrError($"No Outlet was found in JSON file").Id);
                    var postDataOutlets = new NewsQueryOutletsIncludeExcludePostData()
                    {
                        q_outlets = new NewsQueryMultiTypePostData()
                        {
                            ids = arrOutletNameData,
                            @operator = operation.Equals(NewsSearchOperator.Exclude) ? "notequals" : "equals"
                        }
                    };
                    response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataOutlets);
                    break;
                case NewsSearchCriteria.Outlet_List:
                    List<int> outletslist = new List<int>();
                    var udfs = TestData.DeserializedJson<List<OutletsList>>("NewsOutletsLists.json", Assembly.Load("CCC-API"));
                    outletslist.Add(udfs.FirstOrError($"No Outlet List was found in JSON file").Id);
                    var postData = new NewsQueryOutletsListIncludeExcludePostData()
                    {
                        q_outletlists = new NewsQueryMultiTypePostData()
                        {
                            ids = outletslist,
                            @operator = operation.Equals(NewsSearchOperator.Exclude) ? "notequals" : "equals"
                        }
                    };
                    response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
                    break;
                case NewsSearchCriteria.Invalid_Outlet_Name:
                    var arrInvalidOutletNameData = new List<string> { "3111s6" };
                    var postDataInvalidOutlet = new NewsQueryOutletsInvalidPostData()
                    {
                        q_outlets = new NewsQueryMultiTypeStringPostData()
                        {
                            ids = arrInvalidOutletNameData,
                            @operator = "equals"
                        }
                    };
                    response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataInvalidOutlet);
                    break;
                default:
                    throw new ArgumentException(Err.Msg($"'{criteria}' is not a valid criteria to search News by Include/Exclude"));
            }
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Browses a collection of news items
        /// </summary>
        /// <param name="outletName"></param>
        /// <returns>
        /// Returns TRUE if all the items are coming from the given/expected Outlet
        /// Returns FALSE if at least one item in the collection does not come from the given/expected Outlet
        /// </returns>
        public bool AllItemsFromOutlet(List<NewsItem> items, string outletName = null)
        {
            outletName = outletName ?? TestData.DeserializedJson<List<Outlet>>("NewsOutlet.json", Assembly.Load("CCC-API")).FirstOrError().Name;
            return items.All(i => i.Outlet.Name.ToUpper().Equals(outletName.ToUpper()));
        }

        /// <summary>
        /// Search News by Outlet Name (Include/Exclude) & Keywords
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByKeywordsAndOutletName(string keywords, int limit = -1, string outletName = null)
        {
            IRestResponse<NewsQuery> response;
            var arrOutletNameData = new List<int>();
            if (outletName == null)
            {
                var otlts = TestData.DeserializedJson<List<Outlet>>("NewsOutlet.json", Assembly.Load("CCC-API"));
                arrOutletNameData.Add(otlts.FirstOrError($"No Outlet was found in JSON file").Id);
            }
            else
            {
                arrOutletNameData.Add(Convert.ToInt32(outletName));
            }
            var postData = new NewsQueryOutletsIncludeExcludeAndKeywordsPostData()
            {
                q_outlets = new NewsQueryMultiTypePostData()
                {
                    ids = arrOutletNameData,
                    @operator = "equals"
                },
                q_keywords = keywords
            };
            response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Returns TRUE if all clips in the list include the keyword either in the clip's Text or Headline
        /// </summary>
        /// <returns></returns>
        public bool IsKeywordContainedInNewsClipTextOrHeadline(List<NewsItem> items, string keyword) => items.All(i => i.Text.ToLower().Contains(keyword) || i.Headline.ToLower().Contains(keyword));

        /// <summary>
        /// Returns the ID for the first matching facet
        /// </summary>
        /// <param name="category"></param>
        /// <param name="facets"></param>
        /// <returns></returns>
        public SingleFacet GetSingleFacetByCategory(string category, List<SingleFacet> facets) => facets.FirstOrDefault(f => f.Category.ToLower().Equals(category));

        /// <summary>
        /// GET News with single facets applied given a search result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facetId"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsWithSingleFacet(string key, string facetId) => Get<NewsView>($"{NewsViewEndPoint}?key={key}&facets={facetId}");

        /// <summary>
        /// Returns True if all news items have selected facet
        /// </summary>
        /// <param name="facetName"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool AreAllClipsFromGivenFacet(string facetName, List<NewsItem> items) => items.All(i => i.Type.Name.ToLower().Equals(facetName.ToLower()));

        /// <summary>
        /// Gets a single custom field by Type and Multiselect given values
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <param name="multiselect"></param>
        /// <returns></returns>
        public NewsCustomFields GetCustomFieldByTypeAndMultiSelect(List<NewsCustomFields> list, string type, bool multiselect) =>
            list.FirstOrError(cf => cf.Type.ToLower().Equals(type) && cf.MultiSelect.Equals(multiselect), "Not found '{type}' and '{multiselect}'");

        /// <summary>
        /// Gets the first allowed value for a single custom field
        /// </summary>
        /// <param name="singleCustomField"></param>
        /// <returns></returns>
        public string GetCustomFieldFirstAllowedValue(NewsCustomFields singleCustomField)
        {
            var customFieldId = singleCustomField.Id;
            var retValue = singleCustomField.AllowedValues.FirstOrError().Value;
            return retValue;
        }

        /// <summary>
        /// Performs a News Search by INclude/Exclude a Custom Field allowed value 
        /// Only applicable to custom fields => SingleSelect & MultiSelect
        /// </summary>
        /// <param name="customField"></param>
        /// <param name="operation"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByCustomFieldIncludeExclude(NewsCustomFields customField, NewsSearchOperator operation, int limit = 10)
        {
            IRestResponse<NewsQuery> response;
            List<int> valuesList = new List<int>();
            var customFieldId = customField.Id;
            valuesList.Add(customField.AllowedValues.FirstOrError().Id);
            NewsCustomFieldPostData newsCustomField = new NewsCustomFieldPostData()
            {
                values = valuesList,
                id = customField.Id.ToString(),
                @operator = operation.Equals(NewsSearchOperator.Exclude) ? "notequals" : "equals"
            };
            List<NewsCustomFieldPostData> postDataList = new List<NewsCustomFieldPostData>();
            postDataList.Add(newsCustomField);
            var postDataCustomField = new NewsCustomFieldsIncludeExcludePostData()
            {
                q_customfields = postDataList,
            };
            response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataCustomField);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Returns TRUE if al the nes clips in the given collection have the custom field allowed value
        /// </summary>
        /// <param name="items"></param>
        /// <param name="allowedValue"></param>
        /// <param name="singleCustomField"></param>
        /// <returns></returns>
        public bool IsNewsCustomFieldAllowedValueInAllNewsClips(List<NewsItem> items, string allowedValue, NewsCustomFields singleCustomField) =>
            items.All(i => i.CustomFields.FirstOrError(cf => cf.Id.Equals(singleCustomField.Id), $"Custom Field Id '{singleCustomField.Id}' not found.").Value.Exists(cfv => cfv.Equals(allowedValue)));

        /// <summary>
        /// Gets News by Include/Exclude a Tag 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="operation"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByTagIncludeExclude(string tagName, NewsSearchOperator operation, int limit = 10)
        {
            IRestResponse<NewsQuery> response;
            NewsTagsService newsTagsService = new NewsTagsService(SessionKey);
            List<int> tagIds = new List<int>();
            tagIds.Add(newsTagsService.GetTagIdByName(tagName));
            var postDataTags = new NewsQueryTagsIncludeExcludePostData()
            {
                q_tags = new NewsQueryMultiTypePostData()
                {
                    ids = tagIds,
                    @operator = operation.Equals(NewsSearchOperator.Exclude) ? "notequals" : "equals"
                }
            };
            response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataTags);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Returns TRUE if all News Clips form the search result are tagged with given Tag
        /// </summary>
        /// <param name="items"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public bool IsTagNamePresentInAllNewsClips(List<NewsItem> items, string tagName) => items.All(i => i.Tags.Any(t => t.Name.ToLower().Equals(tagName.ToLower())));

        /// <summary>
        /// Gets News by a single Analytics Fields by Include or Exclude its value
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="operation"></param>
        /// <param name="productValue"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByIncludeExcludeAnalytics(NewsSearchCriteria criteria, NewsSearchOperator operation, string productValue, int limit = 10)
        {
            IRestResponse<NewsQuery> response;
            List<int> valuesList = new List<int>();
            var NewsAnalyticsField = TestData.DeserializedJson<List<NewsAnalyticsFields>>("NewsAnalyticsFields.json", Assembly.Load("CCC-API"));
            var NewsAnalyticsSearch = TestData.DeserializedJson<List<NewsItemAnalyticsSearch>>("NewsAnalyticsProduct.json", Assembly.Load("CCC-API"));
            valuesList.Add(NewsAnalyticsSearch.FirstOrError(s => s.Name.ToLower().Equals(productValue.ToLower()), $"No Analytics Product was found in JSON file").Id);
            var newsAnalyticsField = new NewsAnalyticsPostdata()
            {
                values = valuesList,
                id = NewsAnalyticsField.FirstOrError(f => f.Name.ToLower().Equals(criteria.ToString().ToLower()), $"No Analytics Fields were foun with name {criteria}").Id.ToString(),
                @operator = operation.Equals(NewsSearchOperator.Exclude) ? "notequals" : "equals"
            };
            List<NewsAnalyticsPostdata> postDataList = new List<NewsAnalyticsPostdata>();
            postDataList.Add(newsAnalyticsField);
            var postDataAnalytics = new NewsAnalyticsIncludeExcludePostData()
            {
                q_analyticssearches = postDataList
            };
            response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataAnalytics);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Returns TRUE if all the News Clips from a given List have the expected value in Analytics Fiels
        /// </summary>
        /// <param name="items"></param>
        /// <param name="analyticsSearchName"></param>
        /// <returns></returns>
        public bool IsAnalyticsFieldValueInRandomNewsClip(int newsId, string analyticsSearchName = null, string analyticsSearchId = null)
        {
            analyticsSearchName = analyticsSearchName ?? TestData.DeserializedJson<List<NewsItemAnalyticsSearch>>("NewsAnalyticsProduct.json", Assembly.Load("CCC-API")).FirstOrError().Name;
            analyticsSearchId = analyticsSearchId ?? TestData.DeserializedJson<List<NewsItemAnalyticsSearch>>("NewsAnalyticsProduct.json", Assembly.Load("CCC-API")).FirstOrError().Id.ToString();
            var response = GetSingleNews(newsId);
            return response.Data.Analytics.Searches.Any(s => s.Name.Equals(analyticsSearchName) && s.Id.ToString().Equals(analyticsSearchId));
        }

        /// <summary>
        /// Gets a Single Facet Value by its Text
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="facets"></param>
        /// <returns></returns>
        public string GetSingleFacetValueByText(string typeName, List<SingleFacet> facets) => facets.FirstOrError(f => f.Text.ToLower().Equals(typeName.ToLower()), $"Type Name '{typeName}' not found.").Value;

        /// <summary>
        /// Returns the ID for the first matching facet by Category and Text
        /// </summary>
        /// <param name="category"></param>
        /// <param name="text"></param>
        /// <param name="facets"></param>
        /// <returns></returns>
        public SingleFacet GetSingleFacetByText(string text, List<SingleFacet> facets) => facets.FirstOrDefault(f => f.Text.Equals(text));

        /// <summary>
        /// Returns a single News Item by its Category text
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public NewsItem GetSingleNewsIdByFacetText(string typeName, List<NewsItem> items) => items.FirstOrError(i => i.Type.Name.ToLower().Equals(typeName.ToLower()), $"Type Name '{typeName}' not found.");

        /// <summary>
        /// Performs a PATCH to a Single News Item's Type Name
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IRestResponse<NewsItem> PatchSingleNewsItemTypeValue(int newsId, string value)
        {
            var arrayPatchData = new[] { new PatchData("Update", "newstype", value) };
            return Patch<NewsItem>($"{NewsViewEndPoint}/{newsId}", arrayPatchData);
        }

        public IRestResponse<NewsView> GetNewsByIncludeExcludeSmartTags(NewsSearchOperator operation, int smartTagId, int limit = 10)
        {
            IRestResponse<NewsQuery> response;
            List<int> smartTagsIds = new List<int> { smartTagId };
            var postDataSmartTags = new NewsQuerySmartTagsIncludeExcludePostData
            {
                q_smarttags = new NewsQueryMultiTypePostData()
                {
                    ids = smartTagsIds,
                    @operator = operation.Equals(NewsSearchOperator.Exclude) ? "notequals" : "equals"
                }
            };
            response = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postDataSmartTags);
            var uri = $"{NewsViewEndPoint}?key={response.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Gets News by Keywords, Start Date and End Date
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByKeywordsStartDateEndDate(string keywords, string startDate, string endDate, int limit = 10)
        {
            var postData = new NewsQueryKeywordsStartDateEndDatePostData()
            {
                q_keywords = keywords,
                q_startdate = startDate,
                q_enddate = endDate
            };
            var postResponse = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            var uri = $"{NewsViewEndPoint}?key={postResponse.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Returns TRUE if all clips dates in a list are within a given date range
        /// </summary>
        /// <param name="items"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool AreNewsClipsDateContainedInDateRange(List<NewsItem> items, DateTime startDate, DateTime endDate)
        {
            bool retValue = false;
            var date = endDate.AddDays(1);
            foreach (NewsItem i in items)
            {
                if (Convert.ToDateTime(i.NewsDate).IsBetweenInclusive(startDate, date))
                    retValue = true;
                else
                    throw new ArgumentException(Err.Msg($"News Clip with ID {i.Id} is not within expected Date Range"));
            }
            return retValue;
        }

        /// <summary>
        /// Sorts the news results by any sort direction
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public IRestResponse<NewsView> SortNewsResults(string key, string sortField, string sortDirection, int limit = 100, int page = 1) =>
            Request().Get()
                .ToEndPoint($"{NewsViewEndPoint}?key={key}&limit={limit}&page={page}&sortdirection={sortDirection}&sortfield={sortField}")
                .Exec<NewsView>();

        /// </summary>
        /// <param name="newsItemId"></param>
        /// <param name="property"></param>
        /// <param name="patchValue"></param>
        /// <returns></returns>
        public IRestResponse<NewsItem> PatchNewsItemProperty(string newsItemId, string property, string patchValue)
        {
            var op = "Update";
            var path = property;
            var value = patchValue;
            var arrayPatchData = new[] { new PatchData(op, path, value) };
            var response = Patch<NewsItem>($"{NewsViewEndPoint}/{newsItemId}", arrayPatchData);
            return response;
        }

        /// <summary>
        /// Patches news item.
        /// </summary>
        /// <param name="newsItemId"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        public IRestResponse<NewsItem> PatchNewsItem(int newsItemId, params PatchData[] patch) =>
            Request().Patch().ToEndPoint($"{NewsViewEndPoint}/{newsItemId}").Data(patch).Exec<NewsItem>();

        /// <summary>
        /// Facets News by two facets of the given category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByTwoFacetsOfTheSameCategory(string searchKey, List<SingleFacet> facets, string category)
        {
            IEnumerable<SingleFacet> facetsList = facets.FindAll(f => f.CategoryText.Equals(category));
            IEnumerable<string> facetsIds = facetsList == null ? new List<string>() : facetsList.Select(i => i.Id.ToString()).TakeExactly(2);
            return Get<NewsView>($"{NewsViewEndPoint}?key={searchKey}&facets={facetsIds.ToList()[0]},{facetsIds.ToList()[1]}");
        }

        /// <summary>
        /// Factes News by two facets of different categories
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="facets"></param>
        /// <param name="category1"></param>
        /// <param name="category2"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByTwoFacetsOfDifferentCategories(string searchKey, List<SingleFacet> facets, string category1, string category2)
        {
            var facet1 = facets.FirstOrError(f => f.CategoryText.Equals(category1), $"Category '{category1}' not found.").Id.ToString();
            var facet2 = facets.FirstOrError(f => f.CategoryText.Equals(category2), $"Category '{category2}' not found.").Id.ToString();
            return Get<NewsView>($"{NewsViewEndPoint}?key={searchKey}&facets={facet1},{facet2}");
        }

        /// <summary>
        /// Get News by all news details criteria (keywords, startDate, endDate, tone, tags, smartTags)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByAllNewsDetails(int limit = 100)
        {
            var postData = new NewsQueryAllDetailsPostData()
            {
                q_keywords = "marvel",
                q_startdate = DateTime.Today.AddDays(-10).ToString("M/d/yyyy"),
                q_enddate = DateTime.Now.ToString("M/d/yyyy"),
                q_smarttags = new NewsQueryMultiTypePostData() { ids = GetRandomSingleSmartTagId(), @operator = "equals" },
                q_tags = new NewsQueryMultiTypePostData() { ids = GetRandomSingleTagId(), @operator = "equals" },
                q_tones = GetRandomSingleToneId()
            };
            var postResponse = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            var uri = $"{NewsViewEndPoint}?key={postResponse.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// Gets a single random smart tag id as a list
        /// </summary>
        /// <returns></returns>
        public List<int> GetRandomSingleSmartTagId()
        {
            var response = Get<NewsTypes>($"{NewsViewEndPoint}/smarttags");
            var items = response.Data.Items;
            return items.Select(i => i.Id).TakeExactly(1).ToList();
        }

        /// <summary>
        /// Gets a single random tag id as a list
        /// </summary>
        /// <returns></returns>
        public List<int> GetRandomSingleTagId()
        {
            var response = Get<NewsTags>($"{NewsViewEndPoint}/tags");
            var items = response.Data.Items;
            return items.Select(i => i.Id).TakeExactly(1).ToList();
        }

        /// <summary>
        /// Gets a single random tone id as a list
        /// </summary>
        /// <returns></returns> 
        public List<int> GetRandomSingleToneId()
        {
            var response = Get<NewsTags>($"{NewsViewEndPoint}/tones");
            var items = response.Data.Items;
            return items.Select(i => i.Id).TakeExactly(1).ToList();
        }

        /// <summary>
        /// Deletes single news item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteSingleNewsItem(int id) => Request().Delete().ToEndPoint($"{NewsViewEndPoint}/{id}").Exec();

        /// <summary>
        /// Gets News by Company Tone
        /// </summary>
        /// <param name="companyToneId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByCompanyTone(int companyToneId, int limit = 10)
        {
            var postData = new NewsQueryCompanyTonePostData
            {
                q_companytone = companyToneId
            };
            var postResponse = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            var uri = $"{NewsViewEndPoint}?key={postResponse.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// PATCH Bulk Edit Advanced Analytics
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsView> PatchBulkEditAdvancedAnalyitcs(string key)
        {
            Random rnd = new Random();
            var impact = 200;
            var prominence = rnd.Next(1, 51);
            var op = "Update";
            var path = "analyticssearches/3448";
            var value = "{\"tone\":-2,\"impact\":" + impact + ",\"prominence\":" + prominence + "}";
            var patchData = new[] { new PatchData { Op = op, Path = path, Value = value } };
            var bulkPatchData = new BulkPatchDataDelta(key, true, patchData, null);
            return Patch<NewsView>(NewsViewEndPoint, bulkPatchData);
        }

        /// <summary>
        /// Gets CEDROM News Caption Url
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsCedromClipLinks> GetCedromLinks()
        {
            var date = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
            var hour = DateTime.Now.ToString("HHmmss");
            var uri = $"{NewsViewEndPoint}/clip/cedrom?channelId=67&date={date}&hour={hour}&length=180&format=mp4";
            return Get<NewsCedromClipLinks>(uri);
        }

        /// <summary>
        /// Gets a single news item from CEDROM feed
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int GetCedromClipId(List<NewsItem> items)
        {
            int newsId = 0;
            IEnumerable<NewsItem> news = items.Where(i => i.Feed.Id.Equals(105));
            foreach (NewsItem n in news)
            {
                if (Get<NewsView>($"{NewsViewEndPoint}/{n.Id}/clip/cedrom/segments").StatusDescription.Equals("Not Found"))
                {
                    newsId = n.Id;
                    break;
                }
            }
            return newsId;
        }

        /// <summary>
        /// Creates a CEDROM video segment
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IRestResponse<SegmentItem> CreateCedromClip(int newsId)
        {
            Random rnd = new Random();
            var postData = new CedromClipPostData { StartSecond = rnd.Next(1, 30), EndSecond = rnd.Next(30, 180), IsPrimary = true };
            return Post<SegmentItem>($"{NewsViewEndPoint}/{newsId}/clip/cedrom/segments", postData);
        }

        /// <summary>
        /// Updates a CEDROM video segment
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IRestResponse<SegmentItem> UpdateCedromClip(int newsId)
        {
            Random rnd = new Random();
            var segmentId = GetCedromClipSegment(newsId).Data.Items.FirstOrDefault().Id;
            var putData = new CedromClipPutData { StartSecond = rnd.Next(1, 30), EndSecond = rnd.Next(30, 180), IsPrimary = true, Name = "AutomationEdit" };
            return Put<SegmentItem>($"{NewsViewEndPoint}/{newsId}/clip/cedrom/segments/{segmentId}", putData);
        }

        /// <summary>
        /// Gets a single news item from CEDROM feed with previously edited video
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int GetCedromClipIdWithEditedVideo(List<NewsItem> items)
        {
            int newsId = 0;
            IEnumerable<NewsItem> news = items.Where(i => i.Feed.Id.Equals(105));
            foreach (NewsItem n in news)
            {
                var status = Get<NewsView>(NewsViewEndPoint + $"/{n.Id}/clip/cedrom/segments").StatusDescription;
                if (status.Equals("OK"))
                {
                    newsId = n.Id;
                    break;
                }
            }
            return newsId;
        }

        /// <summary>
        /// Gets Cedrom Clip Segment
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IRestResponse<Segments> GetCedromClipSegment(int newsId) =>
            Request().Get().ToEndPoint($"{NewsViewEndPoint}/{newsId}/clip/cedrom/segments/").Exec<Segments>();

        /// <summary>
        /// Gets News Item links for Cedrom clip
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IRestResponse<NewsItemLinks> GetCedromClipNewsItemLinks(int newsId) =>
            Request().Get().ToEndPoint($"{NewsViewEndPoint}/{newsId}").Exec<NewsItemLinks>();

        /// <summary>
        /// Gets a random double value
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        double GetRandomDouble(Random random, double min, double max) => min + (random.NextDouble() * (max - min));

        /// <summary>
        /// Creates a Cedrom Video Clip with SubSeconds
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IRestResponse<SegmentItem> CreateCedromClipWithSubSeconds(int newsId)
        {
            Random rnd = new Random();
            var min = GetRandomDouble(rnd, 1, 15);
            var max = GetRandomDouble(rnd, 15, 30);
            var start = Math.Round(min, 3);
            var end = Math.Round(max, 3);
            
            var postData = new CedromClipWithSubSecondsPostData { StartSecond = start, EndSecond = end, IsPrimary = true };
            return Post<SegmentItem>($"{NewsViewEndPoint}/{newsId}/clip/cedrom/segments", postData);
        }

        /// <summary>
        /// GET news by Social Location
        /// </summary>
        /// <param name="locationsIds"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsBySocialLocations(List<string> locationsIds, int limit = 100)
        {
            var postData = new NewsQuerySocialLocationsPostData()
            {
                q_sociallocations = new NewsQueryMultiTypeStringPostData()
                {
                    ids = locationsIds, @operator = "equals"
                }
            };
            var postResponse = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            var uri = $"{NewsViewEndPoint}?key={postResponse.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }

        /// <summary>
        /// GET a Cedrom Item from a specific feed
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int GetCedromClipFromSpecificFeed(List<NewsItem> items)
        {
            IEnumerable<NewsItem> news = items.Where(i => i.Feed.Id.Equals(207));
            return news.First().Id;
        }

        /// <summary>
        /// GET News by Outlet Media Type
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IRestResponse<NewsView> GetNewsByMediaType(int limit = 100)
        {
            List<string> mediaTypeIds = new List<string>{ "170000" };
            var postData = new NewsQueryMediaTypePostData()
            {
                q_outletmediatypes = new NewsQueryMultiTypeStringPostData()
                {
                    ids = mediaTypeIds,
                    @operator = "equals"
                }
            };
            var postResponse = Post<NewsQuery>(NewsQueryEndpoint, GetAuthorizationHeader(), postData);
            var uri = $"{NewsViewEndPoint}?key={postResponse.Data.Key}";
            if (limit > 0) uri += $"&limit={limit}";
            return Get<NewsView>(uri);
        }
    }
}