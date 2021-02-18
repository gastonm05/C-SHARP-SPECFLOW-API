using BoDi;
using CCC_API.Data.PostData.Grids;
using CCC_API.Data.Responses.Grid;
using CCC_API.Services.Common.ui;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class GridSteps : AuthApiSteps
    {
        private const string GET_GRID_RESPONSE_KEY = "GetGridTemplateResponse";
        private const string POST_GRID_TEMPLATE_CREATE_RESPONSE_KEY = "CreateGridTemplateResponse";
        private const string PUT_GRID_TEMPLATE_MODIFY_RESPONSE_KEY = "ModifyGridTemplateResponse";
        private const string PUT_GRID_TEMPLATE_DATA_RESTORED_KEY = "GridTemplateDataRestored";
        private const string PUT_GRID_DATA_KEY = "GridTemplateData";        
        
        public GridSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        public enum PostOrPut { POST, PUT }


        [Then(@"UI/Grid endpoint '(.*)' response code should be (.*)")]
        public void ThenUIGridEndpointResponseCodeShouldBe(PostOrPut postOrPut, int responseCode)
        {

            IRestResponse<GridPostData> response = null;
            if (postOrPut.Equals(PostOrPut.POST))
            {
                response = PropertyBucket.GetProperty<IRestResponse<GridPostData>>(POST_GRID_TEMPLATE_CREATE_RESPONSE_KEY);
            }
            if (postOrPut.Equals(PostOrPut.PUT))
            {
                response = PropertyBucket.GetProperty<IRestResponse<GridPostData>>(PUT_GRID_TEMPLATE_MODIFY_RESPONSE_KEY);
            }
            Assert.NotNull(response, "Response was null");
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
        }

        [When(@"I perform a GET on grid endpoint the GridTemplateId '(.*)'")]
        public void WhenIPerformAGETOnGridEndpointTheGridTemplateId(string gridTemplateId)
        {
            var response = new GridService(SessionKey).GetGridTemplate(gridTemplateId);
            PropertyBucket.Remember(GET_GRID_RESPONSE_KEY, response);
        }

        [Then(@"Get grid endpoint response code should be (.*)")]
        public void ThenGetGridEndpointResponseCodeShouldBe(int responseCode)
        {
            IRestResponse<GridPostData> responseGet = PropertyBucket.GetProperty<IRestResponse<GridPostData>>(GET_GRID_RESPONSE_KEY);
            Assert.AreEqual(responseCode, (int)responseGet.StatusCode, responseGet.Content);
        }


        [When(@"I perform a PUT on Grid Template endpoint to modify template id '(.*)' with test data\.")]
        public void WhenIPerformAPUTOnGridTemplateEndpointToModifyTemplateIdWithTestData_(string gridTemplateId)
        {
            IRestResponse<GridPostData> responseGet = PropertyBucket.GetProperty<IRestResponse<GridPostData>>(GET_GRID_RESPONSE_KEY);
            GridPostData gridTemplateGet = new GridPostData(gridTemplateId,  JsonConvert.DeserializeObject<GridPostData>(responseGet.Content).columns);
            var numColumns = gridTemplateGet.columns.Count();           
            PropertyBucket.Remember(PUT_GRID_DATA_KEY, gridTemplateGet);
      
            Column column = new Column();

            column.Name = $"Automation Testing View {Guid.NewGuid()}";
            column.Order = numColumns;
            column.Sort = null;
            column.Visibility = true;
            
            Column[] columnsModified = GridService.AddColumn(gridTemplateGet.columns, column);

            
            GridPostData gridTemplateModified = new GridPostData(gridTemplateId, columnsModified);
            var putResponse = new GridService(SessionKey).ModifyGridTemplate(gridTemplateId, gridTemplateModified);
            PropertyBucket.Remember(PUT_GRID_TEMPLATE_MODIFY_RESPONSE_KEY, putResponse);
            GridPostData gridTemplatePut = new GridPostData(gridTemplateId, JsonConvert.DeserializeObject<GridPostData>(putResponse.Content).columns);
            PropertyBucket.Remember(PUT_GRID_TEMPLATE_DATA_RESTORED_KEY, gridTemplatePut);            
        }

        [Then(@"I verify all Grid Template data was changed")]
        public void ThenIVerifyAllGridTemplateDataWasChanged()
        {

            GridPostData dataGrid = new GridPostData(PropertyBucket.GetProperty<GridPostData>(PUT_GRID_DATA_KEY).gridTemplateId, PropertyBucket.GetProperty<GridPostData>(PUT_GRID_DATA_KEY).columns);
            GridPostData putGrid = new GridPostData(PropertyBucket.GetProperty<GridPostData>(PUT_GRID_TEMPLATE_DATA_RESTORED_KEY).gridTemplateId, PropertyBucket.GetProperty<GridPostData>(PUT_GRID_TEMPLATE_DATA_RESTORED_KEY).columns);
                                    
            Assert.IsTrue(putGrid.gridTemplateId != null, "The gridview id shouldn't be null.");
            Assert.AreEqual(dataGrid.gridTemplateId, putGrid.gridTemplateId);
            bool equalColumnResultModified = putGrid.columns.SequenceEqual(dataGrid.columns, new ColumnComparer());
            Assert.IsFalse(equalColumnResultModified, "Equal Column Result is modified");

            //step to set scenario after steps
            var putResponse = new GridService(SessionKey).ModifyGridTemplate(dataGrid.gridTemplateId, dataGrid);
            Column[] restoredColumns = JsonConvert.DeserializeObject<GridPostData>(putResponse.Content).columns;
            Column[] dataGridColumn = dataGrid.columns;
            bool equalColumnResultRestored = restoredColumns.SequenceEqual(dataGridColumn, new ColumnComparer());
                      
            Assert.IsTrue(equalColumnResultRestored, "Equal Column Result Restored is false");

        }        
    }
}
