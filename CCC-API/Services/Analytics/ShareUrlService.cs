using System.Dynamic;
using RestSharp;

namespace CCC_API.Services.Analytics
{
    public class ShareUrlService : AuthApiService
    {
        public const string ShareUrlEndPoint = "news/analytics/share/url";

        public ShareUrlService(string sessionKey) : base(sessionKey){}
        
        /// <summary>
        /// Shares the url of analytics view by id.
        /// </summary>
        /// <param name="analyticsViewId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IRestResponse ShareUrl(int analyticsViewId, string password)
        {
            dynamic payload = new ExpandoObject();
            payload.AnalyticsViewId = analyticsViewId;
            payload.password = password;
            return Request().Data(payload).ToEndPoint(ShareUrlEndPoint).Post().Exec();
        }
    }
}
