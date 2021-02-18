using CCC_API.Data.PostData.Grids;
using CCC_API.Data.Responses.Grid;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Common.ui
{
    public class GridService : AuthApiService
    {
        private const string uiGridServiceEndpoint = "ui/grid";
        public GridService(string sessionKey) : base(sessionKey) { }
        
        /// <summary>
        /// Performs a POST to Grid Template endpoint
        /// </summary>
        /// <param name="GridPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse CreateGridTemplate(GridPostData gridPostData)
        {
            string resource = string.Format($"{uiGridServiceEndpoint}/{gridPostData.gridTemplateId}");
            return Post<GridPostData>(resource, gridPostData);
            
        }

        /// <summary>
        /// Performs a DELETE to Grid Template endpoint for specified Grid Template Id
        /// </summary>               
        /// <param name="gridTemplateId"></param>        
        /// <returns> IRestResponse<AllCustomFieldsResponse> </returns>
        public IRestResponse<Dictionary<string, string>> deleteGridTemplate(string gridTemplateId)
        {
            string resource = string.Format($"{uiGridServiceEndpoint}/{gridTemplateId}");

            return Delete<Dictionary<string, string>>(resource);
        }

        /// <summary>
        /// Performs a GET to Grid Template endpoint to get all current available grid templates
        ///<param name="type">this can be news/Activity</param>
        /// </summary>               
        /// <returns> IRestResponse<AllCustomFieldsResponse> </returns>
        public IRestResponse<GridPostData> GetGridTemplate(string gridTemplateId)
        {
            return Get<GridPostData>($"{uiGridServiceEndpoint}/{gridTemplateId}");
        }

        /// <summary>
        /// Performs a PUT to Grid Template endpoint
        /// </summary>
        /// <param name="gridTemplateId"></param>
        /// <param name="beModifiedGridPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse<GridPostData> ModifyGridTemplate(string gridTemplateId, GridPostData beModifiedGridPostData)
        {
            string resource = string.Format($"{uiGridServiceEndpoint}/{gridTemplateId}");
            return Put<GridPostData>(resource, beModifiedGridPostData);
        }

        /// <summary>
        /// Add a new Column to sent Columns
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="column"></param>   
        /// <returns>IRestResponse</returns>
        public static Column[] AddColumn(Column[] columns, Column column)
        {

            List<Column> listColumns = new List<Column> { };
            foreach (var col in columns)
               listColumns.Add(col);
            listColumns.Add(column);


            return listColumns.ToArray();
        }
    }
}
