namespace CCC_API.Data.Responses.News
{
    public class Outlet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MediaType { get; set; }
        public string OutletMedium { get; set; }
        public string OutletDMAName { get; set; }
        public string OutletCountry { get; set; }
        public int UniqueVisitors { get; set; }
        public OutletLinks _links { get; set; }
    }
}
