using BoDi;
using CCC_API.Data.PostData.Grids;
using CCC_API.Data.Responses.DataView;
using CCC_API.Services.Common.ui;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class DataViewSteps : AuthApiSteps
    {
        public DataViewSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string DELETE_DATAVIEW_RESPONSE_KEY = "DeleteDataViewResponse";
        private const string GET_DATAVIEW_RESPONSE_KEY = "GetDataViewResponse";
        private const string POST_DATAVIEW_RESPONSE_KEY = "PostDataViewResponse";
        private const string PUT_DATAVIEW_RESPONSE_KEY = "PutDataViewResponse";
        private const string DATAVIEW_DATA_FOR_RESTORE_KEY = "DataViewData";


        [When(@"I perform a POST on DataView endpoint using '(.*)' as data view")]
        public void WhenIPerformAPOSTOnDataViewEndpointUsingAsDataView(string dataView)
        {
            var dataViewToCreate = new DataViewPostData(dataView);
            var response = new DataViewService(SessionKey).CreateDataView(dataViewToCreate);
            PropertyBucket.Remember(POST_DATAVIEW_RESPONSE_KEY, response);
        }

        [Then(@"Post DataView endpoint response code should be (.*) and Response '(.*)'")]
        public void ThenPostDataViewEndpointResponseCodeShouldBeAndResponse(int responseCode, string dataViewTypeResult)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<DataViewPostData>>(POST_DATAVIEW_RESPONSE_KEY);
            Assert.NotNull(response, "DataView POST failed", response.Content);
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);            
            Assert.AreEqual(JsonConvert.DeserializeObject<string>(response.Content), dataViewTypeResult, "Result differ from expected, it should be: {dataViewTypeResult}");
        }

        [When(@"I perform a GET on DataView endpoint")]
        public void WhenIPerformAGETOnDataViewEndpoint()
        {
            var response = new DataViewService(SessionKey).GetDataView();
            PropertyBucket.Remember(GET_DATAVIEW_RESPONSE_KEY, response);
        }

        [Then(@"I verify response code should be (.*) and it was properly set to '(.*)'")]
        public void ThenIVerifyResponseCodeShouldBeAndItWasProperlySetTo(int statusCode, string dataViewTypeResult)
        {
            var responseGet = PropertyBucket.GetProperty<IRestResponse<DataViewPostData>>(GET_DATAVIEW_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responseGet.StatusCode, responseGet.Content);
            Assert.AreEqual(JsonConvert.DeserializeObject<string>(responseGet.Content), dataViewTypeResult, "Result differ from expected, it should be: {dataViewTypeResult}");
        }

        [When(@"I perform a DELETE on DataView endpoint")]
        public void WhenIPerformADELETEOnDataViewEndpoint()
        {
            var responseDelete = new DataViewService(SessionKey).DeleteDataView();
            PropertyBucket.Remember(DELETE_DATAVIEW_RESPONSE_KEY, responseDelete);
        }
        [Then(@"Delete DataView endpoint response code should be (.*)")]
        public void ThenDeleteDataViewEndpointResponseCodeShouldBe(int statusCode)
        {
            var responseDelete = PropertyBucket.GetProperty<IRestResponse>(DELETE_DATAVIEW_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responseDelete.StatusCode, responseDelete.Content);            
        }

        [When(@"I run a PUT on DataView endpoint to modify '(.*)' DataView with sort column to '(.*)' and sort direction to '(.*)'")]
        public void WhenIRunAPUTOnDataViewEndpointToModifyDataViewWithSortColumnToAndSortDirectionTo(string gridViewId, string sortColumn, string sortDirection)
        {
            //Steps to set scenario before steps 
            var responseCurrent = new DataViewService(SessionKey).GetDataViewSort(gridViewId);
            var dataViewToRestore = new DataViewResponse(JsonConvert.DeserializeObject<DataViewResponse>(responseCurrent.Content).GridViewId, JsonConvert.DeserializeObject<DataViewResponse>(responseCurrent.Content).Sort);
            PropertyBucket.Remember(DATAVIEW_DATA_FOR_RESTORE_KEY, dataViewToRestore);
            //end

            var sortToModify = new SortResponse(sortColumn, sortDirection);
            var dataViewToModify = new DataViewResponse(gridViewId, sortToModify);
            var response = new DataViewService(SessionKey).UpdateDataViewSort(dataViewToModify);
            PropertyBucket.Remember(PUT_DATAVIEW_RESPONSE_KEY, response);
        }

        [Then(@"PUT DataView endpoint response code should be (.*)")]
        public void ThenPUTDataViewEndpointResponseCodeShouldBe(int statusCode)
        {
            var responsePut= PropertyBucket.GetProperty<IRestResponse>(PUT_DATAVIEW_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responsePut.StatusCode, responsePut.Content);
        }

        [When(@"I perform a GET on DataView endpoint for GridView '(.*)'")]
        public void WhenIPerformAGETOnDataViewEndpointForGridView(string gridViewId)
        {
            var response = new DataViewService(SessionKey).GetDataViewSort(gridViewId);
            PropertyBucket.Remember(GET_DATAVIEW_RESPONSE_KEY, response);
        }

        [Then(@"I verify response code should be (.*) and sort column was properly set to '(.*)' and sort direction to '(.*)'")]
        public void ThenIVerifyResponseCodeShouldBeAndSortColumnWasProperlySetToAndSortDirectionTo(int statusCode, string expectedColumn, string expectedDirection)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<DataViewResponse>>(GET_DATAVIEW_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)response.StatusCode, response.Content);
            Assert.AreEqual(JsonConvert.DeserializeObject<DataViewResponse>(response.Content).Sort.Column, expectedColumn, "Result differ from expected, it should be: {expectedColumn}");
            Assert.AreEqual(JsonConvert.DeserializeObject<DataViewResponse>(response.Content).Sort.Direction, expectedDirection, "Result differ from expected, it should be: {expectedDirection}");

            //step to set scenario after steps
            var gridViewRestored = new DataViewResponse(PropertyBucket.GetProperty<DataViewResponse>(DATAVIEW_DATA_FOR_RESTORE_KEY).GridViewId,
                                   PropertyBucket.GetProperty<DataViewResponse>(DATAVIEW_DATA_FOR_RESTORE_KEY).Sort);
                     
            var responseRestored = new DataViewService(SessionKey).UpdateDataViewSort(gridViewRestored);
            Assert.AreEqual(201, (int)responseRestored.StatusCode, responseRestored.Content);
        }
    }
}
