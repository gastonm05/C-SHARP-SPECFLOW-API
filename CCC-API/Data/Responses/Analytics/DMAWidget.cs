
namespace CCC_API.Data.Responses.Analytics
{
    public class DMAWidget
    {
        public DataPoint[] DataPoints { get; set; }
        public string Name { get; set; }

        public class DataPoint
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public double Lat { set; get; }
            public double Lon { set; get; }
            public long Z { set; get; }
        }
    }
}
