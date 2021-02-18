using System.Collections.Generic;

namespace CCC_API.Data.PostData.Media.Contact
{
    class DeleteContactsInAList
    {
        public string Key { get; set; }
        public bool SelectAll { get; set; }
        public int[] Delta { get; set; }
        public string Name { get; set; }
        public int ListId { get; set; }
    }
}
