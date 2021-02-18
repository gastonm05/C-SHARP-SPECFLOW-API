namespace CCC_API.Data.Responses.News
{
    public class ClipbookItem
    {
        public int Id { get; set; }
        public string NewsDate { get; set; }
        public string Headline { get; set; }
        public int CirculationAudience { get; set; }
        public int UniqueVisitors { get; set; }
        public int PublicityValue { get; set; }
        public string OutletCountry { get; set; }
        public string Medium { get; set; }
    }
}
