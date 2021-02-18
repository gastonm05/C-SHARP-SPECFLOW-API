using System.Collections.Generic;

namespace CCC_API.Data.Responses.Campaigns
{
    public class CampaignsList
    {
        public int ItemCount { get; set; }
        public List<Campaign> Items { get; set; }
        public Links _links { get; set; }
    }
}
