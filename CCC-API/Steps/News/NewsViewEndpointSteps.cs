using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.Common;
using CCC_API.Services.Media.Outlet;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;
using static CCC_API.Services.News.NewsViewService;
using Is = NUnit.Framework.Is;
using static CCC_API.Services.News.NewsReportsService;

namespace CCC_API.Steps.News
{
    [Binding]
    public class NewsViewEndpointSteps : AuthApiSteps
    {
        private NewsViewService _newsViewService;
        private NewsTagsService _newsTagService;
        private NewsSmartTagsService _newsSmartTagsService;
        private NewsReportsService _newsReportsService;
        private const string FACETS = "facets";
        public const string GET_NEWS_RESPONSE_KEY = "GetNewsResponse";
        private const string GET_FACETS_RESPONSE_KEY = "GetFacetsResponse";
        private const string GET_SINGLE_NEWS_ITEM_RESPONSE = "GetSingleNewsItemResponse";
        private const string GET_CLIP_REPORT_FIELDS_KEY = "GetClipReportFieldsResponse";
        private const string PATCH_NEWS_CUSTOM_FIELD = "PatchNewsCustomField";
        private const string GET_CEDROM_CLIP = "GetCedromClip";
        private const string PATCH_BULK_NEWS_CUSTOM_FIELD = "PatchBulkNewsCustomField";
        private const string GET_CUSTOM_FIELDS_RESPONSE = "GetCustomFieldsResponseKey";
        private const string GET_NEWS_ORDERED_BY_CUSTOM_FIELD = "GetNewsOrderedByCustomField";
        private const string GET_NEWS_BY_SINGLE_FACET_KEY = "GetNewsBySingleFacet";
        private const string GET_NEWS_BY_EXCLUDE_FIELD_KEY = "GetNewsByExcludeFieldKey";
        private const string GET_NEWS_BY_INCLUDE_FIELD_KEY = "GetNewsByIncludeFieldKey";
        private const string GET_SINGLE_CUSTOM_FIELD = "GetSingleCustomField";
        private const string GET_SINGLE_SMART_TAG_TEXT = "GetSingleSmartTag";
        private const string PATCH_SINGLE_NEWS_RESPONSE = "PatchSingleNewsResponse";
        private const string GET_ALL_SMART_TAGS_RESPONSE = "GetAllSmartTagsResponse";
        private const string GET_SINGLE_SMART_TAG_INCLUDE_RESPONSE = "GetSingleSmartTagIncludeResponse";
        private const string GET_SINGLE_SMART_TAG_EXCLUDE_RESPONSE = "GetSingleSmartTagExcludeResponse";
        private const string GET_NEWS_KEYWORDS_SDATE_EDATE_RESPONSE_KEY = "GetNewsKeywordsStartDateEndDateResponseKey";
        private const string NEWS_ITEM_ID_KEY = "NewsItemIdKey";
        private const string GET_FACETED_NEWS_RESPONSE = "GetFacetedNewsResponse";
        private const string GET_DELETE_NEWS_RESPONSE_KEY = "GetDeleteNewsResponseKey";
        private const string PATCH_RESPONSE_BULK_EDIT_ADVANCED_ANALYTICS = "PatchResponseBulkEditAdvancedAnalytics";
        private const string GET_CEDROM_LINKS_RESPONSE = "GetCedromLinksReponse";
        private const string GET_SINGLE_CEDROM_CLIP_ID = "GetSingleCedromClipId";
        private const string POST_CEDROM_CLIP_SEGMENT_RESPONSE = "PostCedromClipSegmentResponse";
        private const string PUT_CEDROM_CLIP_SEGMENT_RESPONSE = "PutCedromClipSegmentResponse";
        private const string GET_CEDROM_CLIP_SEGMENT_RESPONSE = "GetCedromClipSegmentResponse";
        private const string GET_CEDROM_CLIP_NEWS_LINKS_RESPONSE = "GetCedromClipNewsLinksResponse";
        private const string POST_NEWS_CLIPBOOK_RESPONSE = "PostNewsClipBookResponse";
        private const string GET_ALL_NEWS_CLIPBOOKS_RESPONSE = "GetNewsClipbooksResponse";
        private const string GET_SINGLE_NEWS_CLIPBOOK_RESPONSE = "GetSingleNewsClipbookResponse";
        private const string DELETE_NEWS_CLIPBOOK_RESPONSE = "DeleteClipbookResponse";
        private const string PUT_NEWS_CLIPBOOK_RESPONSE = "PutClipbookResponse";
        private const string GET_NEWS_CLIPBOOK_DEFINITIONS = "GetNewsClipbookDef";
        private const string GET_GEO_PLACES_IDS = "GetGeoPlacesIds";
        private const string GET_NEWS_SORTED = "GetNewsSorted";
        private const string GET_SINGLE_CEDROM_NEWS_ITEM = "GetSingleCedromNewsItem";

        public NewsViewEndpointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _newsViewService = new NewsViewService(SessionKey);
            _newsTagService = new NewsTagsService(SessionKey);
            _newsSmartTagsService = new NewsSmartTagsService(SessionKey);
            _newsReportsService = new NewsReportsService(SessionKey);
        }

        #region Given Steps
        [Given(@"news article exist")]
        public void GivenNewsArticleExist()
        {
            var item = new NewsItemPayload()
            {
                Text = "Test " + StringUtils.RandomAlphaNumericString(20),
                Headline = "Auto news " + StringUtils.RandomAlphaNumericString(10),
                ArticleUrl = "https://www.theguardian.com/uk"
            };
            item.SetNewsDate(DateTime.Now);
            var news = _newsViewService.PostNewsItem(item);
            PropertyBucket.Remember(NewsViewService.NewsViewEndPoint, news);
            PropertyBucket.Remember(NEWS_ITEM_ID_KEY, news.Id);
        }
        #endregion

        #region When Steps
        /// <summary>
        /// Performs a GET to endpoint api/v1/news
        /// </summary>
        [When(@"I perform a GET for all news")]
        public void WhenIPerformAGETForAllNews()
        {
            var response = _newsViewService.GetAllNews();
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        /// <summary>
        /// Performs a GET to endpoint api/v1/news/facets
        /// </summary>
        [When(@"I perform a GET for all available facets")]
        public void WhenIPerformAGETForAllAvailableFacets()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var facetsResponse = _newsViewService.GetAvailableFacets(response.Data.Key);
            PropertyBucket.Remember(GET_FACETS_RESPONSE_KEY, facetsResponse);
        }

        /// <summary>
        /// Performs a GET to endpoint api/v1/news with all Available facets
        /// </summary>
        [When(@"I perform a GET for multiple faceted news")]
        public void WhenIPerformAGETForMultipleFacetedNews()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            IRestResponse<Facets> facetsResponse = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var multiFacetedNews = _newsViewService.GetNewsWithMultipleFacets(response.Data.Key, facetsResponse.Data.Available);
            PropertyBucket.Remember(GET_FACETED_NEWS_RESPONSE, multiFacetedNews);
        }

        [When(@"I perform a GET for news with '(.*)' facets")]
        public void WhenIPerformAGETForNewsWithFacets(List<string> facetCatgories)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var key = response.Data.Key;
            var facetsResponse = _newsViewService.GetAvailableFacets(key);
            var facets = new List<SingleFacet>();
            IRestResponse<NewsView> multiFacetedNews = null;
            foreach (var category in facetCatgories)
            {
                facets.Add(facetsResponse.Data.Available.FirstOrError(c => c.CategoryText.ToLower().Equals(category.ToLower()), $"Category '{category}' not found."));
                //Refresh results to avoid grabbing an invalid facet
                multiFacetedNews = _newsViewService.GetNewsWithMultipleFacets(key, facets);
                facetsResponse = _newsViewService.GetAvailableFacets(key);
            }
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, multiFacetedNews, true);
            PropertyBucket.Remember(FACETS, facets);
        }

        /// <summary>
        /// Performs a GET with parameters to endpoint api/v1/news?q_paramters
        /// </summary>
        /// <param name="criteria">
        ///     The criteria to search by matched with the NewsSearchCriteria enum
        ///     in the NewsViewService. The value must match exactly as the enum value
        /// </param>
        /// <param name="parameters">The parameters to search by, correctly formatted as the endpoint expects</param>
        [When(@"I search for news by '(.*)' with a value of '(.*)'")]
        public void WhenISearchForNewsByWithAValueOf(NewsSearchCriteria criteria, string parameters)
        {
            var response = _newsViewService.GetNewsWithParameters(criteria, parameters);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        /// <summary>
        /// Performs a GET with parameters to endpoint api/v1/news?q_paramters with limit set to TotalCount (all news)
        /// </summary>
        /// <param name="criteria">
        ///     The criteria to search by matched with the NewsSearchCriteria enum
        ///     in the NewsViewService. The value must match exactly as the enum value
        /// </param>
        /// <param name="parameters">The parameters to search by, correctly formatted as the endpoint expects</param>
        [When(@"I search for '(.*)' news by '(.*)' with a value of '(.*)'")]
        public void WhenISearchForAllNewsByWithAValueOf(int limit, NewsSearchCriteria criteria, string parameters)
        {
            var response = _newsViewService.GetNewsWithParameters(criteria, parameters, limit);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        /// <summary>
        /// Performs a GET with news clip Id to endpoint api/v1/news/NEWS_ID
        /// </summary>
        [When(@"I perform GET to a single news item from search")]
        public void WhenIPerformAGetForASingleItemFromSearch()
        {
            IRestResponse<NewsItem> resp = null;
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            List<NewsItem> items = response.Data.Items;
            foreach (var item in items)
            {
                if (item.Id != 0)
                {
                    resp = _newsViewService.GetSingleNews(item.Id);
                    break;
                }
            }

            PropertyBucket.Remember(GET_SINGLE_NEWS_ITEM_RESPONSE, resp);
        }

        /// <summary>
        /// Performs a GET to endpoint news/reports/clipreport/fields
        /// </summary>
        [When(@"I perform a GET to Clip Report endpoint")]
        public void IPerformAGetToClipReportEndpoint()
        {
            var response = _newsViewService.GetClipReportFields();
            PropertyBucket.Remember(GET_CLIP_REPORT_FIELDS_KEY, response);
        }

        /// <summary>
        /// Performs a PATCH to endpoint customfields/[CUSTOM_FIELD_ID]
        /// </summary>
        /// <param name="type"></param>
        [When(@"I perform a PATCH to News Custom Field with type '(.*)' from Single News")]
        public void WhenIPerformAPatchToAGivenNewsCustomField(string type)
        {
            var singleNewsId = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(GET_SINGLE_NEWS_ITEM_RESPONSE).Data.Id.ToString();
            var customFields = PropertyBucket.GetProperty<IRestResponse<NewsCustomFieldsView>>(GET_CUSTOM_FIELDS_RESPONSE).Data.Items;
            var singleCustomField = customFields.FirstOrDefault(cf => cf.Type.Equals(type, StringComparison.CurrentCultureIgnoreCase))?.Id ?? 0;
            var response = _newsViewService.PatchNewsCustomField(singleCustomField.ToString(), singleNewsId);
            PropertyBucket.Remember(PATCH_NEWS_CUSTOM_FIELD, response);
        }

        /// <summary>
        /// Performs a PATCH to endpoint customfields/[CUSTOM_FIELD_ID] to null out value
        /// </summary>
        /// <param name="type"></param>
        [When(@"I perform a PATCH to null out News Custom Field with type '(.*)' from Single News")]
        public void WhenIPerformAPatchToNullOutAGivenNewsCustomField(string type)
        {
            var singleNewsId = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(GET_SINGLE_NEWS_ITEM_RESPONSE).Data.Id.ToString();
            var customFields = PropertyBucket.GetProperty<IRestResponse<NewsCustomFieldsView>>(GET_CUSTOM_FIELDS_RESPONSE).Data.Items;
            var singleCustomField = customFields.FirstOrDefault(cf => cf.Type.Equals(type, StringComparison.CurrentCultureIgnoreCase))?.Id ?? 0;
            var response = _newsViewService.PatchNullOutNewsCustomField(singleCustomField.ToString(), singleNewsId);
            PropertyBucket.Remember(PATCH_NEWS_CUSTOM_FIELD, response);
        }

        /// <summary>
        /// Performs a GET to the CEDROM Clip endpoint
        /// </summary>
        [When(@"I perform a GET to the CEDROM clip endpoint")]
        public void WhenIPerformAGetToTheCedromClipEndpoint()
        {
            var response = _newsViewService.GetNewsItemClip();
            PropertyBucket.Remember(GET_CEDROM_CLIP, response);
        }

        [When(@"I perform a PATCH to all the results to update '(.*)' custom field")]
        public void WhenIPerformAPatchToAllTheResultsToUpdateCustomFields(string customField)
        {
            var key = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            var response = _newsViewService.PatchBulkNewsCustomField(key, customField);
            PropertyBucket.Remember(PATCH_BULK_NEWS_CUSTOM_FIELD, response);
        }

        [When(@"I perform a GET for all available custom fields")]
        public void WhenIPerfomAGetForAllAvailableCustomFields()
        {
            var response = _newsViewService.GetAllCustomFields();
            PropertyBucket.Remember(GET_CUSTOM_FIELDS_RESPONSE, response);
        }

        [When(@"I perform a GET for news ordered by allowed custom field")]
        public void WhenIPerformAGetForNewsOrderedByAllowedCustomField()
        {
            var key = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            var list = PropertyBucket.GetProperty<IRestResponse<NewsCustomFieldsView>>(GET_CUSTOM_FIELDS_RESPONSE).Data.Items;
            var response = _newsViewService.GetNewOrderedBySingleCustomField(key, list);
            PropertyBucket.Remember(GET_NEWS_ORDERED_BY_CUSTOM_FIELD, response);
        }

        [When(@"I perform a GET for news faceted by '(.*)'")]
        public void WhenIPerformAGetForNewsFacetedByGivenCriteria(string facet)
        {
            var key = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            IRestResponse<Facets> facetsResponse = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var response = _newsViewService.GetNewsBySingleFacet(key, facetsResponse.Data.Available, facet);
            PropertyBucket.Remember(GET_NEWS_BY_SINGLE_FACET_KEY, response);
        }

        [When(@"I search for news by (start|end) date with a value of '(.*)'")]
        public void WhenISearchForNewsByStartDateWithAValueOf(string dateType, DateTime date)
        {
            var response = _newsViewService.GetNewsBySingleDate(dateType, date);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        [When(@"I search for news with a start date or '(.*)' and an end date of '(.*)'")]
        public void WhenISearchForNewsWithAStartDateOrAndAnEndDateOf(DateTime start, DateTime end)
        {
            var response = _newsViewService.GetNewsByDateRange(start, end);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for news ordered by inexistent custom field")]
        public void WhenIPerformAGetForNewsOrderedByInexistentCustomField()
        {
            var response = _newsViewService.GetNewOrderedByInexistentCustomField(PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key);
            PropertyBucket.Remember(GET_NEWS_ORDERED_BY_CUSTOM_FIELD, response);
        }

        [When(@"I perform a GET for news by '(.*)' using the '(.*)' operator")]
        public void WhenIPerformAGetForNewsByIncludingOrExcludingAGivenField(NewsSearchCriteria criteria, NewsViewService.NewsSearchOperator operation)
        {
            var response = _newsViewService.GetNewsByIncludeExclude(criteria, operation);
            switch (operation)
            {
                case NewsSearchOperator.Exclude:
                    PropertyBucket.Remember(GET_NEWS_BY_EXCLUDE_FIELD_KEY, response);
                    break;
                case NewsSearchOperator.Include:
                    PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
                    break;
            }
        }

        [When("I perform a GET for news by Keywords '(.*)' and Outlet Name")]
        public void WhenIPerformAGetForNewsByGivenKeywordsAndOutletName(string keywords)
        {
            var response = _newsViewService.GetNewsByKeywordsAndOutletName(keywords);
            PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
        }

        [When(@"I perform a GET for news by single facet with category '(.*)'")]
        public void WhenIPerformAGETForNewsBySingleFacetWithCategory(string categoryName)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var facets = response.Data.Available;
            var singleFacet = _newsViewService.GetSingleFacetByCategory(categoryName, facets);
            var searchKey = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            var facetedResponse = _newsViewService.GetNewsWithSingleFacet(searchKey, singleFacet.Id.ToString());
            PropertyBucket.Remember(GET_NEWS_BY_SINGLE_FACET_KEY, facetedResponse);
            PropertyBucket.Remember(GET_SINGLE_SMART_TAG_TEXT, singleFacet.Text);
        }

        [When(@"I select the first with Type '(.*)' and a MultiSelect value of '(.*)'")]
        public void WhenISelectTheFirstWithTypeAndAMultiSelectValueOf(string type, bool multiselect)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsCustomFieldsView>>(GET_CUSTOM_FIELDS_RESPONSE);
            var customFields = response.Data.Items;
            var singleCustomField = _newsViewService.GetCustomFieldByTypeAndMultiSelect(customFields, type, multiselect);
            PropertyBucket.Remember(GET_SINGLE_CUSTOM_FIELD, singleCustomField);
        }

        [When(@"I perform a GET for news by selected Custom Field using the '(.*)' operator")]
        public void WhenIPerformAGETForNewsBySelectedCustomFieldUsingTheOperator(NewsSearchOperator operation)
        {
            var singleCustomField = PropertyBucket.GetProperty<NewsCustomFields>(GET_SINGLE_CUSTOM_FIELD);
            var response = _newsViewService.GetNewsByCustomFieldIncludeExclude(singleCustomField, operation);
            switch (operation)
            {
                case NewsSearchOperator.Exclude:
                    PropertyBucket.Remember(GET_NEWS_BY_EXCLUDE_FIELD_KEY, response);
                    break;
                case NewsSearchOperator.Include:
                    PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
                    break;
            }
        }

        [When(@"I perform a GET for news by '(.*)' Tag using the '(.*)' operator")]
        public void WhenIPerformAGETForNewsByTagUsingTheOperator(string tagName, NewsSearchOperator operation)
        {
            var response = _newsViewService.GetNewsByTagIncludeExclude(tagName, operation);
            switch (operation)
            {
                case NewsSearchOperator.Exclude:
                    PropertyBucket.Remember(GET_NEWS_BY_EXCLUDE_FIELD_KEY, response);
                    break;
                case NewsSearchOperator.Include:
                    PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
                    break;
            }
        }

        [When(@"I perform a GET for news by News Analytics field '(.*)' and value '(.*)' using the '(.*)' operator")]
        public void WhenIPerformAGETForNewsByNewsAnalyticsFieldUsingTheOperator(NewsSearchCriteria criteria, string productValue, NewsSearchOperator operation)
        {
            var response = _newsViewService.GetNewsByIncludeExcludeAnalytics(criteria, operation, productValue);
            switch (operation)
            {
                case NewsSearchOperator.Exclude:
                    PropertyBucket.Remember(GET_NEWS_BY_EXCLUDE_FIELD_KEY, response);
                    break;
                case NewsSearchOperator.Include:
                    PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
                    break;
            }
        }

        [When(@"I perform a GET for news by facet with text '(.*)'")]
        public void WhenIPerformAGETForNewsBySingleFacetWithCategoryAndText(string text)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var facets = response.Data.Available;
            var singleFacet = _newsViewService.GetSingleFacetByText(text, facets);
            var searchKey = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            var facetedResponse = _newsViewService.GetNewsWithSingleFacet(searchKey, singleFacet.Id.ToString());
            PropertyBucket.Remember(GET_NEWS_BY_SINGLE_FACET_KEY, facetedResponse);
            PropertyBucket.Remember(GET_SINGLE_SMART_TAG_TEXT, singleFacet.Text);
        }

        [When(@"I perform a GET for the first single News Item with faceted type")]
        public void WhenIPerformAGETForTheFirstSingleNewsItemWithType()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_SINGLE_FACET_KEY);
            var typeName = PropertyBucket.GetProperty<string>(GET_SINGLE_SMART_TAG_TEXT);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "The search returned zero results");
            var singleNewsResponse = _newsViewService.GetSingleNewsIdByFacetText(typeName, response.Data.Items);
            PropertyBucket.Remember(GET_SINGLE_NEWS_ITEM_RESPONSE, singleNewsResponse);
        }

        [When(@"I PATCH to update single News Item to Type '(.*)'")]
        public void WhenIPATCHToUpdateSingleNewsItemToType(string typeName)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var singleNewsResponse = PropertyBucket.GetProperty<NewsItem>(GET_SINGLE_NEWS_ITEM_RESPONSE);
            var facets = response.Data.Available;
            var singleFacetValue = _newsViewService.GetSingleFacetValueByText(typeName, facets);
            var patchResponse = _newsViewService.PatchSingleNewsItemTypeValue(singleNewsResponse.Id, singleFacetValue);
            PropertyBucket.Remember(PATCH_SINGLE_NEWS_RESPONSE, patchResponse);
        }

        [When(@"I perform a GET for all smart tags")]
        public void WhenIPerformAGETForAllSmartTags()
        {
            var response = _newsSmartTagsService.GetAllSmartTags();
            PropertyBucket.Remember(GET_ALL_SMART_TAGS_RESPONSE, response);
        }

        [When(@"I perform a GET for news by Smart Tags using the '(.*)' operator")]
        public void WhenIPerformAGETForNewsBySmartTagUsingTheOperator(NewsSearchOperator operation)
        {
            var smartTagsResponse = PropertyBucket.GetProperty<NewsTypes>(GET_ALL_SMART_TAGS_RESPONSE);
            var smartTagItem = _newsSmartTagsService.GetSingleSmartTag(smartTagsResponse.Items);
            var response = _newsViewService.GetNewsByIncludeExcludeSmartTags(operation, smartTagItem.Id);
            switch (operation)
            {
                case NewsSearchOperator.Exclude:
                    PropertyBucket.Remember(GET_NEWS_BY_EXCLUDE_FIELD_KEY, response);
                    PropertyBucket.Remember(GET_SINGLE_SMART_TAG_EXCLUDE_RESPONSE, smartTagItem);
                    break;
                case NewsSearchOperator.Include:
                    PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
                    PropertyBucket.Remember(GET_SINGLE_SMART_TAG_INCLUDE_RESPONSE, smartTagItem);
                    break;
            }
        }

        [When(@"I perform a GET for news by '(.*)' using the Outlet Id '(.*)'")]
        public void WhenIPerformAGETForNewsByUsingTheOutletId(NewsSearchCriteria criteria, NewsSearchOperator operation)
        {
            var response = _newsViewService.GetNewsByIncludeExclude(criteria, operation);
            switch (operation)
            {
                case NewsSearchOperator.Exclude:
                    PropertyBucket.Remember(GET_NEWS_BY_EXCLUDE_FIELD_KEY, response);
                    break;
                case NewsSearchOperator.Include:
                    PropertyBucket.Remember(GET_NEWS_BY_INCLUDE_FIELD_KEY, response);
                    break;
            }
        }

        [When(@"I perform a GET for news by Keywords '(.*)' and Start Date '(.*)' and End Date '(.*)'")]
        public void WhenIPerformAGETForNewsByKeywordsAndStartDateAndEndDate(string keywords, string startDate, string endDate)
        {
            var response = _newsViewService.GetNewsByKeywordsStartDateEndDate(keywords, startDate, endDate);
            PropertyBucket.Remember(GET_NEWS_KEYWORDS_SDATE_EDATE_RESPONSE_KEY, response);
            PropertyBucket.Remember("startDate", startDate);
            PropertyBucket.Remember("endDate", endDate);
        }

        [When(@"I sort news results field '(.*)' by direction '(.*)'")]
        public void WhenISortNewsResultsFieldByDirection(string field, string direction)
        {
            var key = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            var response = _newsViewService.SortNewsResults(key, field, direction);
            PropertyBucket.Remember(GET_NEWS_SORTED, response, true);
        }

        [When(@"I get a random news clip")]
        public void WhenIGetARandomNewsClip()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var singleNewsItem = response.Data.Items.FirstOrDefault().Id.ToString();
            PropertyBucket.Remember(NEWS_ITEM_ID_KEY, singleNewsItem);
        }

        [When(@"I perform a PATCH to update news '(.*)' to '(.*)'")]
        public void WhenIPerformAPATCHToUpdateNewsHeadlineTo(string property, string patchValue)
        {
            var singleNewsItem = PropertyBucket.GetProperty<string>(NEWS_ITEM_ID_KEY);
            var response = _newsViewService.PatchNewsItemProperty(singleNewsItem, property, patchValue);
            PropertyBucket.Remember(PATCH_SINGLE_NEWS_RESPONSE, response);
        }

        [When(@"I facet news by two facets of the '(.*)' category")]
        public void WhenIFacetNewsByTwoFacetsOfTheCategory(string categoryName)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            IRestResponse<Facets> facetsResponse = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var facetedNews = _newsViewService.GetNewsByTwoFacetsOfTheSameCategory(response.Data.Key, facetsResponse.Data.Available, categoryName);
            PropertyBucket.Remember(GET_FACETED_NEWS_RESPONSE, facetedNews);
        }

        [When(@"I facet news by '(.*)' category and '(.*)' category")]
        public void WhenIFacetNewsByCategoryAndCategory(string category1, string category2)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            IRestResponse<Facets> facetsResponse = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var facetedNews = _newsViewService.GetNewsByTwoFacetsOfDifferentCategories(response.Data.Key, facetsResponse.Data.Available, category1, category2);
            PropertyBucket.Remember(GET_FACETED_NEWS_RESPONSE, facetedNews);
        }

        [When(@"I delete a news item")]
        public void WhenIDeleteANewsItem()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var itemId = response.Data.Items.FirstOrError().Id;
            PropertyBucket.Remember(NEWS_ITEM_ID_KEY, itemId);
            var delete = _newsViewService.DeleteSingleNewsItem(itemId);
            PropertyBucket.Remember(GET_DELETE_NEWS_RESPONSE_KEY, delete);
        }

        [When(@"I search for news by location criteria '(.*)' with a value of '(.*)'")]
        public void WhenISearchForNewsByLocationCriteriaOutlet_LocationWithAValueOf(NewsSearchCriteria criteria, string param)
        {
            List<string> list = new List<string>();
            if (criteria.Equals(NewsSearchCriteria.Outlet_Locations))
            {
                var places = new GeoService(SessionKey).GetPlaces(param);
                var id = places.Data.Items.FirstOrError().Id;
                list.Add(id);
            }
            else
            {
                list.Add(param);
            }
            var response = _newsViewService.GetNewsByLocation(criteria, list);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        [When(@"I delete the news item with ID '(.*)'")]
        public void WhenIDeleteTheNewsItemWithID(int id)
        {
            var response = _newsViewService.DeleteSingleNewsItem(id);
            PropertyBucket.Remember(GET_DELETE_NEWS_RESPONSE_KEY, response);
        }

        [When(@"I search for news by Company Tone with value '(.*)'")]
        public void WhenISearchForNewsByCompanyToneWithValue(int companyToneId)
        {
            var response = _newsViewService.GetNewsByCompanyTone(companyToneId);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        [When(@"I perform a PATCH to all the results to update Advanced Analytics field")]
        public void WhenIPerformAPATCHToAllTheResultsToUpdateAdvancedAnalyticsField()
        {
            var responseKey = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Key;
            var response = _newsViewService.PatchBulkEditAdvancedAnalyitcs(responseKey);
            PropertyBucket.Remember(PATCH_RESPONSE_BULK_EDIT_ADVANCED_ANALYTICS, response);
        }

        [When(@"I perform a GET to Cedrom News Endpoint")]
        public void WhenIPerformAGETToCedromNewsEndpoint()
        {
            var response = _newsViewService.GetCedromLinks();
            PropertyBucket.Remember(GET_CEDROM_LINKS_RESPONSE, response);
        }

        [When(@"I perform a GET for a single CEDROM clip")]
        public void WhenIPerformAGETForASingleCEDROMClip()
        {
            var newsId = _newsViewService.GetCedromClipId(PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Items);
            PropertyBucket.Remember(GET_SINGLE_CEDROM_CLIP_ID, newsId);
        }

        [When(@"I perform a POST to edit clip video")]
        public void WhenIPerformAPOSTToEditClipVideo()
        {
            var response = _newsViewService.CreateCedromClip(PropertyBucket.GetProperty<int>(GET_SINGLE_CEDROM_CLIP_ID));
            PropertyBucket.Remember(POST_CEDROM_CLIP_SEGMENT_RESPONSE, response);
        }

        [When(@"I perform a POST to edit clip with sub seconds")]
        public void WhenIPerformAPOSTToEditClipWithSubSeconds()
        {
            var response = _newsViewService.CreateCedromClipWithSubSeconds(PropertyBucket.GetProperty<int>(GET_SINGLE_CEDROM_CLIP_ID));
            PropertyBucket.Remember(POST_CEDROM_CLIP_SEGMENT_RESPONSE, response);
        }

        [When(@"I perform a GET for CEDROM clip recently edited")]
        public void WhenIPerformAGETForCEDROMClipRecentlyEdited()
        {
            var newsId = PropertyBucket.GetProperty<int>(GET_SINGLE_CEDROM_CLIP_ID);
            var response = _newsViewService.GetCedromClipSegment(newsId);
            PropertyBucket.Remember(GET_CEDROM_CLIP_SEGMENT_RESPONSE, response);
        }

        [When(@"I perform a GET for a single CEDROM clip with video segment")]
        public void WhenIPerformAGETForASingleCEDROMClipWithVideoSegment()
        {
            var newsId = _newsViewService.GetCedromClipIdWithEditedVideo(PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Items);
            PropertyBucket.Remember(GET_SINGLE_CEDROM_CLIP_ID, newsId);
        }

        [When(@"I perform a PUT to update clip video")]
        public void WhenIPerformAPUTToUpdateClipVideo()
        {
            var response = _newsViewService.UpdateCedromClip(PropertyBucket.GetProperty<int>(GET_SINGLE_CEDROM_CLIP_ID));
            PropertyBucket.Remember(PUT_CEDROM_CLIP_SEGMENT_RESPONSE, response);
        }

        [When(@"I perform a GET for CEDROM clip recently edited to get dnews item links")]
        public void WhenIPerformAGETForCEDROMClipRecentlyEditedToGetDnewsItemLinks()
        {
            var newsId = PropertyBucket.GetProperty<int>(GET_SINGLE_CEDROM_CLIP_ID);
            var response = _newsViewService.GetSingleNews(newsId);
            PropertyBucket.Remember(GET_CEDROM_CLIP_NEWS_LINKS_RESPONSE, response);
        }


        [When(@"I perform a GET for all news Clipbooks")]
        public void WhenIPerformAGETForAllNewsClipbooks()
        {
            var response = _newsReportsService.GetAllNewsClipbooks();
            PropertyBucket.Remember(GET_ALL_NEWS_CLIPBOOKS_RESPONSE, response);
        }

        [When(@"I perform a GET for a single news Clipbook with title '(.*)'")]
        public void WhenIPerformAGETForASingleNewsClipbookWithTitle(string title)
        {
            var getAllNewsClipbooksResponse = PropertyBucket.GetProperty<IRestResponse<NewsClipBooks>>(GET_ALL_NEWS_CLIPBOOKS_RESPONSE);
            var singleNewsClipbookResponse = _newsReportsService.GetSingleNewsClipbookByTitle(title, getAllNewsClipbooksResponse.Data.Items);
            PropertyBucket.Remember(GET_SINGLE_NEWS_CLIPBOOK_RESPONSE, singleNewsClipbookResponse);
        }

        [When(@"perform DELETE for created news Clipbook with title '(.*)'")]
        public void WhenPerformDELETEForCreatedNewsClipbookWithTitle(string title)
        {
            var getAllNewsClipbooksResponse = PropertyBucket.GetProperty<IRestResponse<NewsClipBooks>>(GET_ALL_NEWS_CLIPBOOKS_RESPONSE);
            var singleNewsClipbookResponse = _newsReportsService.GetSingleNewsClipbookByTitle(title, getAllNewsClipbooksResponse.Data.Items);
            PropertyBucket.Remember(DELETE_NEWS_CLIPBOOK_RESPONSE, _newsReportsService.DeleteClipbookById(singleNewsClipbookResponse.Data.Id));
        }

        [When(@"I perform a DELETE for recently created news Clipbook")]
        public void WhenIPerformADELETEForRecentlyCreatedNewsClipbook()
        {
            var postResponse = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(POST_NEWS_CLIPBOOK_RESPONSE);
            PropertyBucket.Remember(DELETE_NEWS_CLIPBOOK_RESPONSE, _newsReportsService.DeleteClipbookById(postResponse.Data.Id));
        }

        [When(@"I perform a PUT to update recently created News Clipbook sorted by Oldest to Newest")]
        public void WhenIPerformAPUTToUpdateRecentlyCreatedNewsClipbook()
        {
            var postResponse = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(POST_NEWS_CLIPBOOK_RESPONSE);
            var response = _newsReportsService.EditClipbook(postResponse.Data.Id, PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Items);
            PropertyBucket.Remember(PUT_NEWS_CLIPBOOK_RESPONSE, response);
        }

        [When(@"I perform a GET for news Clipbooks definitions")]
        public void WhenIPerformAGETForNewsClipbooksDefinitions()
        {
            var response = _newsReportsService.GetNewsClipbookDefinitions();
            PropertyBucket.Remember(GET_NEWS_CLIPBOOK_DEFINITIONS, response);
        }

        [When(@"I perform a GET for social locations by name '(.*)'")]
        public void WhenIPerformAGETForSocialLocationsByName(string name)
        {
            var list = new List<string>();
            var places = new GeoService(SessionKey).GetPlaces(name);
            var items = places.Data.Items;
            foreach (var i in items)
            {
                list.Add(i.Id);
            }
            PropertyBucket.Remember(GET_GEO_PLACES_IDS, list);
        }

        [When(@"I perform a GET for news searching by Social Locations")]
        public void WhenIPerformAGETForNewsSearchingBySocialLocations()
        {
            var locations = PropertyBucket.GetProperty<List<string>>(GET_GEO_PLACES_IDS);
            var response = _newsViewService.GetNewsBySocialLocations(locations);
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, response);
        }

        [When(@"I perform a POST to save a News Clipbook sorted by (.*)")]
        public void WhenIPerformAPOSTToSaveANewsClipbookSortedByGivenType(Sorting sortType)
        {
            var items = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Items;
            List<NewsClipbookGroup> groups = new List<NewsClipbookGroup>();
            var response = _newsReportsService.PostSaveNewsClipbook("ClipBook Sorted By Medium", items, "Medium Sorted Summary", "NT-1", sortType, Grouping.Company, groups);
            PropertyBucket.Remember(POST_NEWS_CLIPBOOK_RESPONSE, response);
        }

        [When(@"I perform a POST to save a News Clipbook grouped by (.*)")]
        public void WhenIPerformAPOSTToSaveANewsClipbookGroupedBy(Grouping groupType)
        {
            var items = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Items;
            List<NewsClipbookGroup> groups = new List<NewsClipbookGroup>();
            var response = _newsReportsService.PostSaveNewsClipbook("Grouped Clipbook", items, "Grouped Clipbook", "NT-1", Sorting.NewestToOldest, groupType, groups);
            PropertyBucket.Remember(POST_NEWS_CLIPBOOK_RESPONSE, response);
        }

        [When(@"I perform a GET for a CEDROM clip with specific feed")]
        public void WhenIPerformAGETForACEDROMClipWithSpecificFeed()
        {
            var response = _newsViewService.GetSingleNews(_newsViewService.GetCedromClipFromSpecificFeed(PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.Items));
            PropertyBucket.Remember(GET_SINGLE_CEDROM_NEWS_ITEM, response);
        }

        [When(@"I perform a GET for news searching by Media Type")]
        public void WhenIPerformAGETForNewsSearchingByMediaType()
        {
            PropertyBucket.Remember(GET_NEWS_RESPONSE_KEY, _newsViewService.GetNewsByMediaType());
        }
        #endregion

        #region Then Steps
        /// <summary>
        /// Validates the correct response from api/v1/news endpoint
        /// </summary>
        [Then(@"the News endpoint has the correct response")]
        public void ThenTheNewsEndpointHasTheCorrectResponse()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            NewsView newsView = response.Data;
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(newsView.Key, "Key was null");
                Assert.IsNotNull(newsView.TotalCount, "TotalCount was null");
                Assert.IsNotNull(newsView.ItemCount, "ItemCount was null");
                Assert.IsNotNull(newsView.ActiveCount, "ActiveCount was null");
                Assert.That(newsView.Items.Count, Is.GreaterThan(0), "Expected Item Count to be greater than zero but was not");
            });
        }

        /// <summary>
        /// Validates there are no results from api/v1/news endpoint
        /// </summary>
        [Then(@"the News endpoint has no results")]
        public void ThenTheNewsEndpointHasNoResults()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            NewsView newsView = response.Data;
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(newsView.Key, "Key was null");
                Assert.IsNotNull(newsView.TotalCount, "TotalCount was null");
                Assert.IsNotNull(newsView.ItemCount, "ItemCount was null");
                Assert.IsNotNull(newsView.ActiveCount, "ActiveCount was null");
                Assert.That(newsView.Items.Count, Is.EqualTo(0), "There should be no results.");
            });
        }

        /// <summary>
        /// Validates the correct response from api/v1/news endpoint for a given value
        /// </summary>
        [Then(@"the News endpoint has news with value '(.*)'")]
        public void ThenTheNewsEndpointContainsNewsWith(string value)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            NewsView newsView = response.Data;
            Assert.True(newsView.Items.Any(item => item.Headline.ToLower().Contains(value.ToLower()) ||
                                           item.Text.ToLower().Contains(value.ToLower())),
                $"None of the news contained value '{value}'");
        }

        /// <summary>
        /// Validates the correct response from api/v1/news endpoint for a given values
        /// </summary>
        [Then(@"the News endpoint has news with values '(.*)' and '(.*)'")]
        public void ThenTheNewsEndpointContainsNewsWith(string value1, string value2)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            NewsView newsView = response.Data;
            Assert.True(newsView.Items.Any(item =>
                // headline contains both words
                (item.Headline.ToLower().Contains(value1.ToLower()) && item.Headline.ToLower().Contains(value2.ToLower())) ||
                // or text contains both words
                (item.Text.ToLower().Contains(value1.ToLower()) && item.Text.ToLower().Contains(value2.ToLower()))
                ||
                // or headline and text contain either word
                ((item.Headline.ToLower().Contains(value1.ToLower()) || item.Headline.ToLower().Contains(value2.ToLower())) &&
                (item.Text.ToLower().Contains(value1.ToLower()) || item.Text.ToLower().Contains(value2.ToLower())))),
                $"None of the news contained value '{value1}' and '{value2}'");
        }

        /// <summary>
        /// Validates the Status Code Response from an endpoint
        /// </summary>
        [Then(@"the Faceted News Endpoint should have the correct response")]
        public void ThenTheEndpointResponseShouldBeAsExpected()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            IRestResponse<NewsView> multiFacetedNews = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_FACETED_NEWS_RESPONSE);
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(multiFacetedNews), multiFacetedNews.Content);
            Assert.That(response.Data.ActiveCount, Is.GreaterThan(multiFacetedNews.Data.ActiveCount), "News were not properly faceted");
        }

        [Then(@"all returned results contain '(.*)' in the headline")]
        public void ThenAllReturnedResultsContainInTheHeadline(string value)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            List<NewsItem> items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), $"No results for headline '{value}'");
            Assert.True(items.All(item => item.Headline.ToLower().Contains(value.ToLower())),
                $"A news item headline did not contain '{value}'");
        }

        [Then(@"all News clips contain External ID")]
        public void ThenIShouldSeeThatNewsClipsContainExternalID()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            List<NewsItem> items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), $"Search returned with No Results");
            foreach (var item in items)
            {
                Assert.IsNotNull(item.ExternalId, "External id is null");
            }
        }

        [Then(@"I should see that the News Clip contains Custom Fields")]
        public void ThenIShouldSeeThatTheNewsClipContainsCustomFields()
        {
            IRestResponse<NewsItem> response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(GET_SINGLE_NEWS_ITEM_RESPONSE);
            Assert.IsNotNull(response.Data.CustomFields, "Custom Fields were null");
        }

        [Then(@"I should see that the Clip Report contains Custom Fields")]
        public void ThenIShouldSeeThatTheClipReportContainsCustomFields()
        {
            IRestResponse<ClipReportFields> response = PropertyBucket.GetProperty<IRestResponse<ClipReportFields>>(GET_CLIP_REPORT_FIELDS_KEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "Clip Report Fields are not available");
        }

        [Then(@"I see campaign '(.*)' in news article")]
        public void ThenISeeCampaignInNewsArticle(string expectedCampaignName)
        {
            var id = PropertyBucket.GetProperty<NewsItem>(NewsViewEndPoint).Id;
            var newsItem = _newsViewService.GetNewsItem(id);
            Assert.IsTrue(newsItem.Campaigns.Any(x => x.Name == expectedCampaignName), "Campaign was not assigned to the news item");
        }

        [Then(@"news article campaigns are '(.*)' and '(.*)'")]
        public void ThenNewsArticleCampaignsAre(string expectedCampaignName, string expectedCampaignName2)
        {
            var id = PropertyBucket.GetProperty<NewsItem>(NewsViewEndPoint).Id;
            var newsItem = _newsViewService.GetNewsItem(id);
            Assert.IsTrue(newsItem.Campaigns.Any(x => x.Name == expectedCampaignName), $"Campaign {expectedCampaignName} was assigned to the news item");
            Assert.IsTrue(newsItem.Campaigns.Any(x => x.Name == expectedCampaignName2), $"Campaign {expectedCampaignName2} was assigned to the news item");
            Assert.IsTrue(newsItem.Campaigns.Count == 2, $"Campaigns are only {expectedCampaignName} and {expectedCampaignName2}"); // per definition we can't lack any since the prior asserts ensure it contains both
        }

        [Then(@"I cannot see campaign '(.*)' in news article")]
        public void ThenICannotSeeCampaignInNewsArticle(string campaignName)
        {
            var id = PropertyBucket.GetProperty<NewsItem>(NewsViewEndPoint).Id;
            var newsItem = _newsViewService.GetNewsItem(id);
            var campaign = newsItem.Campaigns.FirstOrDefault(x => x.Name == campaignName);
            Assert.IsNull(campaign, "Campain was assigned to the news item");
        }

        [Then("I cannot see any campaigns in news article")]
        public void ThenICannotSeeAnyCampaignsOnNewsArticle()
        {
            var id = PropertyBucket.GetProperty<NewsItem>(NewsViewEndPoint).Id;
            var newsItem = _newsViewService.GetNewsItem(id);
            Assert.IsEmpty(newsItem.Campaigns, "There are campaigns on news article.");
        }

        [Then(@"the News Custom Field Endpoint response should be '(.*)'")]
        public void ThenTheNewsCustomFieldEndpointShouldBeAGivenResponse(int responseCode)
        {
            IRestResponse<NewsItem> response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_NEWS_CUSTOM_FIELD);
            Assert.AreEqual(responseCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see the CEDROM clip endpoint response is '(.*)'")]
        public void ThenIShouldSeeTheCedromClipEndpointIsAGivenResponse(int responseCode)
        {
            IRestResponse<NewsItemClip> response = PropertyBucket.GetProperty<IRestResponse<NewsItemClip>>(GET_CEDROM_CLIP);
            Assert.AreEqual(responseCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the News Custom Field Bulk Edit Endpoint response should be '(.*)'")]
        public void ThenTheNewsCustomFieldBulkEditEndpointShouldBeAGivenResponse(int responseCode)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(PATCH_BULK_NEWS_CUSTOM_FIELD);
            Assert.AreEqual(responseCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the News endpoint has the correct response for ordered news by custom field")]
        public void ThenTheNewsEndpointHasTheCorrectResponseForOrderedNewsByCustomFields()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_ORDERED_BY_CUSTOM_FIELD);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            NewsView newsView = response.Data;
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(newsView.Key, "Key was null");
                Assert.IsNotNull(newsView.TotalCount, "TotalCount was null");
                Assert.IsNotNull(newsView.ItemCount, "ItemCount was null");
                Assert.IsNotNull(newsView.ActiveCount, "ActiveCount was null");
                Assert.That(newsView.Items.Count, Is.GreaterThan(0), "Expected Item Count to be greater than zero but was not");
            });
        }

        [Then(@"the News endpoint has the correct response for faceted news")]
        public void ThenTheNewsEndpointHasTheCorrectResponseForSingleFacetNews()
        {
            var totalCount = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY).Data.TotalCount;
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_SINGLE_FACET_KEY);
            NewsView newsView = response.Data;
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(newsView.Key, "Key was null");
                Assert.IsNotNull(newsView.TotalCount, "TotalCount was null");
                Assert.IsNotNull(newsView.ItemCount, "ItemCount was null");
                Assert.IsNotNull(newsView.ActiveCount, "ActiveCount was null");
                Assert.That(newsView.TotalCount, Is.LessThanOrEqualTo(totalCount), "Facet was not applied correctly");
            });
        }

        [Then(@"all returned news results have a date greater than or equal to '(.*)'")]
        public void ThenAllReturnedNewsResultsHaveADateGreaterThanOrEqualTo(DateTime date)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var data = response.Data.Items;
            Assert.That(data.Count, Is.GreaterThan(0), "No news items were returned by the search");
            foreach (var d in data)
            {
                Assert.That(d.NewsDate, Is.GreaterThanOrEqualTo(date), $"Not all dates are Greater than {date}");
            }
        }

        [Then(@"all returned news results have a date less than or equal to '(.*)'")]
        public void ThenAllReturnedNewsResultsHaveADateLessThanOrEqualTo(DateTime date)
        {
            date = date.AddDays(1); //This creates a date of Midnight the following day, for our testing anything less than this is acceptable
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var data = response.Data.Items;
            Assert.That(data.Count, Is.GreaterThan(0), "No news items were returned by the search");
            foreach (var d in data)
            {
                Assert.That(d.NewsDate, Is.LessThan(date), $"Not all dates are Less than {date}");
            }
        }

        [Then(@"all returned news results have a news date that is greater than or equal to '(.*)' and less than or equal to '(.*)'")]
        public void ThenAllReturnedNewsResultsHaveANewsDateThatIsGreaterThanOrEqualToAndLessThanOrEqualTo(DateTime start, DateTime end)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var data = response.Data.Items;
            Assert.That(data.Count, Is.GreaterThan(0), "No news items were returned by the search");
            foreach (var d in data)
            {
                Assert.That(d.NewsDate, Is.InRange(start, end), $"Not all dates are within the specified date range: {start} - {end}");
            }
        }

        [Then(@"No news results are returned")]
        public void ThenNoNewsResultsAreReturned()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.EqualTo(0), "News search returned some results");
        }

        [Then(@"the News endpoint has the correct response for News Tags search")]
        public void ThenTheNewsEndpointHasTheCorrectResponseForNewsTagsSearch()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"all the News Clips are tagged with '(.*)'")]
        public void ThenAllTheNewsClipsAreTaggedWithGivenTag(string tagName)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var data = response.Data.Items;
            Assert.That(data.Count, Is.GreaterThan(0), Err.Msg("No news items were returned by the search"));
            foreach (var d in data)
            {
                Assert.True(_newsTagService.GetIsNewsTagged(d, tagName), $"News Item with Id => {d.Id} is not tagged with {tagName}");
            }
        }

        [Then(@"the News endpoint has the correct response for inexistent custom field")]
        public void ThenTheNewsEndpointHasTheCorrectResponseForInexistentCustomField()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_ORDERED_BY_CUSTOM_FIELD);
            Assert.AreEqual(404, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the News Endpoint responds with a '(.*)' for search by Exclude field")]
        public void ThenTheNewsEndpointRespondsWithAGivenCodeForSearchByExcludeField(int statusCode)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_EXCLUDE_FIELD_KEY);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the News Endpoint responds with a '(.*)' for search by Include field")]
        public void ThenTheNewsEndpointRespondsWithAGivenCodeForSearchByIncludeField(int statusCode)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"all returned news results have a non-null Outlet Medium")]
        public void ThenAllReturnedNewsResultsHaveANon_NullOutletMedium()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items.Take(100);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), Err.Msg("No results returned by search"));
            Assert.IsFalse(items.Any(i => string.IsNullOrEmpty(i.Outlet.OutletMedium)), "At least one Outlet Medium is null or empty");
        }

        [Then(@"all returned news results have analytics social data")]
        public void ThenAllReturnedNewsResultsHaveAnalyticsSocialData()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items;
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No results returned by search");

            string errorMsg = "No {0:s} of Social Engagement were returned as result of the search";
            Assert.IsTrue(items.Any(i => i.SocialAnalyticsLikes >= 0), string.Format(errorMsg, "Likes"));
            Assert.IsTrue(items.Any(i => i.SocialAnalyticsShares >= 0), string.Format(errorMsg, "Shares"));
            Assert.IsTrue(items.Any(i => i.SocialAnalyticsComments >= 0), string.Format(errorMsg, "Comments"));
            Assert.IsTrue(items.Any(i => i.SocialAnalyticsFollowers >= 0), string.Format(errorMsg, "Followers"));
        }

        [Then(@"all items should be from the included outlet")]
        public void ThenAllItemsShouldBeFromAGivenOutlet()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), "No items were returned as result of the search");
            Assert.That(_newsViewService.AllItemsFromOutlet(items), Is.True, $"1 or more items with an Outlet Name other than given were returned as result of the search");
        }

        [Then(@"none of the items should be from the excluded outlet")]
        public void ThenNoneOfThenItemsShouldBeFromAGivenOutlet()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_EXCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), "No items were returned as result of the search");
            Assert.That(_newsViewService.AllItemsFromOutlet(items), Is.False, $"1 or more items with given Outlet Name were returned as result of the search");
        }

        [Then(@"news item has news analytics searches:")]
        public void ThenNewsItemHasNewsAnalyticsSearches(Table table)
        {
            var item = _newsViewService.GetNewsItem(PropertyBucket.GetProperty<int>(NEWS_ITEM_ID_KEY));
            var searches = item.Analytics.Searches;

            Assert.That(searches.Count, Is.EqualTo(table.RowCount).And.GreaterThan(0), "Some searches missing");
            table.Rows.ToList().ForEach(r =>
            {
                var expSearch = r["search"];
                var search = searches.FirstOrError(it => it.Name == expSearch, "Search not found: " + expSearch);
                Assert.AreEqual(search.Impact?.ToString(), r["impact"], "Impact not saved");
                Assert.AreEqual(search.Prominence?.ToString(), r["prominence"], "Prominence not saved");

                var tone = r["tone"];
                if (tone == Services.Analytics.Common.ToneId.None.ToString()) tone = null;
                Assert.AreEqual(search.Tone?.Name, tone, "Tone not saved");
            });
        }

        [Then(@"news item has news analytics searches: '(.*)'")]
        public void ThenNewsItemHasNewsAnalyticsSearches(string items)
        {
            var item = _newsViewService.GetNewsItem(PropertyBucket.GetProperty<int>(NEWS_ITEM_ID_KEY));
            var searches = item.Analytics.Searches.Select(_ => _.Name);
            var expected = items.Split(',').Select(_ => _.Trim()).ToList();
            Assert.That(searches, Is.EquivalentTo(expected), "Analytics searches wrong");
        }

        [Then(@"all items should contain '(.*)' in either the Headline or Text")]
        public void ThenAllItemsShouldContainTheGivenKeywords(string keywords)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            Assert.That(_newsViewService.IsKeywordContainedInNewsClipTextOrHeadline(items, keywords), Is.True, $"1 or more clips do not contain {keywords} in either the Headline nor the Text");
        }

        [Then(@"all news should have selected facet option")]
        public void ThenAllNewsShouldHaveSelectedFacetOption()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_SINGLE_FACET_KEY);
            var facetText = PropertyBucket.GetProperty<string>(GET_SINGLE_SMART_TAG_TEXT);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            Assert.That(_newsViewService.AreAllClipsFromGivenFacet(facetText, items), $"All clips not from facet '{facetText}'");
        }

        [Then(@"all items should be from the included custom field")]
        public void ThenAllItemsShouldBeFromTheIncludedCustomField()
        {
            var singleCustomField = PropertyBucket.GetProperty<NewsCustomFields>(GET_SINGLE_CUSTOM_FIELD);
            var allowedValue = _newsViewService.GetCustomFieldFirstAllowedValue(singleCustomField);
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), "No items were returned as result of the search");
            Assert.That(_newsViewService.IsNewsCustomFieldAllowedValueInAllNewsClips(items, allowedValue, singleCustomField), Is.True, $"1 or more items do not have the expected {allowedValue}");
        }

        [Then(@"none of the items should be from the excluded custom field")]
        public void ThenNoneOfTheItemsShouldBeFromTheExcludedCustomField()
        {
            var singleCustomField = PropertyBucket.GetProperty<NewsCustomFields>(GET_SINGLE_CUSTOM_FIELD);
            var allowedValue = _newsViewService.GetCustomFieldFirstAllowedValue(singleCustomField);
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_EXCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            Assert.That(_newsViewService.IsNewsCustomFieldAllowedValueInAllNewsClips(items, allowedValue, singleCustomField), Is.False, $"1 or more items do have the {allowedValue}");
        }

        [Then(@"all items should have the '(.*)' tag")]
        public void ThenAllItemsShouldHaveTheTag(string tagName)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            Assert.That(_newsViewService.IsTagNamePresentInAllNewsClips(items, tagName), Is.True, $"1 or more items are not tagged with {tagName}");
        }

        [Then(@"none of the items should have the '(.*)' tag")]
        public void ThenAllItemsShouldNotHaveTheTag(string tagName)
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_EXCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            Assert.That(_newsViewService.IsTagNamePresentInAllNewsClips(items, tagName), Is.False, $"1 or more items are tagged with {tagName}");
        }

        [Then(@"all items should have the included Analytics Field value")]
        public void ThenAllItemsShouldHaveTheIncludedAnalyticsFieldValue()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            foreach (NewsItem i in items)
            {
                Assert.That(_newsViewService.IsAnalyticsFieldValueInRandomNewsClip(i.Id), Is.True, $"1 or more items are not including the expected Analytics value");
            }
        }

        [Then(@"none of the items should have the excluded Analytics Field value")]
        public void ThenNoneOfTheItemsShouldHaveTheExcludedAnalyticsFieldValue()
        {
            IRestResponse<NewsView> response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_EXCLUDE_FIELD_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            foreach (NewsItem i in items)
            {
                Assert.That(_newsViewService.IsAnalyticsFieldValueInRandomNewsClip(i.Id), Is.False, $"1 or more items have the excluded Analytics value");
            }
        }

        [Then(@"the endpoint should return the single News Item with Type '(.*)'")]
        public void ThenEndpointShouldReturnTheSingleNewsItemWithType(string value)
        {
            var facetsResponse = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var patchResponse = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_SINGLE_NEWS_RESPONSE);
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(patchResponse), Err.Line(patchResponse.Content));
            Assert.AreEqual(value.ToLower(), patchResponse.Data.Type.Name.ToLower(), Err.Line($"The Type Name for News Item with Id {patchResponse.Data.Id} was not updated"));
            Assert.That(patchResponse.Data.Type.Id.ToString().Equals(_newsViewService.GetSingleFacetValueByText(value, facetsResponse.Data.Available)), Is.True, $"The Type Id for News Item with Id {patchResponse.Data.Id} was not updated");
        }

        [Then(@"all items should have the included Smart Tags value")]
        public void ThenAllItemsShouldHaveTheIncludedSmartTagValue()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            var smartTag = PropertyBucket.GetProperty<NewsType>(GET_SINGLE_SMART_TAG_INCLUDE_RESPONSE);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), Err.Msg("No items were returned as result of the search"));
            Assert.That(_newsSmartTagsService.AreAllNewsItemsTaggedWithGivenNewsType(items, smartTag.Name), Is.True, $"1 or more items do not have the {smartTag.Name} Type");
        }

        [Then(@"the News Endpoint responds with a '(.*)' for invalid Outlet Id")]
        public void ThenTheNewsEndpointRespondsWithAForInvalidOutletId(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_BY_INCLUDE_FIELD_KEY);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the News Endpoint response should be '(.*)' for keywords, start date and end date news search")]
        public void ThenTheNewsEndpointResponseShouldBeForKeywordsStartDateAndEndDateNewsSearch(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_KEYWORDS_SDATE_EDATE_RESPONSE_KEY);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"all the news clips are within the expected date range")]
        public void ThenAllTheNewsClipsAreWithinTheExpectedDateRange()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_KEYWORDS_SDATE_EDATE_RESPONSE_KEY);
            var items = response.Data.Items;
            DateTime startDate = Convert.ToDateTime(PropertyBucket.GetProperty<string>("startDate"));
            DateTime endDate = Convert.ToDateTime(PropertyBucket.GetProperty<string>("endDate"));
            Assert.That(items.Count, Is.GreaterThan(0), "No items were returned as result of the search");
            Assert.That(_newsViewService.AreNewsClipsDateContainedInDateRange(items, startDate, endDate), Is.True, $"1 or more clips are not within the expected date range");
        }

        [Then(@"all news results Outlet field '(.*)' should be sorted '(.*)'")]
        public void ThenAllNewsResultsOutletFieldShouldBeSorted(string field, NewsSortDirection direction)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_SORTED);
            var items = response.Data.Items.Select(o => o.Outlet);
            Assert.That(items.Count(), Is.GreaterThan(0), "No results were returned by search");
            var sorted = direction == NewsSortDirection.Ascending ?
                items.OrderBy(s => s.GetType().GetProperty(field).GetValue(s, null)) :
                items.OrderByDescending(s => s.GetType().GetProperty(field).GetValue(s, null));
            Assert.IsTrue(items.SequenceEqual(sorted), "Not all items are sorted");
        }

        [Then(@"all news results field '(.*)' should be sorted '(.*)'")]
        public void ThenAllNewsResultsFieldShouldBeSorted(string field, NewsSortDirection direction)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_SORTED);
            var items = response.Data.Items;
            Assert.That(items.Count(), Is.GreaterThan(0), "No results were returned by search");
            var sorted = direction == NewsSortDirection.Ascending ?
                items.OrderBy(s => s.GetType().GetProperty(field).GetValue(s, null)) :
                items.OrderByDescending(s => s.GetType().GetProperty(field).GetValue(s, null));
            Assert.IsTrue(items.SequenceEqual(sorted), "Not all items are sorted");
        }


        [Then(@"the news endpoint response code should be '(.*)'")]
        public void ThenTheNewsEndpointResponseCodeShouldBe(int code)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_SORTED);
            Assert.AreEqual(code, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the response code for deleting a news item should be'(.*)'")]
        public void ThenTheResponseCodeForDeletingANewsItemShouldBe(int code)
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(GET_DELETE_NEWS_RESPONSE_KEY);
            Assert.AreEqual(code, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the News Endpoint PATCH response should be '(.*)' or successful operation")]
        public void ThenTheNewsEndpointPATCHResponseShouldBeOrSuccessfulOperation(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_SINGLE_NEWS_RESPONSE);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I should see the headline was updated to '(.*)'")]
        public void ThenIShouldSeeTheHeadlineWasUpdatedTo(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_SINGLE_NEWS_RESPONSE);
            Assert.AreEqual(value.ToUpper(), response.Data.Headline.ToUpper(), $"The Headline for News Clip with ID {response.Data.Id} was not updated");
        }

        [Then(@"I should see the text was updated to '(.*)'")]
        public void ThenIShouldSeeTheTextWasUpdatedTo(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_SINGLE_NEWS_RESPONSE);
            Assert.AreEqual(value.ToUpper(), response.Data.Text.ToUpper(), $"The Text for News Clip with ID {response.Data.Id} was not updated");
        }

        [Then(@"all news clips should be toned as '(.*)'")]
        public void ThenAllNewsClispShouldBeTonedAs(string toneValue)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_FACETED_NEWS_RESPONSE);
            Assert.True(response.Data.Items.Count > 0, string.Empty);
            var items = response.Data.Items;
            Assert.That(items.All(i => i.Tone.Name.ToUpper().Equals(toneValue.ToUpper())), Is.True, $"1 or more News Clips were not toned as {toneValue}");
        }

        [Then(@"all returned news items have an Outlet '(.*)' equal to the facet category '(.*)'")]
        public void ThenAllReturnedNewsItemsHaveAnOutletEqualToTheFacetCategory(string field, string category)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var facets = PropertyBucket.GetProperty<List<SingleFacet>>(FACETS);
            Assert.That(response.Data.Items.Count(), Is.GreaterThan(0), "No results were returned via faceting");
            var facetText = facets.FirstOrError(f => f.CategoryText.ToLower().Equals(category.ToLower()), $"Category '{category}' not found.").Text;
            if (field.Equals("Name")) // if name must strip off the city
            {
                var paren = facetText.IndexOf('(');
                if (paren > 0)
                {
                    facetText = facetText.Remove(paren - 1);
                }
            }
            var items = response.Data.Items;
            string text = null;
            Assert.IsTrue(
                items.All(i =>
                {
                    text = i.Outlet.GetType().GetProperty(field).GetValue(i.Outlet, null).ToString();
                    if (text.EndsWith(", The")) // normalize names that end with ", The"
                    {
                        text = "The " + text.Substring(0, text.Length - 5);
                    }
                    return text.Equals(facetText);
                }), $"Not all news items contain the faceted value '{facetText}' for '{category}' value found was '{text}'");
        }

        [Then(@"News item is deleted")]
        public void ThenNewsItemIsDeleted()
        {
            var id = PropertyBucket.GetProperty<int>(NEWS_ITEM_ID_KEY);
            var response = _newsViewService.GetSingleNews(id);
            Assert.That(response.Data._meta.IsDeleted, Is.True, "News item was not deleted");
        }

        [Then(@"all returned news items have an Outlet '(.*)' equal to '(.*)'")]
        public void ThenAllReturnedNewsItemsHaveAnOutletEqualTo(string field, string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var ids = response.Data.Items.Select(n => n.Outlet.Id).Distinct().ToArray();
            var outletResponse = new OutletsService(SessionKey).GetOutlets(ids);
            Assert.IsTrue(outletResponse.Data.Items.All(o => o.GetType().GetProperty(field).GetValue(o).Equals(value)),
                $"Not all returned Outlets have a(n) '{field}' equal to '{value}'");
        }

        [Then(@"All news items have a creation date")]
        public void ThenAllNewsItemsHaveACreationDate()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items;
            Assert.That(items.Any(i => string.IsNullOrWhiteSpace(i.CreationDate)), Is.False, "One or more news items do not have a creation date");
        }

        [Then(@"all news clips Company Tone include the value '(.*)'")]
        public void ThenAllNewsClipsCompanyToneValueIs(int companyToneId)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items;
            foreach (NewsItem i in items)
            {
                Assert.That(_newsViewService.GetSingleNews(i.Id).Data.Analytics.Searches.Where(s => s.Tone != null).ToList().Any(s => s.Tone.Id.Equals(companyToneId)),
                    $"One or more News Clip Searches do not have the expected value {companyToneId} for Company Tone");
            }
        }

        [Then(@"the News Advanced Analytics Bulk Edit Endpoint response should be '(.*)'")]
        public void ThenTheNewsAdvancedAnalyticsBulkEditEndpointResponseShouldBe(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(PATCH_RESPONSE_BULK_EDIT_ADVANCED_ANALYTICS);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the Cedrom News Endpoint response should be '(.*)'")]
        public void ThenTheCedromNewsEndpointResponseShouldBe(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsCedromClipLinks>>(GET_CEDROM_LINKS_RESPONSE);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the value for Caption Url should not be null")]
        public void ThenTheValueForCaptionUrlShouldNotBeNull()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsCedromClipLinks>>(GET_CEDROM_LINKS_RESPONSE);
            Assert.That(response.Data.CaptionUrl, Is.Not.Null, "Caption Url value is Null");
        }

        [Then(@"the CEDROM segment endpoint response should be '(.*)'")]
        public void ThenTheCEDROMSegmentEndpointResponseShouldBe(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<SegmentItem>>(POST_CEDROM_CLIP_SEGMENT_RESPONSE);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the endpoint response should include the segment information")]
        public void ThenTheEndpointResponseShouldIncludeTheSegmentInformation()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<Segments>>(GET_CEDROM_CLIP_SEGMENT_RESPONSE);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(response.Data.Items.First().Id, "Segment Id was null");
                Assert.IsNotNull(response.Data.Items.First().StartSecond, "Segment Start Time was null");
                Assert.IsNotNull(response.Data.Items.First().EndSecond, "Segment End Time was null");
            });
        }

        [Then(@"the CEDROM segment endpoint response for updating a segment should be '(.*)'")]
        public void ThenTheCEDROMSegmentEndpointResponseForUpdatingASegmentShouldBe(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<SegmentItem>>(PUT_CEDROM_CLIP_SEGMENT_RESPONSE);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }


        [Then(@"the endpoint response should include the download link")]
        public void ThenTheEndpointResponseShouldIncludeTheDownloadLink()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(GET_CEDROM_CLIP_NEWS_LINKS_RESPONSE);
            Assert.IsNotNull(response.Data._links.mediaDownload, "Download link wasn't created for Cerdom clip");
        }


        [Then(@"I should see that news endpoint response includes metrics")]
        public void ThenIShouldSeeThatNewsEndpointReponseIncludesMetrics()
        {
            var newsId = Convert.ToInt32(PropertyBucket.GetProperty<string>(NEWS_ITEM_ID_KEY));
            var randomNewsClip = _newsViewService.GetSingleNews(newsId);
            Assert.That(randomNewsClip.Data._meta.KeywordOffsets, Is.Not.Null, "Metrics were not included in the response");
        }

        [Then(@"the News Clipbook endpoint should have the correct response")]
        public void ThenTheNewsClipbookEndpointShouldHaveTheCorrectResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(POST_NEWS_CLIPBOOK_RESPONSE);
            Assert.AreEqual(201, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(response.Data.Id, "News Clipbook Id was null");
                Assert.IsNotNull(response.Data.Title, "News Clipbook Title was null");
                Assert.IsNotNull(response.Data.Owner, "News Clipbook Owner was null");
                Assert.IsNotNull(response.Data.NumberOfClips, "News Clipbook Number of Clips was null");
                Assert.IsNotNull(response.Data.CreatedDate, "News Clipbook Created Date was null");
                Assert.IsNotNull(response.Data.LastEditedDate, "News Clipbook Last Edited Date was null");
                Assert.IsNotNull(response.Data.Summary, "News Clipbook Summary was null");
                Assert.IsNotNull(response.Data.DeliveryOptions, "New Clipbook Delivery Options were null");
            });
        }

        [Then(@"the news Clipbook endpoint should respond with a '(.*)'")]
        public void ThenTheNewsClipbookEndpointShouldRespondWithA(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsClipBooks>>(GET_ALL_NEWS_CLIPBOOKS_RESPONSE);
            Assert.AreEqual(statusCode, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the news Clipbook endpoint should have the correct response for a single news Clipbook")]
        public void ThenTheNewsClipbookEndpointShouldHaveTheCorrectResponseForASingleNewsClipook()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(GET_SINGLE_NEWS_CLIPBOOK_RESPONSE);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(response.Data.Id, "News Clipbook Id was null");
                Assert.IsNotNull(response.Data.Title, "News Clipbbok Name was null");
                Assert.IsNotNull(response.Data.Owner, "News Clipbook Owner was null");
            });
        }

        [Then(@"the news Clipbook endpoint should have the correct response for deleting a single Clipbook")]
        public void ThenTheNewsClipbookEndpointShouldHaveTheCorrectResponseForDeletingASingleClipbook()
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(DELETE_NEWS_CLIPBOOK_RESPONSE);
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"the news Clipbook endpoint should have the correct response for editing a News Clipbook")]
        public void ThenTheNewsClipbookEndpointShouldHaveTheCorrectResponseForEditingANewsClipbook()
        {
            var postResponse = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(POST_NEWS_CLIPBOOK_RESPONSE);
            var putResponse = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(PUT_NEWS_CLIPBOOK_RESPONSE);
            Assert.AreEqual(200, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(putResponse), Err.Line(putResponse.Content));
            Assert.AreNotEqual(putResponse.Data.Title, postResponse.Data.Title, "News Clipbook Name was not updated");
            Assert.AreNotEqual(putResponse.Data.NumberOfClips, postResponse.Data.NumberOfClips, "News Clipbook Number of Clips was not updated");
        }

        [Then(@"I should see max length for Title is '(.*)'")]
        public void ThenIShouldSeeMaxLengthForTitleIs(int maxLength)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsClipBookDefinitions>>(GET_NEWS_CLIPBOOK_DEFINITIONS);
            Assert.That(maxLength.Equals(response.Data.Title.Attributes.MaxLength), Is.True, $"Expected: {maxLength} Response was: {response.Data.Title.Attributes.MaxLength}");
        }

        [Then(@"I should see max length for Summary is '(.*)'")]
        public void ThenIShouldSeeMaxLengthForSummaryIs(int maxLength)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsClipBookDefinitions>>(GET_NEWS_CLIPBOOK_DEFINITIONS);
            Assert.That(maxLength.Equals(response.Data.Summary.Attributes.MaxLength), Is.True, $"Expected: {maxLength} Response was: {response.Data.Summary.Attributes.MaxLength}");
        }

        [Then(@"I should see a facet with name '(.*)'")]
        public void ThenIShouldSeeAFacetWithName(string facetName)
        {
            string category = facetName.Replace(" ", string.Empty).ToLower();
            var response = PropertyBucket.GetProperty<IRestResponse<Facets>>(GET_FACETS_RESPONSE_KEY);
            var facets = response.Data.Available;
            Assert.That(_newsViewService.GetSingleFacetByCategory(category, facets).CategoryText.Equals(facetName, StringComparison.CurrentCultureIgnoreCase), Is.True, $"Facet {facetName} Not Found");
        }

        [Then(@"I should see Social Country is included in the response")]
        public void ThenIShouldSeeSocialCountryIsIncludedInTheResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items;
            foreach (NewsItem i in items)
            {
                Assert.That(i.SocialCountry, Is.Not.Null, $"Social Country was Null for one or more items");
            }
        }

        [Then(@"I should see that news are properly sorted by (.*)")]
        public void ThenIShouldSeeThatNewsAreProperlySorted(Sorting sortField)
        {
            var clipbookId = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(POST_NEWS_CLIPBOOK_RESPONSE);
            var response = _newsReportsService.GetNewsClipbook(clipbookId.Data.Id);
            Assert.True(response.Data.NewsItems.Count > 0, $"No Clips were included in the Clipbook response");
            Assert.True(_newsReportsService.IsClipbookSorted(response.Data.NewsItems, sortField), $"Clipbook items were not sorted by {sortField} as expected");
        }

        [Then(@"I should see the notes was updated to '(.*)'")]
        public void ThenIShouldSeeTheNotesWasUpdatedTo(string notes)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_SINGLE_NEWS_RESPONSE);
            Assert.That(notes.Equals(response.Data.Notes, StringComparison.CurrentCultureIgnoreCase), $"The Notes for News Clip with ID {response.Data.Id} were not updated");
        }

        [Then(@"I should see that news are properly grouped by (.*)")]
        public void ThenIShouldSeeThatNewsAreProperlyGroupedBy(Grouping groupType)
        {
            int groupingValue = (int)groupType;
            var clipbookId = PropertyBucket.GetProperty<IRestResponse<NewsSingleClipBook>>(POST_NEWS_CLIPBOOK_RESPONSE);
            var response = _newsReportsService.GetNewsClipbook(clipbookId.Data.Id);
            Assert.True(response.Data.NewsItems.Count > 0, $"No Clips were included in the Clipbook response");
            Assert.True(groupingValue.Equals(response.Data.GroupType), $"Clipbook items were not grouped by {groupType} as expected");
        }

        [Then(@"I should see an attribute by the name of '(.*)'")]
        public void ThenIShouldSeeAnAttributeByTheNameOf(string name)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsClipBookDefinitions>>(GET_NEWS_CLIPBOOK_DEFINITIONS);
            Assert.That(response.Data.GroupType.Attributes.ValidValues.Any(a => a.Name.Equals(name)), Is.True, $"Expected Name {name} was not found");
        }

        [Then(@"I should see that the FileUrl field is not null")]
        public void ThenIShouldSeeThatTheFileUrlFieldIsNotNull()
        {
            var item = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(GET_SINGLE_CEDROM_NEWS_ITEM);
            Assert.That(item.Data.FileUrl, Is.Not.Null, "FileUrl is Null for this News Item");
        }

        [Then(@"all news have media type '(.*)'")]
        public void ThenAllNewsHaveMediaType(string mediaType)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var items = response.Data.Items;
            foreach (var i in items)
                Assert.That(i.Outlet.MediaType.Equals(mediaType, StringComparison.CurrentCultureIgnoreCase), 
                    $"News Clip with ID => {i.Id}, does not have the expected Media Type");
        }

        [Then(@"I should see the VTKey attribute is present")]
        public void ThenIShouldSeeTheVTKeyAttributeIsPresent()
        {
            var item = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(GET_SINGLE_NEWS_ITEM_RESPONSE);
            Assert.IsNotNull(item.Data.VTKey, $"{item.Data.Id} VTKey attribute was null");
        }

        [Then(@"I should see the article URL was updated to '(.*)'")]
        public void ThenIShouldSeeTheArticleURLWasUpdatedTo(string value)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsItem>>(PATCH_SINGLE_NEWS_RESPONSE);
            Assert.That(value.Equals(response.Data.ArticleUrl, StringComparison.CurrentCultureIgnoreCase), $"The ArticleUrl for News Clip with ID {response.Data.Id} was not updated");
        }

        [Then(@"I should see that the Clip property is null")]
        public void ThenIShouldSeeThatTheClipPropertyIsNull()
        {
            var newsID = PropertyBucket.GetProperty<int>(GET_SINGLE_CEDROM_CLIP_ID);
            var item = _newsViewService.GetSingleNews(Convert.ToInt32(newsID));
            Assert.That(item.Data.Clip, Is.Null, "Clip Property is not Null for CEDROM News Item");
        }
        #endregion
    }
}