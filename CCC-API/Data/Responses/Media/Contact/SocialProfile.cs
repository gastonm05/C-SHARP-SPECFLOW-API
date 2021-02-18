using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class SocialProfile
    {
        public List<SocialItem> Twitter { get; set; }
        public List<SocialItem> LinkedIn { get; set; }
        public List<SocialItem> Facebook { get; set; }
    }
}
