namespace CCC_API.Data.Responses.ACLS
{
    public class NewsAnalyticsPermissions
    {
        public bool HasScoring { get; set; }
        public bool HasToning { get; set; }
        public bool HasToningOverride { get; set; }
        public bool HasFullAdvancedAnalytics { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}