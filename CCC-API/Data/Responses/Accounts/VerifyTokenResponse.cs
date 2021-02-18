namespace CCC_API.Data.Responses.Accounts
{
    public class VerifyTokenResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string OMCAccountID { get; set; }
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
    }
}
