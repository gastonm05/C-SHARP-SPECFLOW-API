namespace CCC_API.Data.Responses.Media.Contact
{
    public class ContactExportResponse
    {
        public string IncidentId { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorData { get; set; }
        public ModelState ModelState { get; set; }
    }

    public class ModelState
    {
        public string[] ExportCriteria { get; set; }
    }
}
