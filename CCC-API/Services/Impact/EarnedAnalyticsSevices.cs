using CCC_API.Data.Responses.Impact.CisionId;
using CCC_API.Data.Responses.Impact.CisionIDEarned;
using CCC_API.Data.Responses.Impact.Earned;
using CCC_Infrastructure.API.Utils;
using System;
using System.Linq;

namespace CCC_API.Services.Impact
{
    public class EarnedAnalyticsSevices : ImpactBaseServices
    {

        // C3 Resources
        public static string EarnedAnalyticsViewsUri = "/impact/earnedmedia/analytics/views";
        public static string EarnedAnalyticsWebEventsUri = "/impact/earnedmedia/analytics/conversions";
        public static string EarnedAnalyticsAudienceUri = "/impact/earnedmedia/analytics/audience";
        public static string EarnedTopOutletsUri = "/impact/earnedmedia/analytics/top_outlets";

        // Cision ID Resources
        public static string EarnedAnalyticsViewsCisionId = "/earnedmedia/searches/{0}/views";
        public static string EarnedAnalyticsWebEventsUriCisionId = "/earnedmedia/searches/{0}/conversions";
        public static string EarnedAnalyticsAudienceUriCisionId = "/earnedmedia/searches/{0}/audience_characteristics";
        public static string EarnedTopOutletsUriCisionId = "/earnedmedia/searches/{0}/top_outlets";

        public const int DEFAULT_TOP_OUTLETS = 50;

        public EarnedAnalyticsSevices(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets views on earned media 
        /// </summary>
        /// <returns Views[]></returns>
        public ViewsEarned[] GetViews(int days, int searchId)
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT);

            string endpoint = $"{EarnedAnalyticsViewsUri}?endDate={endDateTimeFormatted}&prnAccountId={null}&searchId={searchId}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<ViewsEarned>().ToArray();
        }

        /// <summary>
        /// Gets web event data on earned media
        /// </summary>
        /// <returns WebEventsEarned[]></returns>
        public WebEventsEarned[] GetWebEvents(int days, int searchId)
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT);

            string endpoint = $"{EarnedAnalyticsWebEventsUri}?endDate={endDateTimeFormatted}&prnAccountId={null}&searchId={searchId}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<WebEventsEarned>().ToArray();
        }

        /// <summary>
        /// Gets audience data on earned media
        /// </summary>
        /// <returns Audience[]></returns>
        public AudienceEarned[] GetAudience(int days, int searchId)
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT);

            string endpoint = $"{EarnedAnalyticsAudienceUri}?endDate={endDateTimeFormatted}&prnAccountId={null}&searchId={searchId}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<AudienceEarned>().ToArray();
        }

        /// <summary>
        /// Gets topOutlets data on earned media
        /// </summary>
        /// <returns TopOutletEarned></returns>
        public TopOutletEarned GetTopOutlets(int days, int searchId)
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT);

            string endpoint = $"{EarnedTopOutletsUri}?endDate={endDateTimeFormatted}&prnAccountId=null&searchId={searchId}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecContentCheck<TopOutletEarned>();
        }

        /// <summary>
        /// Gets topOutlets data from CID side
        /// </summary>
        /// <returns TopOutletEarnedCID></returns>
        public TopOutletsCID GetTopOutletsCID(int days, int searchId)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT_CID);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT_CID);

            string endpoint = $"{EarnedTopOutletsUriCisionId}?fromDate={startDateTimeFormatted}&toDate={endDateTimeFormatted}&top={DEFAULT_TOP_OUTLETS}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, searchId)).AddHeader("Authorization", token).ExecContentCheck<TopOutletsCID>();
        }


        /// <summary>
        /// Gets WebEvents data from CID side
        /// </summary>
        /// <returns TopOutletEarnedCID></returns>
        public WebEventsCisionId GetWebEventCID(int days, int searchId)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT_CID);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT_CID);

            string endpoint = $"{EarnedAnalyticsWebEventsUriCisionId}?fromDate={startDateTimeFormatted}&toDate={endDateTimeFormatted}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, searchId)).AddHeader("Authorization", token).ExecContentCheck<WebEventsCisionId>();
        }

        /// <summary>
        /// Gets views data from CID side
        /// </summary>
        /// <returns ViewEarnedCID></returns>
        public ViewCisionId GetViewCID(int days, int searchId)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT_CID);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT_CID);

            string endpoint = $"{EarnedAnalyticsViewsCisionId}?fromDate={startDateTimeFormatted}&toDate={endDateTimeFormatted}&includeTimeSeries=true";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, searchId)).AddHeader("Authorization", token).ExecContentCheck<ViewCisionId>();
        }

        /// <summary>
        /// Gets audience data from CID side
        /// </summary>
        /// <returns AudienceEarnedCID></returns>
        public AudienceCisionId GetAudienceCID(int days, int searchId)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT_CID);
            var startDateTimeFormatted = DateTime.Today.AddDays(-days).ToString(DATE_TIME_FORMAT_CID);

            string endpoint = $"{EarnedAnalyticsAudienceUriCisionId}?fromDate={startDateTimeFormatted}&toDate={endDateTimeFormatted}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, searchId)).AddHeader("Authorization", token).ExecContentCheck<AudienceCisionId>();
        }

    }
}
