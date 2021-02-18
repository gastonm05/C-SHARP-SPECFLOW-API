using CCC_API.Data.Responses.Analytics;
using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Impact
{
    public class SeriesData
    {
        public bool ShowTotalsInLegend { get; set; }
        public AnalyticsSeries[] Series { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}