using System.Collections.Generic;
using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// response from data endpoints such as company prominence and impact
    /// </summary>
    public class WidgetData
    {
        public bool ShowTotalsInLegend { get; set; }
        public List<AnalyticsSeries> Series { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> YAxis { get; set; }
    }
}
