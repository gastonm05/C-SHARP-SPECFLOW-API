using System.Collections.Generic;

namespace CCC_API.Data.PostData.Messages
{
    public class MessageData
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public List<SocialPostInfo> SocialPostInfos { get; set; }
        public bool ShortenUrls { get; set; }
        public PublishTime PublishTime { get; set; }
        public SharedLink SharedLink { get; set; }
        public string SharedImageUrl { get; set; }
    }
}
