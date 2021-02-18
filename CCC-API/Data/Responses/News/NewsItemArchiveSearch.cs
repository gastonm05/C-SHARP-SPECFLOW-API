namespace CCC_API.Data.Responses.News
{
    public class NewsItemArchiveSearch
    {
        public string Id { get; set; }
        public string Headline { get; set; }
        public string TextPreview { get; set; }
        public string NewsDate { get; set; }
        public string CreationDate { get; set; }
        public string OutletName { get; set; }
        public string OutletType { get; set; }
        public int ToneId { get; set; }
        public string Tone { get; set; }
        public string ArticleUrl { get; set; }
        public string Clip { get; set; }
        public ArchiveItemFeed Feed { get; set; }
        public NewsItemArchiveSearchLinks _links { get; set; }
    }
}
