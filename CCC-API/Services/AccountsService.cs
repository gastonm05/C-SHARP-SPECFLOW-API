using CCC_API.Data.PostData;
using CCC_API.PostData;
using CCC_API.Utils;
using CCC_Infrastructure.UserSupport;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace CCC_API.Services
{
    public class AccountsService : BaseApiService
    {
        public static readonly string SessionKey = "SessionKey";
        public static string AccountsConnectEndPoint = "accounts/connect";
        public static string AccountsMSAConnectEndPoint = "accounts/msa";
        public static string AccountsMSACodeEndPoint = "accounts/msa/code";
        public static string AccountsAutologin = "accounts/autologin";
        public static string AccountsSSOAutologin = "accounts/ssoautologin";

        /// <summary>
        /// Post data to the api endpoint to login users
        /// </summary>
        /// <param name="user">object that has CompanyID, Username and Password properties</param>
        /// <returns>SessionKey from response or null if it was not in the response</returns>
        public string Login(User user)
        {
            if (user == null)
                throw new ArgumentException(Err.Msg("User cannot be null"));

            var postData = new AccountsConnectPostData()
            {
                Company = user.CompanyID,
                Username = user.Username,
                Password = user.Password
            };
            var response = SimplePost(ApiEndpoints.UriBaseDomain, AccountsConnectEndPoint, postData);

            if (response == null)
                throw new NullReferenceException(Err.Msg($"Response from '{AccountsConnectEndPoint}' endpoint was null, could not login"));
            
            var sessionKey = response.ContainsKey(SessionKey) ? response[SessionKey] : null;

            if (string.IsNullOrEmpty(sessionKey))
                throw new Exception(Err.Msg($"Could not login to '{ApiEndpoints.UriBaseDomain}{AccountsService.AccountsConnectEndPoint}' using user '{user.CompanyID}/{user.Username}'"));
         
            return sessionKey;
        }

        /// <summary>
        /// Logins for a user with retry logic and default timeout.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string LoginWithRetry(User user)
        {
            if (user == null)
                throw new ArgumentException(Err.Msg("User cannot be null"));
            var postDataFinal = new AccountsConnectPostData();
            if (user.LCID == null)
            {
                var postData = new AccountsConnectPostData()
                {
                    Company = user.CompanyID,
                    Username = user.Username,
                    Password = user.Password,
                    LanguageKey = user.Language                
                };
                postDataFinal = postData;
            } else
            {
                var postData = new AccountsConnectPostData()
                {
                    Company = user.CompanyID,
                    Username = user.Username,
                    Password = user.Password,
                    LanguageKey = user.Language,
                    LCID = user.LCID
                };
                postDataFinal = postData;
            }

            var resp = Request()
                .Post()
                .ToEndPoint(AccountsConnectEndPoint)
                .Data(postDataFinal)
                .ExecUntil<Dictionary<string, string>>(r => r.StatusCode.Equals(HttpStatusCode.OK));
            var sessionKey = resp.Data[SessionKey];

            if (string.IsNullOrEmpty(sessionKey))
                throw new Exception(Err.Msg($"Could not login to '{ApiEndpoints.UriBaseDomain}{AccountsService.AccountsConnectEndPoint}' using user '{user.CompanyID}/{user.Username}'"));

            return sessionKey;
        }

        /// <summary>
        /// Post data to the api endpoint to login users
        /// </summary>
        /// <param name="user">object that has CompanyID, Username and Password properties</param>
        /// <returns>SessionKey from response or null if it was not in the response</returns>
        public Dictionary<string, string> Login(string company, string username, string password)
        {
            var postData = new AccountsConnectPostData()
            {
                Company = company,
                Username = username,
                Password = password
            };
            return SimplePost(ApiEndpoints.UriBaseDomain, AccountsConnectEndPoint, postData);                        
        }
        /// <summary>
        /// Post data to the api endpoint to login users
        /// </summary>
        /// <param name="company"</param>/param>
        /// <param name="username"</param>/param>
        /// <param name="password"</param>/param>
        /// <param name="languageKey"</param>/param>
        /// <param name="msaToken"</param>/param>
        /// <returns>SessionKey from response or null if it was not in the response</returns>
        public Dictionary<string, string> Login(string company, string username, string password, string languageKey, string msaToken)
        {
            var postData = new AccountsConnectPostData()
            {
                Company = company,
                Username = username,
                Password = password,
                LanguageKey = languageKey,
                MSAToken = msaToken

            };
            return SimplePost(ApiEndpoints.UriBaseDomain, AccountsConnectEndPoint, postData);
        }
        /// <summary>
        /// Post data to the api endpoint to login users
        /// </summary>
        /// <param name="company"</param>/param>
        /// <param name="username"</param>/param>
        /// <param name="password"</param>/param>
        /// <param name="LCID"</param>/param>
        /// <returns>SessionKey from response or null if it was not in the response</returns>
        public Dictionary<string, string> Login(string company, string username, string password, string lcid)
        {
            var postData = new AccountsConnectPostData()
            {
                Company = company,
                Username = username,
                Password = password,
                LCID = lcid               

            };
            return SimplePost(ApiEndpoints.UriBaseDomain, AccountsConnectEndPoint, postData);
        }

        /// <summary>
        /// Post data to the MSA login api endpoint to login users
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="loginInfo"></param>
        /// <returns>MSATokenfrom response or null if it was not in the response</returns>
        public Dictionary<string, string> MSALogin(int activationCode, string loginInfo)
        {
            var postData = new MSAConnectPostData()
            {
                MsaToken = activationCode,
                LoginInfo = loginInfo
            };
            return SimplePost(ApiEndpoints.UriBaseDomain, AccountsMSAConnectEndPoint, postData);
        }

        /// <summary>
        /// Post data to the MSA Code api endpoint to re send Activation Code to users
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns>Message and statusCode in the response</returns>
        public Dictionary<string, string> ReSendMSACode(string loginInfo)
        {
            var postData = new MSACodePostData()
            {
                LoginInfo = loginInfo
            };
            return SimplePost(ApiEndpoints.UriBaseDomain, AccountsMSACodeEndPoint, postData);
        }

        /// <summary>
        /// Post data to the AutoLogin Code api endpoint to re send login event in ChurnZero
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <returns>Message and statusCode in the response</returns>
        public IRestResponse autoLogin(String sessionKey)
        {
            IDictionary<string, string> postData = new Dictionary<string, string>();
            IDictionary<string, string> headers = new Dictionary<string, string>() { { "Authorization", $"Bearer {sessionKey}" } };
            return Post(ApiEndpoints.UriBaseDomain, AccountsAutologin, postData, headers);
        }
        /// <summary>
        /// Post data to login to SSO AutoLogin api endpoint
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <returns>Message and statusCode in the response</returns>
        public Dictionary<string, string> SSOAutoLogin(string company)
        {
            IDictionary<string, string> postData = new Dictionary<string, string>() { { "Company", $"{company}" } };            
            return SimplePost(ApiEndpoints.UriBaseDomain, AccountsSSOAutologin, postData);
            
        }


    }
}
