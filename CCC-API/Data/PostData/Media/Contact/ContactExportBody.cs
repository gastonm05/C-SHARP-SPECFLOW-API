namespace CCC_API.Data.PostData.Media.Contact
{
    public class ContactExportBody
    {
        public DeliveryOptions DeliveryOptions { get; set; }
        public DataOptions DataOptions { get; set; }
    }

    public class DeliveryOptions
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string[] Recipients { get; set; }
    }

    public class DataOptions
    {
        public int[] IncludedSections { get; set; }
        public AudienceGeographicAffinities AudienceGeographicAffinities { get; set; }
        public TopTerms TopTerms { get; set; }
    }

    public class AudienceGeographicAffinities
    {
        public string Title { get; set; }
        public string LegendItems { get; set; }
        public string SVG { get; set; }
    }

    public class TopTerms
    {
        public string Title { get; set; }
        public string LegendItems { get; set; }
        public string SVG { get; set; }
    }
}
