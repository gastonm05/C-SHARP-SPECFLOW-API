using CCC_API.Data.PostData.Grids;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Common
{

    namespace CCC_API.Services.Common
    {
        public class ExportTemplateService : AuthApiService
        {
            private const string exportTemplateServiceEndpoint = "exporttemplates";
            public ExportTemplateService(string sessionKey) : base(sessionKey) { }

            /// <summary>
            /// Performs a POST to Export Template endpoint
            /// </summary>
            /// <param name="exportTemplatePostData"></param>   
            /// <returns>IRestResponse</returns>
            public IRestResponse CreateExportTemplate(ExportTemplatePostData exportTemplatePostData)
            {
                string resource = string.Format($"{exportTemplateServiceEndpoint}");
                return Post<ExportTemplatePostData>(resource, exportTemplatePostData);

            }

            /// <summary>
            /// Performs a DELETE to Export Template endpoint for specified Export Template Id
            /// </summary>               
            /// <param name="exportTemplateId"></param>        
            /// <returns> IRestResponse<AllCustomFieldsResponse> </returns>
            public IRestResponse<Dictionary<string, string>> deleteExportTemplate(int exportTemplateId)
            {
                string resource = string.Format($"{exportTemplateServiceEndpoint}/{exportTemplateId}");

                return Delete<Dictionary<string, string>>(resource);
            }

            /// <summary>
            /// Performs a GET to Export Template endpoint to get specified Export template id
            ///<param name="exportTemplateId">this can be news/Activity</param>
            /// </summary>               
            /// <returns> IRestResponse<ExportTemplatePostData> </returns>
            public IRestResponse<ExportTemplatePostData> GetExportTemplate(int exportTemplateId)
            {
                return Get<ExportTemplatePostData>($"{exportTemplateServiceEndpoint}/{exportTemplateId}");
            }

            /// <summary>
            /// Performs a PUT to Grid Template endpoint
            /// </summary>
            /// <param name="exportTemplateId"></param>
            /// <param name="beModifiedExportTemplatePostData"></param>   
            /// <returns>IRestResponse</returns>
            public IRestResponse<ExportTemplatePostData> ModifyExportTemplate(int exportTemplateId, ExportTemplatePostData beModifiedExportTemplatePostData)
            {
                string resource = string.Format($"{exportTemplateServiceEndpoint}/{exportTemplateId}");
                return Put<ExportTemplatePostData>(resource, beModifiedExportTemplatePostData);
            }
           }
        }
}

