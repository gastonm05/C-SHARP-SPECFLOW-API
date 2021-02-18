using System.Collections.Generic;


namespace CCC_API.Data.Responses.Media.SocialInfluencer
{
    public class SocialInfluencerListOfList
    {
        public List<SocialInfluencerListItem> Results { get; set; }
        public int ListLimit { get; set; }
        public int TotalCount { get; set; }
    }



    public class SocialInfluencerListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
