namespace CCC_API.Data.PostData.Media.Contact
{
    public class EntityListFilterBody : BaseEntityListFilterBody
    {
        public int MembershipEntityId { get; set; }
    }

    public class BaseEntityListFilterBody
    {
        public string EntityType { get; set; }
        public bool IncludePublicAndShared { get; set; }
        public bool UnlimitedResults { get; set; }
        public string FilterType { get; set; }
        public string SortField { get; set; }
        public int SortDirection { get; set; }
        public bool ExcludeBounceBackLists { get; set; }
    }
}
