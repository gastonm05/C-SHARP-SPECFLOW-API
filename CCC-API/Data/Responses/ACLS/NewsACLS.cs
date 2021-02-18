namespace CCC_API.Data.Responses.ACLS
{
    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// News Section should include bool values for HasAccess 
    /// News Section should include the following objects: 
    /// Archive, Items, Searches, Templates, Tags, Analytics, ForwardWizard
    /// </summary>
    public class NewsACLS
    {
        public bool HasAccess { get; set; }
        public NewsArchivePermissions Archive { get; set; }
        public NewsItemsPermissions Items { get; set; }
        public NewsSearchesPermissions Searches { get; set; }
        public NewsTemplatesPermissions Templates { get; set; }
        public NewsTagsPermissions Tags { get; set; }
        public NewsAnalyticsPermissions Analytics { get; set; }
        public NewsForwardWizardPermissions ForwardWizard { get; set; }
    }
}
