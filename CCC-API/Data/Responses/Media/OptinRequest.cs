
namespace CCC_API.Data.Responses.Media
{
   public class OptinRequest
    {
        public string Key { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string SenderName { get; set; }
        public bool HasOptedIn { get; set; }
    }
}
