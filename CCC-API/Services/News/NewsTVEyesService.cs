using CCC_API.Data.Responses.News;
using RestSharp;

namespace CCC_API.Services.News
{
    public class NewsTVEyesService : AuthApiService
    {
        public NewsTVEyesService(string sessionKey) : base(sessionKey) { }

        public static string NewsViewEndPoint = "news";

        /// <summary>
        /// GET call to receive all edited clips for a given News Item
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IRestResponse<TVEyesEditedClipsView> GetTVEyesEditedClips(int newsId) =>
            Get<TVEyesEditedClipsView>($"{NewsViewEndPoint}/{newsId}/clip/tveyes");

    }
}
