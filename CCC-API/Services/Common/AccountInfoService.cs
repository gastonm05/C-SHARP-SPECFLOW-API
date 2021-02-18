using CCC_API.Data.Responses.Accounts;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Linq;

namespace CCC_API.Services.Common
{
    public class AccountInfoService : AuthApiService
    {
        public static string AccountSessionInfoEndPoint = "accounts/me";
        public static string AccountVerifyTokenEndPoint = "accounts/verifytoken";
        
        public AccountInfoService(string sessionKey) : base(sessionKey)
        {
        }

        private MeResponse _meResponse = null;
        /// <summary>
        /// Cached Me Response so that subsequent gets don't hit the endpoint again. Create a new instance if you want to refresh the cache.
        /// </summary>
        /// <value>
        /// Cached Me Response
        /// </value>
        public MeResponse Me
        {
            get
            {
                if (_meResponse == null)
                {
                    _meResponse = GetSessionInfo().Data;
                    if (_meResponse == null)
                        throw new Exception(Err.Msg($"Endpoint {AccountVerifyTokenEndPoint} returned no data."));
                }
                return _meResponse;
            }
        }

        /// <summary>
        /// Provides all the Company information that is in current session
        /// </summary>
        /// <returns></returns>
        public IRestResponse<MeResponse> GetSessionInfo()
        {
            return Get<MeResponse>(AccountSessionInfoEndPoint);
        }

        /// <summary>
        /// Gets the id for a datagroup by name. Calls accounts/me and profiles endpoint.
        /// </summary>
        /// <param name="name">The name of the datagroup.</param>
        /// <returns>Id for the datagroup or throws an AssertionException if a matching datagroup cannot be found.</returns>
        /// <exception cref="AssertionException">If a matching datagroup cannot be found.</exception>
        public int GetDataGroupId(string name)
        {
            var profiles = GetDataGroups();
            var dg = profiles.Items.FirstOrDefault(profile => profile.Name.ToLower() == name.ToLower());
            if (dg == null)
            {
                throw new Exception(Err.Msg($"Could not find datagroup '{name}'"));
            }
            return dg.Id;
        }

        /// <summary>
        /// Data groups for current user.
        /// </summary>
        /// <returns>ProfilesResponse</returns>
        public ProfilesResponse GetDataGroups()
        {
            var resource = $"accounts/{Me.Account.Id}/profiles";
            return Request().Get().ToEndPoint(resource).ExecContentCheck<ProfilesResponse>();
        }

        /// <summary>
        /// Gets the data groups for the user
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRestResponse GetDataGroupsById(int id)
        {
            var resource = $"accounts/{id}/profiles";
            return Request().Get().ToEndPoint(resource).Exec();
        }

        /// <summary>
        /// Changes the datagroup by name. Calls accounts/me, profiles and profiles/datagroupId endpoints.
        /// </summary>
        /// <param name="name">The name of the datagroup to select.</param>
        /// <exception cref="AssertionException">If a matching datagroup cannot be found.</exception>
        public void ChangeDataGroup(string name)
        {
            int dataGroupId = GetDataGroupId(name);
            if (dataGroupId != Me.Profile.Id)
            {
                var resource = $"accounts/{Me.Account.Id}/profiles/{dataGroupId}";
                Request().Put().ToEndPoint(resource).ExecCheck();
            }
        }

        /// <summary>
        /// Changes the data group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public IRestResponse ChangeDataGroup(int id)
        {             
            var resource = $"accounts/{Me.Account.Id}/profiles/{id}";
            return Request().Put().ToEndPoint(resource).Exec();            
        }

        /// <summary>
        /// Verifies the token.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<VerifyTokenResponse> VerifyToken()
        {
            var headers = GetAuthorizationHeader();
            headers.Add("cache-control", "no-cache");
            return Get<VerifyTokenResponse>(new Uri(TestSettings.GetConfigValue("BaseApiUrl")), AccountVerifyTokenEndPoint, headers);
        }
    }
}
