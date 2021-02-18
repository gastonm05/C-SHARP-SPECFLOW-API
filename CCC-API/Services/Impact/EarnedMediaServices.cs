using CCC_API.Data.Responses.Impact.CisionId;
using CCC_API.Data.Responses.Impact.CisionIDEarned;
using CCC_API.Data.Responses.Impact.Earned;
using System;

namespace CCC_API.Services.Impact
{
    public class EarnedMediaServices : ImpactBaseServices
    {
        // C3 Resources
        public static string ImpactSearchUri = "/impact/earnedmedia/searches";
        public const int DEFAULT_DAYS = -90;
        public const string DEFAULT_c3AcctId = "98956";

        // C3 Resources
        public static string ImpactSearchUriCisionId = "/earnedmedia/searches";


        public EarnedMediaServices(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets impact searches on Earned Media page
        /// </summary>
        /// <returns SearchEarned></returns>
        public SearchEarned GetSearches()
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(DEFAULT_DAYS).ToString(DATE_TIME_FORMAT);

            string endpoint = $"{ImpactSearchUri}?endDate={endDateTimeFormatted}&startDate={startDateTimeFormatted}";
            return Request().Get().ToEndPoint(endpoint).ExecContentCheck<SearchEarned>();
        }

        /// <summary>
        /// Gets searches on from CID side
        /// </summary>
        /// <returns SearchCID></returns>
        public SearchCID GetSearchesCID()
        {
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;
            string endpoint = $"{ImpactSearchUriCisionId}?c3AcctId={DEFAULT_c3AcctId}";
            return CisionIdRequest().Get().ToEndPoint(endpoint).AddHeader("Authorization", token).ExecContentCheck<SearchCID>();
        }
    }
}
