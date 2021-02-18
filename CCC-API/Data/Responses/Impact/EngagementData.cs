using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Impact
{
    public class EngagementData
    {
        public string Name { get; set; }
        public float Value { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }
    }
}
