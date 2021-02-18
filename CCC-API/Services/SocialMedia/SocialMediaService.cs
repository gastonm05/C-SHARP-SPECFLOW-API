using CCC_API.Data.Responses.SocialMedia;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.SocialMedia
{
    public class SocialMediaService : AuthApiService
    {
        public SocialMediaService(string sessionKey) : base(sessionKey)  {  }

        public static string PinterestBoardsEndpoint = "social/pinterest/";
        public static string SocialAccountsEndpoint = "social/accounts";
        public static string PRWSocialAccountsEndpoint = "distribution/prweb/socialaccounts";

        /// <summary>
        /// Gets the authenticated social media accounts for the company, if no accounts are authenticated
        /// with the company this will return an empty list
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<SocialMediaAccounts>> GetSocialMediaAccounts()
        {
            return Request().Get().ToEndPoint(SocialAccountsEndpoint).Exec<List<SocialMediaAccounts>>();
        }

        /// <summary>
        /// Gets the authenticated PRWeb social media accounts for the company, if no accounts are authenticated
        /// with the company this will return an empty list
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<SocialMediaAccounts>> GetPRWebSocialMediaAccounts()
        {
            return Request().Get().ToEndPoint( PRWSocialAccountsEndpoint ).Exec<List<SocialMediaAccounts>>();
        }

        /// <summary>
        /// Gets all pinterest boards for the user.
        /// </summary>
        /// <param name="cseId">The cse identifier.</param>
        /// <returns></returns>
        public IRestResponse<List<PinterestBoards>> GetPinterestBoards(int cseId)
        {
            return Request().Get().ToEndPoint($"{PinterestBoardsEndpoint}{cseId}/boards").Exec<List<PinterestBoards>>();
        }
    }
}
