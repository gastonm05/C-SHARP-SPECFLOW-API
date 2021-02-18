using CCC_API.Data.Responses.News;

namespace CCC_API.Data.Responses.Email
{
    /// <summary>
    /// Email distribution > Html template.
    /// </summary>
    public class HtmlTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Html { get; set; }
        public string Thumbnail { get; set; }
        public int Type { get; set; }
        public Template_Meta _meta { get; set; }
    }

    public struct HtmlTemplateReq
    {
        public string name { get; set; }
        public string htmlContent { get; set; }
    }
}
    
