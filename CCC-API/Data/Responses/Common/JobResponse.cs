namespace CCC_API.Data.Responses.Common
{
    /// <summary>
    /// Common response for assigned async task. Like generate report.
    /// </summary>
    public class JobResponse
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public Links _links { get; set; }
        public int DailyExportLimit { get; set; }
    }

    public class Links
    {
        public string file { get; set; }
        public string self { get; set; }
    }
}
