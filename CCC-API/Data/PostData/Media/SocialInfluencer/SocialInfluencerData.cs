using CCC_API.Data.PostData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Media.SocialInfluencer
{
    public class SocialInfluencerData
    {
        public string[] Subjectids { get; set; }
        public List<SocialInfluencerLocation> Places { get; set; }
        public string Key { get; set; }
        public string Facets { get; set; }
        public string Sort { get; set; }
        public int InfluencerScoreStart { get; set; }
        public int InfluencerScoreEnd { get; set; }
        public string Keyword { get; set; }
        public string[] SocialList { get; set; }
        public int[] Sources { get; set; }
    }

    public class SocialInfluencerLocation
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string Locality { get; set; }
        public MediaSocialInfluencerGeoPlaceType PlaceType { get; set; }
    }
    public enum MediaSocialInfluencerGeoPlaceType
    {
        Country = 1,
        Region = 2,
        SubRegion = 3,
        Locality = 4
    }
}
