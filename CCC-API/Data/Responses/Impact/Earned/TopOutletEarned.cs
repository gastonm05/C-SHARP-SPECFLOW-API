namespace CCC_API.Data.Responses.Impact.Earned
{
    public class TopOutletEarned
    {
        public SeriesData[] Series { get; set; }
        public int Total { get; set; }
        public DataList[] Data { get; set; }

        public class DataList
        {
            public string SearchId { set; get; }
            public string Name { set; get; }
            public string Url { set; get; }
        }
    }
}
