    using System.Collections.Generic;

namespace CCC_API.Data.PostData.News
{
    public class ClipReport
    {
        public string key { get; set; }
        public List<int> delta { get; set; }
        public bool selectAll { get; set; }
        public string format { get; set; }
        public List<int> fields { get; set; }
    }
}
