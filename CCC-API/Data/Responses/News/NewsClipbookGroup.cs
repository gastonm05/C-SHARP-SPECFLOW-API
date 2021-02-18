using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsClipbookGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> NewsIds { get; set; }
    }
}
