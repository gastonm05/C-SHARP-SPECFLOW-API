
namespace CCC_API.Data.Responses.Impact.CisionId
{
    public class WebEventsCisionId : CisionIdBaseImpactResponse
    {
        public EventData[] EventData { get; set; }
    }

    public class EventData
    {
        public string EventName { set; get; }
        public float EventCount { set; get; }
    }
}
