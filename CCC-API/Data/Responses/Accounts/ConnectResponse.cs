namespace CCC_API.Data.Responses.Accounts
{
    /// <summary>
    /// response from endpoint api/v1/accounts/connect
    /// </summary>
    public class ConnectResponse
    {
        public string SessionKey { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
