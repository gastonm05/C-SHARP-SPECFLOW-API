namespace CCC_API.Data.PostData.Media.Contact
{
    public class ContactListData
    {
        public ContactListData() { }

        public ContactListData(int listId, string listName)
        {
            ListId = listId;
            ListName = listName;
        }

        public int ListId { get; set; }

        public string ListName { get; set; }
    }
}
