
namespace CCC_API.Data.PostData.Media.Outlet
{
    public class OutletListData
    {
        public OutletListData() { }

        public OutletListData(int listId, string listName)
        {
            ListId = listId;
            ListName = listName;
        }

        public int ListId { get; set; }

        public string ListName { get; set; }
    }
}
