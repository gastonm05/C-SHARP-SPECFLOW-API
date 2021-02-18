using CCC_API.Data.Responses.Common;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class InfluencerRankingsResponse
    {
        public CollectionResponse<InfluencerRankQueryResponseItemBase> System { get; set; }
    }
}