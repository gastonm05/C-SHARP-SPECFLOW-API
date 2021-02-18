using CCC_API.Data.TestDataObjects;

namespace CCC_API.Data.Responses.Accounts
{
    /// <summary>
    /// response from endpoint api/v1/accounts/#companyId/profiles
    /// </summary>
    public class ProfilesResponse
    {
        public int ItemCount { get; set; }
        public Profile[] Items { get; set; }
    }
}
