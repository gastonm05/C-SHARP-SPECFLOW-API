
namespace CCC_API.Data.Responses.Impact.Earned
{
    public class SearchEarned
    {
        public Searches[] Searches { get; set; }
        public int TotalUrlLimit { set; get; }
        public float PercentageUrlUsed { set; get; }
        public int SearchLimit { set; get; }
    }

    public class Searches
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public double WebEvents { set; get; }
        public double URLs { set; get; }
        public double Views { set; get; }
        public string Clause { set; get; }
        public string MediaMonitorId { set; get; }
    }
}
