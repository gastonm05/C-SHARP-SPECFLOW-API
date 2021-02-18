using System;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class ContactHistory
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public int ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string OutletName { get; set; }
        public int OutletId { get; set; }
        public int EntityId { get; set; }
        public bool IsCustomActivity { get; set; }
    }
}
