using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Impact
{
    public class ReleasesImpact
    {
        public Releases[] Releases { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Export Export { get; set; }
    }

    public class Releases
    {
        public string Id { set; get; }
        public string Headline { set; get; }
        public string Date { set; get; }
        public string Language { set; get; }
        public string LanguageCode { set; get; }
        public DataGroups[] DataGroups { set; get; }
        public string Url { set; get; }
    }

    public class DataGroups
    {
        public long Id { set; get; }
        public string Name { set; get; }
    }

    public class Export
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsExportable { set; get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ExportLimit { set; get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ExportOption { set; get; }
    }
}
