using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class TVEyesEditedClipsView
    {
        public int ItemCount { get; set; }
        public List<TVEyesEditedClip> Items { get; set; }
        public TVEyesViewLinks _links { get; set; }
        public string _meta { get; set; }
    }

    public class TVEyesViewLinks
    {
        public string tveyesOrderSubmitCallback { get; set;}
        public string tveyesOrderPlacedCallback { get; set;}
        public string tveyesOrderCompleteCallback { get; set;}
    }

    public class TVEyesEditedClip
    {
        public string Name { get; set; }
        public User User { get; set; }
        public string Url { get; set; }
        public string Date { get; set; }
        public bool IsPending { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsPrimary { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Type { get; set; }
        public TVEyesLinks _links { get; set; }
    }

    public class User
    {
        public int Id {get; set;}
        public string Name {get; set;}
    }

    public class TVEyesLinks
    {
        public string download { get; set; }
    }
}
