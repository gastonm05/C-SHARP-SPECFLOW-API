using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Utils;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Media.Contact
{
    public class InstagramStreamService : AuthApiService
    {
        public InstagramStreamService(string sessionKey) : base(sessionKey) { }

        public static string ContactsEndpoint = "media/contacts";
        public static string ContactInstagramDetailsEndpoint = "Instagram/details";
        public static string InstagramFollowEndpoint = "social/instagram/users";
        public static string FollowEndpoint = "follow"; //used to unfollow too, then operation is a DELETE

        /// <summary>
        /// Get endpoint for retrieving Instagram details for a specific contact
        /// </summary>
        /// <param name="contactId">The contact id</param>
        /// <returns>The response body contains relevant data from the contact's Instagram account.</returns>
        public IRestResponse<InstagramDetail> GetContactInstagramDetails(int contactId)
        {
            return Get<InstagramDetail>($"{ContactsEndpoint}/{contactId}/{ContactInstagramDetailsEndpoint}");
        }

        /// <summary>
        /// Post endpoint for following a contact's Instagram - This feature is behind a parameter in Prod environment
        /// For Internal and Released environmnets we use fake data in Contact's IG streams. So it will not be possible to
        /// 'follow' a contact until we get permissions from IG.
        /// </summary>
        /// <param name="contactId">The id of the contact whose Instagram we wish to follow</param>
        /// <returns>The response should be 200.</returns>
        public string PostFollowContactInstagram(int contactId)
        {
            IDictionary<string, string> postData = new Dictionary<string, string>();
            return SimplePost(ApiEndpoints.UriBaseDomain, $"{InstagramFollowEndpoint}/{contactId}/{FollowEndpoint}", postData, GetAuthorizationHeader());
        }

        /// <summary>
        /// Delete endpoint for unfollowing a contact's Instagram - This feature is behind a parameter in Prod environment
        /// For Internal and Released environmnets we use fake data in Contact's IG streams. So it will not be possible to
        /// 'unfollow' a contact until we get permissions from IG.
        /// </summary>
        /// <param name="contactId">The id of the contact whose Instagram we wish to unfollow</param>
        /// <returns>The response should be 200.</returns>
        public string DeleteUnfollowContactInstagram(int contactId)
        {
            return SimpleDelete(ApiEndpoints.UriBaseDomain, $"{InstagramFollowEndpoint}/{contactId}/{FollowEndpoint}", GetAuthorizationHeader());
        }
    }
}
