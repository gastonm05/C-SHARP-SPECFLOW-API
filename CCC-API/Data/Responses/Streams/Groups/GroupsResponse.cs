using System.Collections.Generic;

namespace CCC_API.Data.Responses.Streams.Groups
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

    public class Stream
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Source { get; set; }
        public int Group { get; set; }
        public Data Data { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Stream> Streams { get; set; }
    }

    public class Links
    {
    }

    public class GroupsResponse
    {
        public int ItemCount { get; set; }
        public List<Item> Items { get; set; }
        public Links _links { get; set; }
    }
}