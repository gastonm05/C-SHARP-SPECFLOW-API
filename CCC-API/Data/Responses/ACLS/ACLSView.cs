namespace CCC_API.Data.Responses.ACLS
{
    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// Response should include the following objects: Distribution, News, MediaContact
    /// Streams, Settings, Insights, MediaOutlet, Haro, SocialMedia, PublishActivity, Analytics,
    /// Lists , CustomField, Insights
    /// </summary>
    public class ACLSView
    {
        public CustomFieldACLS CustomField { get; set; }
        public DistributionACLS Distribution { get; set; }
        public NewsACLS News { get; set; }
        public SettingsACLS Settings { get; set; }
        public SupportACLS Support { get; set; }
        public AnalyticsACLS Analytics { get; set; }
        public ImpactACLS Impact { get; set; }
        public InsightsACLS Insights { get; set; }
        //TODO: Create/Add missing objects 
    }
}
