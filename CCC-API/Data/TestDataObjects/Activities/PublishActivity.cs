using System.Collections.Generic;

namespace CCC_API.Data.TestDataObjects
{
    public class PublishActivity
    {
        public int EntityId { get; set; }
        public string Type { get; set; }
        public int CompanyId { get; set; }
        public int DataGroupId { get; set; }
        public string Title { get; set; }
        public string ContentSnippet { get; set; }
        public string PublicationTime { get; set; }
        public int PublicationState { get; set; }
        public string Owner { get; set; }
        public List<Campaigns> Campaigns { get; set; }
    }

    public class Campaigns
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
