namespace CCC_API.Data.Responses.News
{
    public class SavedSearchItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataGroupId { get; set; }
        public SavedSearchItemCriteria Criteria { get; set; }
        public SavedSearchMeta _meta { get; set; }
    }
}