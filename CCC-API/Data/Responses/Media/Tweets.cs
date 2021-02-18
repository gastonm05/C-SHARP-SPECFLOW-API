using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media
{
    public class Tweets
    {
        public int ItemCount { get; set; }
        public List<Tweet> Items { get; set; }
    }
}
