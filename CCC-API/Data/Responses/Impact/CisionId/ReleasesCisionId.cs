using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Impact.CisionId
{
    public class ReleasesCisionId : CisionIdBaseImpactResponse
    {
        public ReleaseOption[] Data { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int ResultsCount { get; set; }

    }

    public class ReleaseOption
    {
        public string OrderPart { set; get; }
        public string Lang { set; get; }
        public string PrnAccount { set; get; }
        public string StoryDate { set; get; }
        public string Headline { set; get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CmsUrl { set; get; }
    }    
}
