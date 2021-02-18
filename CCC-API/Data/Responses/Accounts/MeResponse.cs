using CCC_API.Data.TestDataObjects;
using CCC_Infrastructure.UserSupport;

namespace CCC_API.Data.Responses.Accounts
{
    /// <summary>
    /// response from endpoint api/v1/accounts/me
    /// </summary>
    public class MeResponse
    {
        public Account Account { get; set; }
        public UserMe User { get; set; }
        public Profile Profile { get; set; }
    }
}
