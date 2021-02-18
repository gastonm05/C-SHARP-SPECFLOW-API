using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class InfluencerRankQueryResponseItemBase
    {
        [JsonProperty("items")]
        public List<Items> Items { get; set; }

        [JsonProperty("queryId")]
        public int QueryId { get; set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; set; }

        [JsonProperty("currentRank")]
        public int? CurrentRank { get; set; }
    }

    public class Items
    {
        public DateTime date { get; set; }
        public int rank { get; set; }
    }
}
