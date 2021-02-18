using CCC_API.Data.Responses.Impact;
using CCC_API.Data.Responses.Impact.CisionId;

namespace CCC_API.Services.Impact
{
    public class ImpactService : ImpactBaseServices
    {
        // Cision ID Resources
        public static string ImpactReleasesCisionId = "releases";

        // C3 Resources
        public static string ImpactReleasesUri = "impact/releases";
        public static string ImpactDateboundsUri = "impact/datebounds";

        public enum SortDirection { Ascending, Descending, asc, desc };
        public enum SortField { headline, date, orderPart };
        public enum AllAccounts { including, ignoring };
        public enum TimeSeries { including, ignoring };

        public ImpactService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets releases
        /// </summary>
        /// <param limit=""></param>
        /// <param page=""></param>
        /// <param sortDirection=""></param>
        /// <param sortFile=""></param>
        /// <param allAccounts=""></param>
        /// <return ReleasesImpact=""></param>
        public ReleasesImpact GetReleases(int limit, int page, SortDirection sortDirection, SortField sortField, AllAccounts allAccounts)
        {
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            string endpoint = $"{ImpactReleasesUri}?allAccounts={includeAccounts}&limit={limit}&page={page}&sortDirection={sortDirection}&sortField={sortField}";
            return Request().Get().ToEndPoint(endpoint).ExecContentCheck<ReleasesImpact>();
        }

        /// <summary>
        /// Gets releases from Cision ID impact team 
        /// </summary>
        /// <param limit=""></param>
        /// <param page=""></param>
        /// <param sortDirection=""></param>
        /// <param sortField=""></param>
        /// <param allAccounts=""></param>
        /// <return ReleasesCisionId=""></param>
        public ReleasesCisionId GetReleasesCisionId(int limit, int page, SortDirection sorting, SortField field, AllAccounts allAccounts)
        {
            //Getting token for Cision ID
            LoginCisionId responseLogin = GetLogin(EMAIL_LOGIN, PASSWORD_LOGIN);
            string token = "Bearer " + responseLogin.Token;

            //Setting default account id or multiple account ids
            string accounts = AccountParameter(allAccounts);

            //Setting dates from datebound endpoint
            Datebounds dateBound = GetDatebound();
            string startDateTimeFormatted = dateBound.MinDate;
            string toDateTimeFormatted = dateBound.MaxDate;

            string sort_Direction = (sorting == SortDirection.Ascending) ? "asc" : "desc";
            string sort_Field = (field == SortField.headline) ? "headline" : "storyDate";

            //Calling the controller
            string endpoint = $"{ImpactReleasesCisionId}?page={page}&rpp={limit}&sort={sort_Field}&sort_order={sort_Direction}&prnAccount={accounts}&fromStoryDate={startDateTimeFormatted}&toStoryDate={toDateTimeFormatted}";
            return CisionIdRequest().Get().ToEndPoint(endpoint).AddHeader("Authorization", token).ExecContentCheck<ReleasesCisionId>();
        }
    }
}
