namespace CCC_API.Data.Responses.ACLS
{
    public class InsightsImageIQAccessPermissions
    {
        public bool IsGranted { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
    }

    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// Insights>ImageIQ Section should include the following object: Access
    /// </summary>
    public class InsightsImageIQPermissions
    {
        public InsightsImageIQAccessPermissions Access { get; set; }
    }
}