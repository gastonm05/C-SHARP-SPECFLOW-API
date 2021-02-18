namespace CCC_API.Data.Responses.Company
{
    public class CompanyId
    {
        public string CustomerId { get; set; }
    }

    public class CompanyIdUnauthorized
    {
        public string IncidentId { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorData { get; set; }
    }
}
