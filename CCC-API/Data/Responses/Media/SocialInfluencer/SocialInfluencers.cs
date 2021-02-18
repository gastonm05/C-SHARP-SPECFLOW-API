using CCC_API.Data.Responses.News;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.SocialInfluencer
{
    public class SocialInfluencers
    {
        public int ItemCount { get; set; }
        public List<SocialInfluencerItem> Items { get; set; }
        public string Key { get; set; }
        public Export Export { get; set; }
    }
}
