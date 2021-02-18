namespace CCC_API.Data.Responses.ACLS
{
    public class InsightsTwitterAccessPermissions
    {
        public bool IsGranted { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
    }

    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// Insights>Twitter Section should include the following object: Access
    /// </summary>
    public class InsightsTwitterPermissions
    {
        public InsightsTwitterAccessPermissions Access { get; set; }
    }
}