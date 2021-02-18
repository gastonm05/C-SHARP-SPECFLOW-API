namespace CCC_API.Data.Responses.News
{
    public class NewsQuery
    {
        public string Key { get; set; }
        public int TotalCount { get; set; }
        public QueryLinks _links { get; set; }
    }
}
