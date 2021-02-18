using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Streams
{
    public class StreamData
    {
        public string listname { get; set; }
    }

    public class StreamPostData
    {
        public int Type { get; set; }
        public int Group { get; set; }
        public string Name { get; set; }
        public StreamData Data { get; set; }
    }
}
