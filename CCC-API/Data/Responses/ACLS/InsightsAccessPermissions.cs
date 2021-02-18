namespace CCC_API.Data.Responses.ACLS
{
    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// Insights>Access Section should include the following object: IsGranted, Status, StatusCode
    /// </summary>
    public class InsightsAccessPermissions
    {
        public bool IsGranted { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
    }
}