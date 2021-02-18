using RestSharp;

namespace CCC_API.Services.Media.Updates
{
    public class MediaUpdatesService : AuthApiService
    {
        public MediaUpdatesService(string sessionKey) : base(sessionKey) { }

        private static string UpdatesEndpoint = "media/updates";

        /// <summary>
        /// Gets media updates.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<Data.Responses.Media.Updates> GetMediaUpdates()
        {
            return Request().Get().ToEndPoint(UpdatesEndpoint).Exec<Data.Responses.Media.Updates>();
        }
    }
}
