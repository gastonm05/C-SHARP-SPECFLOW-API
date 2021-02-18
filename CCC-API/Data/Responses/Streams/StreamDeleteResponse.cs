using System.Collections.Generic;

namespace CCC_API.Data.Responses.Streams
{
    public class Links
    {
    }

    public class StreamDeleteResponse
    {
        public int ItemCount { get; set; }
        public List<int> Items { get; set; }
        public Links _links { get; set; }
    }
}