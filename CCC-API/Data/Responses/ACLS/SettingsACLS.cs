

namespace CCC_API.Data.Responses.ACLS
{
    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// Settings Section should include the following objects: 
    /// Access, AllUsers, MyUser, NewsAlertManagement, MediaMonitoringSearchesManagement, AnalyticsProfileManagement, SocialMediaManagement,
    /// Labs, MyAuthorizedSender, AllAuthorizedSender, Support
    /// </summary>
    public class SettingsACLS :SettingsPermissionsBase
    {        
        public SettingsAllUsersPermissions AllUsers { get; set; }
        public SettingsMyUserPermissions MyUser { get; set; }
        public SettingsNewsAlertManagementPermissions NewsAlertManagement { get; set; }
        public SettingsMediaMonitoringSearchesManagementPermissions MediaMonitoringSearchesManagement { get; set; }
        public SettingsAnalyticsProfileManagementPermissions AnalyticsProfileManagement { get; set; }
        public SettingsPermissionsBase OMCAccountId { get; set; }
        public SettingsPermissionsBase SocialMediaManagement { get; set; }
        public SettingsLabPermissions Labs { get; set; }
        public SettingsMyAuthorizedSenderPermissions MyAuthorizedSender { get; set; }
        public SettingsAllAuthorizedSenderPermissions AllAuthorizedSender { get; set; }
        public SettingsPermissionsBase SmartTags { get; set; }
        public SettingsPermissionsBase HasAdvancedSecurity { get; set; }
        public SettingsPermissionsBase PressReleaseImpact { get; set; }
        public SettingsPermissionsBase FtpExport { get; set; }
        public SettingsPermissionsBase EarnedMediaImpact { get; set; }
    }
}
