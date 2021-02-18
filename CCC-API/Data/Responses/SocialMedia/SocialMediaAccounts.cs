namespace CCC_API.Data.Responses.SocialMedia
{
    public class SocialMediaAccounts
    {
        public string AccountName { get; set; }
        public bool Authorized { get; set; }
        public string Avatar { get; set; }
        public int ExternalApplicationId { get; set; }
        public int Id { get; set; }
        public int MaxContentLength { get; set; }
        public string Type { get; set; }
    }
}
