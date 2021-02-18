
namespace CCC_API.Data.Responses.ACLS
{
    public class AnalyticsACLS : SettingsPermissionsBase
    {
        public DashboardsPermissions Dashboards { get; set; }
        public bool AnalysisDocumentDownloadFormatDisabled { get; set; }
    }
}
