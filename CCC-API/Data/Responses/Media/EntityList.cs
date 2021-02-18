namespace CCC_API.Data.Responses.Media
{
    public class EntityList
    {
        public string EntityType { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string LastAccessed { get; set; }
        public string LastModifiedDate { get; set; }
        public string CreationDate { get; set; }
        public int Count { get; set; }
        public Membership Membership { get; set; }
        public Supplement Supplement { get; set; }
    }
}
