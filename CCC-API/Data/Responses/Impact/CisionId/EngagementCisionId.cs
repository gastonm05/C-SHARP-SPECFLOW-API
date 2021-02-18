
using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Impact.CisionId
{
    public class EngagementCisionId : CisionIdBaseImpactResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MediaTypeData[] MediaTypeData { get; set; }
    }

    public class MediaTypeData
    {
        public string MediaType { set; get; }
        public int ImpressionCount { set; get; }
    }
}
