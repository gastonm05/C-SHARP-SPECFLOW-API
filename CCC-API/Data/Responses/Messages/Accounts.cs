
namespace CCC_API.Data.Responses.Messages
{
    public class Accounts
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public string AccountName { get; set; }
        public string Avatar { get; set; }
        public bool Authorized { get; set; }
        public int ExternalApplicationId { get; set; }
        public int MaxContentLength { get; set; }

    }
}
