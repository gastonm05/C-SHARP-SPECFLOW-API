using CCC_API.Data.PostData.Grids;
using CCC_API.Data.Responses.DataView;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Common.ui
{
    public class DataViewService : AuthApiService    
    {
        private const string uiDataViewServiceEndpoint = "ui/dataview";
        public enum ExportType { Contact, Outlet, News }
        public DataViewService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a POST to DataView endpoint
        /// </summary>
        /// <param name="DataViewPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse CreateDataView(DataViewPostData dataViewPostData)
        {
            string resource = string.Format($"{uiDataViewServiceEndpoint}");
            return Post<DataViewPostData>(resource, dataViewPostData);

        }

        /// <summary>
        /// Performs a DELETE to DataView endpoint
        /// </summary>                       
        /// <returns> IRestResponse<string, string> </returns>
        public IRestResponse<Dictionary<string, string>> DeleteDataView()
        {
            string resource = string.Format($"{uiDataViewServiceEndpoint}");

            return Delete<Dictionary<string, string>>(resource);
        }

        /// <summary>
        /// Performs a GET to DataView endpoint to get current DataView        
        /// </summary>               
        /// <returns> IRestResponse<DataViewPostData> </returns>
        public IRestResponse<DataViewPostData> GetDataView()
        {
            return Get<DataViewPostData>($"{uiDataViewServiceEndpoint}");
        }

        /// <summary>
        /// Performs a GET to DataView endpoint to get provided gridDataView sort order       
        /// </summary>            
        /// <param name="gridViewid"></param>   
        /// <returns> IRestResponse<DataViewResponse> </returns>
        public IRestResponse<DataViewResponse> GetDataViewSort(string gridViewid)
        {
            return Get<DataViewResponse>($"{uiDataViewServiceEndpoint}/{gridViewid}");
        }

        /// <summary>
        /// Performs a PUT to DataView endpoint to set provided sort order to provided gridViewid       
        /// </summary>            
        /// <param name="gridView"></param>   
        /// <returns> IRestResponse<DataViewResponse> </returns>
        public IRestResponse<DataViewResponse> UpdateDataViewSort(DataViewResponse gridView)
        {
            string resource = string.Format($"{uiDataViewServiceEndpoint}/{gridView.GridViewId}");
            return Put<DataViewResponse>(resource, gridView);
        }
    }
}
