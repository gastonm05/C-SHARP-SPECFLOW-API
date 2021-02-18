using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.Responses.Streams
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Data
    {
        public string listname { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Source { get; set; }
        public int Group { get; set; }
        public Data Data { get; set; }
    }

    public class StreamResponse
    {
        public Item Item { get; set; }
    }
}