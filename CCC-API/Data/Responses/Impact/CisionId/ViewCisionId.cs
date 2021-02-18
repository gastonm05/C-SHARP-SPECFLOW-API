
namespace CCC_API.Data.Responses.Impact.CisionId
{
    public class ViewCisionId : CisionIdBaseImpactResponse
    {
        public int TotalViews { get; set; }
        public TimeSeriesData[] TimeSeriesData { get; set; }
    }

    public class TimeSeriesData
    {
        public string ImpressionDate { get; set; }
        public int TotalViews { get; set; }
    }
    
}
