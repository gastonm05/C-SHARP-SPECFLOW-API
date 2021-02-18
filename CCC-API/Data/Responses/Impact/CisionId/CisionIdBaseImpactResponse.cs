using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Impact.CisionId
{
    public class CisionIdBaseImpactResponse
    {
        public int Status { get; set; }
        public Parameters Parameters { get; set; }
    }

    public class Parameters
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IncludeTimeSeries { set; get; }

        public string FromStoryDate { set; get; }
        public string PrnAccount { set; get; }
        public string ToStoryDate { set; get; }
    }

}
