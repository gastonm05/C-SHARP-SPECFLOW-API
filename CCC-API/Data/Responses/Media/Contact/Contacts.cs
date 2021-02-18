using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class Contacts
    {
        public int ItemCount { get; set; }
        public List<ContactsItem> Items { get; set; }
        public string Key { get; set; }
        public int ActiveCount { get; set; }
    }
}
