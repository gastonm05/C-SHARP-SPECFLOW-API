using CCC_API.Data.Request;
using CCC_API.Data.Responses.Impact;
using CCC_API.Data.Responses.Impact.CisionId;
using CCC_Infrastructure.API.Utils;
using System;
using System.Linq;
using static CCC_API.Services.Impact.ImpactService;

namespace CCC_API.Services.Impact
{
    public class ImpactAnalyticsServices : ImpactBaseServices
    {

        // Cision ID Resources
        public static string AnalyticsEngagementUriCisionId = "accounts/{0}/engagement";
        public static string AnalyticsConversionsUriCisionId = "accounts/{0}/conversions";
        public static string AnalyticsViewUriCisionId = "accounts/{0}/views";
        public static string AnalyticsAudienceUriCisionId = "accounts/{0}/audience_characteristics";

        // C3 Resources
        public static string AnalyticsViewsUri = "/impact/analytics/views";
        public static string AnalyticsEngagementUri = "/impact/analytics/engagement";
        public static string AnalyticsConversionsUri = "/impact/analytics/conversions";
        public static string AnalyticsAudienceUri = "/impact/analytics/audience";

        public ImpactAnalyticsServices(string sessionKey) : base(sessionKey) { }


        /// <summary>
        /// Gets views
        /// </summary>
        /// <param allAcounts=""></param>
        /// <returns Reach[]></returns>
        public Reach[] GetViews(AllAccounts allAccounts)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            Tuple<string, string> dates = GetDatesParameterFromDatebound();
            string startDateTimeFormatted = dates.Item1;
            string endDateTimeFormatted = dates.Item2;
            string endpoint = $"{AnalyticsViewsUri}?allAccounts={includeAccounts}&endDate={endDateTimeFormatted}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<Reach>().ToArray();
        }

        /// <summary>
        /// Gets an engagement response
        /// </summary>
        /// <param allAcounts=""></param>
        /// <returns EngagementData[]></returns>
        public EngagementData[] GetEngagement(AllAccounts allAccounts)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            Tuple<string, string> dates = GetDatesParameterFromDatebound();
            string startDateTimeFormatted = dates.Item1;
            string endDateTimeFormatted = dates.Item2;
            string endpoint = $"{AnalyticsEngagementUri}?allAccounts={includeAccounts}&endDate={endDateTimeFormatted}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<EngagementData>().ToArray();
        }

        /// <summary>
        /// Gets an webEvents response
        /// </summary>
        /// <param allAcounts=""></param>
        /// <returns WebEventsData[]></returns>
        public WebEventsData[] GetWebEvents(AllAccounts allAccounts)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            Tuple<string, string> dates = GetDatesParameterFromDatebound();
            string startDateTimeFormatted = dates.Item1;
            string endDateTimeFormatted = dates.Item2;
            string endpoint = $"{AnalyticsConversionsUri}?allAccounts={includeAccounts}&endDate={endDateTimeFormatted}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<WebEventsData>().ToArray();
        }

        /// <summary>
        /// Gets audience data
        /// </summary>
        /// <param allAcounts=""></param>
        /// <returns Audience[]></returns>q
        public Audience[] GetAudience(AllAccounts allAccounts)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            Tuple<string, string> dates = GetDatesParameterFromDatebound();
            string startDateTimeFormatted = dates.Item1;
            string endDateTimeFormatted = dates.Item2;
            AudienceRequest request = new AudienceRequest()
            {
                AllAccounts = includeAccounts,
                EndDate = endDateTimeFormatted,
                StartDate = startDateTimeFormatted,
            };
            string endpoint = request.ToString();
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<Audience>().ToArray();
        }

        /// <summary>
        /// Gets a views response per a single release
        /// </summary>
        /// <param allAcounts=""></param>
        /// <param endDate=""></param>
        /// <param languageCode=""></param>
        /// <param ReleaseId=""></param>
        /// <param startDate=""></param>
        /// <returns Reach[]></returns>
        public Reach[] GetViews(AllAccounts allAccounts, DateTime endDate, string languageCode, string ReleaseId, string startDate)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            string endDateTimeFormatted = endDate.ToString(DATE_TIME_FORMAT);
            string endpoint = $"{AnalyticsViewsUri}?allAccounts={includeAccounts}&endDate={endDateTimeFormatted}&languageCode={languageCode}&pressReleaseId={ReleaseId}&startDate={startDate}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<Reach>().ToArray();
        }

        /// <summary>
        /// Gets an engagement response per a single release
        /// </summary>
        /// <param allAcounts=""></param>
        /// <param endDate=""></param>
        /// <param languageCode=""></param>
        /// <param ReleaseId=""></param>
        /// <param startDate=""></param>
        /// <returns EngagementData[]></returns>
        public EngagementData[] GetEngagement(AllAccounts allAccounts, DateTime endDate, string languageCode, string ReleaseId, string startDate)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            string endDateTimeFormatted = endDate.ToString(DATE_TIME_FORMAT);
            string endpoint = $"{AnalyticsEngagementUri}?allAccounts={includeAccounts}&endDate={endDateTimeFormatted}&languageCode={languageCode}&pressReleaseId={ReleaseId}&startDate={startDate}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<EngagementData>().ToArray();
        }

        /// <summary>
        /// Gets a webEvents response per a single release
        /// </summary>
        /// <param allAcounts=""></param>
        /// <param endDate=""></param>
        /// <param languageCode=""></param>
        /// <param ReleaseId=""></param>
        /// <param startDate=""></param>
        /// <returns WebEventsData[]></returns>
        public WebEventsData[] GetWebEvents(AllAccounts allAccounts, DateTime endDate, string languageCode, string ReleaseId, string startDate)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            string endDateTimeFormatted = endDate.ToString(DATE_TIME_FORMAT);
            string endpoint = $"{AnalyticsConversionsUri}?allAccounts={includeAccounts}&endDate={endDateTimeFormatted}&languageCode={languageCode}&pressReleaseId={ReleaseId}&startDate={startDate}";
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<WebEventsData>().ToArray();
        }

        /// <summary>
        /// Gets audience per a single release
        /// </summary>
        /// <param allAcounts=""></param>
        /// <param endDate=""></param>
        /// <param languageCode=""></param>
        /// <param ReleaseId=""></param>
        /// <param startDate=""></param>
        /// <returns Audience[]></returns>
        public Audience[] GetAudience(AllAccounts allAccounts, DateTime endDate, string languageCode, string releaseId, string startDate)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            string endDateTimeFormatted = endDate.ToString(DATE_TIME_FORMAT);
            AudienceRequest request = new AudienceRequest()
            {
                AllAccounts = includeAccounts,
                EndDate = endDateTimeFormatted,
                LanguageCode = languageCode,
                ReleaseId = releaseId,
                StartDate = startDate,
            };
            string endpoint = request.ToString();
            return Request().Get().ToEndPoint(endpoint).ExecCheck().ContentAsEnumerable<Audience>().ToArray();
        }

        /// <summary>
        /// Gets a web events CisionId date response
        /// </summary>
        /// <param allAccounts=""></param>
        /// <param timeSeries=""></param>
        /// <returns WebEventsCisionId></returns>
        public WebEventsCisionId GetWebEventsCisionId(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            bool includeTimeSeries = (timeSeries == TimeSeries.including) ? true : false;
            string accounts = AccountParameter(allAccounts);

            Tuple<string, string> datebounds = GetDateboundsParameters();
            string startDateTimeFormatted = datebounds.Item1;
            string toDateTimeFormatted = datebounds.Item2;

            string endpoint = $"{AnalyticsConversionsUriCisionId}?fromStoryDate={startDateTimeFormatted}&toStoryDate={toDateTimeFormatted}&includeTimeSeries={includeTimeSeries}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, accounts)).AddHeader("Authorization", token).ExecContentCheck<WebEventsCisionId>();
        }

        /// <summary>
        /// Gets engagement CisionId data response
        /// </summary>
        /// <param allAccounts=""></param>
        /// <param timeSeries=""></param>
        /// <returns EngagementCisionId></returns>
        public EngagementCisionId GetEngagementCisionId(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            bool includeTimeSeries = (timeSeries == TimeSeries.including) ? true : false;
            string accounts = AccountParameter(allAccounts);

            Tuple<string, string> datebounds = GetDateboundsParameters();
            string startDateTimeFormatted = datebounds.Item1;
            string toDateTimeFormatted = datebounds.Item2;

            string endpoint = $"{AnalyticsEngagementUriCisionId}?fromStoryDate={startDateTimeFormatted}&toStoryDate={toDateTimeFormatted}&includeTimeSeries={includeTimeSeries}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, accounts)).AddHeader("Authorization", token).ExecContentCheck<EngagementCisionId>();
        }

        /// <summary>
        /// Gets views CisionId data response
        /// </summary>
        /// <param allAccounts=""></param>
        /// <param timeSeries=""></param>
        /// <returns ViewCisionId></returns>
        public ViewCisionId GetViewsCisionId(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            bool includeTimeSeries = (timeSeries == TimeSeries.including) ? true : false;
            string accounts = AccountParameter(allAccounts);

            Tuple<string, string> datebounds = GetDateboundsParameters();
            string startDateTimeFormatted = datebounds.Item1;
            string toDateTimeFormatted = datebounds.Item2;

            string endpoint = $"{AnalyticsViewUriCisionId}?fromStoryDate={startDateTimeFormatted}&toStoryDate={toDateTimeFormatted}&includeTimeSeries={includeTimeSeries}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, accounts)).AddHeader("Authorization", token).ExecContentCheck<ViewCisionId>();

        }

        /// <summary>
        /// Gets audience CisionId data response
        /// </summary>
        /// <param allAccounts=""></param>
        /// <param timeSeries=""></param>
        /// <returns AudienceCisionId></returns>
        public AudienceCisionId GetAudienceCisionId(AllAccounts allAccounts, TimeSeries timeSeries)
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            bool includeTimeSeries = (timeSeries == TimeSeries.including) ? true : false;
            string accounts = AccountParameter(allAccounts);

            Tuple<string, string> datebounds = GetDateboundsParameters();
            string startDateTimeFormatted = datebounds.Item1;
            string toDateTimeFormatted = datebounds.Item2;

            string endpoint = $"{AnalyticsAudienceUriCisionId}?fromStoryDate={startDateTimeFormatted}&toStoryDate={toDateTimeFormatted}&includeTimeSeries={includeTimeSeries}";
            return CisionIdRequest().Get().ToEndPoint(String.Format(endpoint, accounts)).AddHeader("Authorization", token).ExecContentCheck<AudienceCisionId>();
        }

    }
}
