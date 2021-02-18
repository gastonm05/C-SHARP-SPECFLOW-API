
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.SocialInfluencer
{
    public class SocialInfluencerItem
    {
        public long Id { get; set; }
        public Source Source { get; set; }
        public string ScreenName { get; set; }
        public string Name { get; set; }
        public List<Subjects> Subjects { get; set; }
        public List<string> CommunicationRoles { get; set; }
        public CoummunicationTargets CoummunicationTargets { get; set; }
        public Location Location { get; set; }
        public string AccountType { get; set; }
        public TwitterData TwitterData { get; set; }
        public FacebookData FacebookData { get; set; }
        public InstagramData InstagramData { get; set; }        
        public int TractionScore { get; set; }
    }

    public class CoummunicationTargets
    {
        public string BroadCast { get; set; }
        public string Directed { get; set; }
        public Followers Followers { get; set; }
    }

    public class Followers
    {
        public string Name { get; set; }
        public double Score{ get; set; }
    }

    public class TwitterData
    {
        public bool IsVerified { get; set; }
        public int FollowerCount { get; set; }
    }

    public class FacebookData
    {
        public string Id { get; set; }
        public string ScreenName { get; set; }
    }

    public class InstagramData
    {
        public string Id { get; set; }
        public string ScreenName { get; set; }
    }
   


}
