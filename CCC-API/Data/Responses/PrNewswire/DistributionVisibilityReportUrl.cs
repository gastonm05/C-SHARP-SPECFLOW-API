namespace CCC_API.Data.Responses.PrNewswire
{
    /// <summary>
    /// response from endpoint api/v1/prnewswire/distribution/VisibilityReportsUrl: 200 - OK
    /// It should return string values for OMCUrl 
    /// </summary>
    public class DistributionVisibilityReportUrl
    {
        public string OMCUrl { get; set; }
    }
}
