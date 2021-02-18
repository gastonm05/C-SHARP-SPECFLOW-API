namespace CCC_API.Data.Responses.Impact.CisionIDEarned
{
    public class SearchCID
    {
        public DataList[] Data { get; set; }
        public Parameters Parameter { get; set; }
        public int Status { get; set; }

        public class DataList
        {
            public int SearchId { set; get; }
            public string SearchName { set; get; }
            public double TotalURLCount { set; get; }
            public string C3Acctld { set; get; }
        }

        public class Parameters
        {
            public string C3Acctld { set; get; }
        }
    }
}
