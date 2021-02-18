using CCC_API.Data.Responses.News;
using RestSharp;

namespace CCC_API.Services.News
{
    public class NewsAlertService : AuthApiService
    {

        public const string NewsSearch = "news/searches/";

        public NewsAlertService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets all news alerts for a user.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<NewsAlerts> GetAllNewsAlerts() => 
            Request().Get().ToEndPoint($"{NewsSearch}alerts").Exec<NewsAlerts>();

        /// <summary>
        /// Gets a single news alert.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRestResponse<NewsAlerts> GetSingleNewsAlert(int id) =>
            Request().Get().ToEndPoint($"{NewsSearch}{id}/alerts").Exec<NewsAlerts>();
    }
}