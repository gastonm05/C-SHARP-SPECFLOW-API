namespace CCC_API.Data.Responses.News
{
    public class NewsItemAnalyticsSearch
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string CategoryText { get; set; }
        public string Name { get; set; }
        public int? Impact { get; set; }
        public int? Prominence { get; set; }
        public Tone Tone { get; set; }
    }
}
