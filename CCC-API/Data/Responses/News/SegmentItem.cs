namespace CCC_API.Data.Responses.News
{
    public class SegmentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public int StartSecond { get; set; }
        public int EndSecond { get; set; }
        public string CreationDateUTC { get; set; }
        public UserIdAndName CreatedBy { get; set; }
        public UserIdAndName UpdatedBy { get; set; }
        public SegmentLinks _links { get; set; }
    }

    public class UserIdAndName
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SegmentLinks
    {
        public string self { get; set; }
    }
}
