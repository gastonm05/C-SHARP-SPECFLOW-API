using CCC_API.Data.Responses.Settings.AlertManagement;
using RestSharp;

namespace CCC_API.Services.Settings.AlertManagement
{
    public class AlertManagementService : AuthApiService
    {
        public AlertManagementService(string sessionKey) : base(sessionKey) { }

        public static string AlertManagementNewsEndPoint = "management/alerts/news";
        public const string AlertManagementAnalyticsUri = "management/alerts/analytics";
        public const string AlertManagementAlertsUri = "management/alerts/";

        /// <summary>
        /// Gets alerts for a user
        /// </summary>
        /// <returns></returns>
        public IRestResponse<ManagementAlerts> GetManagementNewsAlerts()
        {
            return Get<ManagementAlerts>(AlertManagementNewsEndPoint);
        }

        /// <summary>
        /// Get email alerts for a user.
        /// </summary>
        /// <returns></returns>
        public ManagementAnalyticsAlerts GetManagementAnalyticsAlerts()
        {
            return Request().ToEndPoint(AlertManagementAnalyticsUri).ExecCheck<ManagementAnalyticsAlerts>();
        }
        
        /// <summary>
        /// Deletes alert by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteAlert(int id)
        {
            return Request().Delete().ToEndPoint(AlertManagementAlertsUri + id).Exec();
        }
        
        /// <summary>
        /// Updates alert with given payload.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IRestResponse<AnalyticsAlert> UpdateAnalyticsAlert(AnalyticsAlert data)
        {
            return Request().ToEndPoint(AlertManagementAlertsUri).Put().Data(data).Exec<AnalyticsAlert>();
        }
    }
}
