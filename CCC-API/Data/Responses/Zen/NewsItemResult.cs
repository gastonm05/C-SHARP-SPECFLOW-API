namespace CCC_API.Data.Responses.Zen
{
    /// <summary>
    /// NewsItems returned from the Zen Coverage endpoint only contain news item id
    /// </summary>
    public class NewsItemResult
    {
        public string NewsItemId { get; set; }
    }
}
