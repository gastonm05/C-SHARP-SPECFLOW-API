namespace CCC_API.Data.Responses.ACLS
{
    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// Insights Section should include the following objects: 
    /// Access, ImageIQ, Twitter
    /// </summary>
    public class InsightsACLS
    {
        public InsightsAccessPermissions Access { get; set; }
        public InsightsImageIQPermissions ImageIQ { get; set; }
        public InsightsTwitterPermissions Twitter { get; set; }
    }
}
