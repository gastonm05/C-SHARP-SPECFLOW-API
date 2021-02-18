using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class Segments
    {
        public int ItemCount { get; set; }
        public List<SegmentItem> Items { get; set; }
    }
}
