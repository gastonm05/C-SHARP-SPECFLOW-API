namespace CCC_API.Data.Responses.Impact.CisionIDEarned
{
    public class TopOutletsCID
    {
        public int Status { get; set; }
        public DataListCID[] Data { get; set; }
        public Parameters Parameter { get; set; }

        public class DataListCID
        {
            public int MediaId { set; get; }
            public string OutletName { set; get; }
            public float CdrValue { set; get; }
            public int UrlCount { set; get; }
            public string CleanedHomePage { set; get; }
        }

        public class Parameters
        {
            public string FromDate { set; get; }
            public int Top { set; get; }
            public string ToDate { set; get; }
            public int SearchId { set; get; }
        }
    }
}