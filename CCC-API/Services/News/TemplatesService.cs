using CCC_API.Data.Responses.News;
using RestSharp;

namespace CCC_API.Services.News
{
    class TemplatesService : AuthApiService
    {
        public static string NewsTemplatesEndpoint = "news/templates";

        public TemplatesService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// GET All News Templates
        /// </summary>
        /// <returns></returns>
        public IRestResponse<Templates> GetAllNewsTemplates()
        {
            return Get<Templates>(NewsTemplatesEndpoint);
        }

        /// <summary>
        /// GET a single template by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IRestResponse<SingleTemplate> GetSingletemplate(string Id)
        {
            return Get<SingleTemplate>(NewsTemplatesEndpoint + "/" + Id);
        }
    }
}
