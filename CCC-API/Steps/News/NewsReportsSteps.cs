using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using RestSharp;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using CCC_API.Utils.Assertion;
using Zukini;

namespace CCC_API.Steps.News
{
    [Binding]
    public class NewsReportsSteps : AuthApiSteps
    {
        private NewsReportsService newsReportsService;
        private const string POST_SAVED_SEARCH_RESPONSE_KEY = "PostSavedSearchResponse";
        private const string POST_CREATE_REPORT_RESPONSE_KEY = "PostCreateReportResponse";
        private const string REPORT_FIELD_LIST_KEY = "ReportFieldList";
        private const string GET_NEWS_RESPONSE_KEY = "GetNewsResponse";

        public NewsReportsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            newsReportsService = new NewsReportsService(SessionKey);
        }

        [When(@"I create a clip report")]
        public void ThenICreateAClipReport()
        {
            // get the key the news' search
            var newsResponse = PropertyBucket.GetProperty<IRestResponse<NewsView>>(GET_NEWS_RESPONSE_KEY);
            var keyImported = newsResponse.Data.Key;

            // get the saved search Id
            var savedSearchResponse = PropertyBucket.GetProperty<IRestResponse<SingleSavedSearch>>(POST_SAVED_SEARCH_RESPONSE_KEY);
            int savedSearchId = savedSearchResponse.Data.Item.Id;

            // look for the last piece of news from that search // make an array of 
            int lastPieceOfNewsId = newsReportsService.GetNewsByView(keyImported);

            // create a list of the fields to include on the report 
            var addedFields = PropertyBucket.GetProperty<List<string>>(REPORT_FIELD_LIST_KEY);
            List<int> fields = newsReportsService.GetFieldEnums(addedFields);

            //Create the news clip report
            IRestResponse response = newsReportsService.CreateClipReport(keyImported, lastPieceOfNewsId, fields);
            PropertyBucket.Remember(POST_CREATE_REPORT_RESPONSE_KEY, response);
        }


        [Then(@"Report creation was successful with a '(.*)' response code")]
        public void ThenTheNewsForwardEndpointShouldRespondWithAGivenStatusCodeForCreatingAnItem(int statusCode)
        {
            //Get the clip report creation response
            IRestResponse response = PropertyBucket.GetProperty<IRestResponse>(POST_CREATE_REPORT_RESPONSE_KEY);

            //Verify the status code of the response is the expected
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }
        [When(@"I add '(.*)' field to report request")]
        public void WhenIAddFieldToReportRequest(string field)
        {
            List<string> fields;
            try
            {
                fields = PropertyBucket.GetProperty<List<string>>(REPORT_FIELD_LIST_KEY);
                string s = PropertyBucket.ToString();
            }
            catch (PropertyNotFoundException)
            {
                fields = new List<string>();
            }
            fields.Add(field);
            PropertyBucket.Remember<List<string>>(REPORT_FIELD_LIST_KEY, fields, true);
        }
    }
}





