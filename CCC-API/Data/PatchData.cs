using Newtonsoft.Json;

namespace CCC_API.Data
{
    /// <summary>
    /// Patch apis have a request body that is an array of PatchData objects
    /// </summary>
    public class PatchData
    {
        public string Op { get; set; }
        public string Path { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        public PatchData(string op, string path, string value)
        {
            Op = op;
            Path = path;
            Value = value;
        }

        public PatchData(){}
    }
}
