namespace CCC_API.PostData
{
    /// <summary>
    /// post data for endpoint api/v1/accounts/connect
    /// </summary>
    public class AccountsConnectPostData
    {
        public string Company { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LanguageKey { get; set; } = "en-us";
        public string MSAToken { get; set; }
        public string LCID { get; set; } = "1033";
    }
}
