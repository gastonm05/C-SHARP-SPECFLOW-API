using CCC_API.Data.PostData.Common;
using CCC_API.Data.TestDataObjects.Activities;
using RestSharp;

namespace CCC_API.Services.Common
{
    public class ContactService : AuthApiService
    {
        private const string CONTACT_SERVICE_ENDPOINT = "contact/support";
        private const string EDITORIAL_CONTACT_SERVICE_ENDPOINT = "contact/editorial";
        private const string MEDIA_RESEARCH_SERVICE_ENDPOINT = "contact/mediaresearch";
        

        public ContactService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a POST to EditorialContact endpoint
        /// </summary>
        /// <param name="EditorialSupportPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse SendEditorialContactRequest(EditorialSupportPostData editorialSupportPostData)
        {
            string resource = string.Format($"{EDITORIAL_CONTACT_SERVICE_ENDPOINT}");
            return Post<object>(resource, editorialSupportPostData);
        }

        /// <summary>
        /// Performs a POST to MediaResearch endpoint
        /// </summary>
        /// <param name="EditorialSupportPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse SendMediaResearchRequest(MediaResearchRequest mediaResearchRequest)
        {
            return Post<object>(MEDIA_RESEARCH_SERVICE_ENDPOINT, mediaResearchRequest);
        }

        public IRestResponse SendMediaResearchRequest(string changeType, int id, string entityType)
        {
            MediaResearchRequest mediaResearchRequest = new MediaResearchRequest(id, changeType, entityType);
            return this.SendMediaResearchRequest(mediaResearchRequest);
        }
        /// <summary>
        /// Get request for the contact service endpoint with the given user and language key
        /// </summary>
        /// <param name="languageKey"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse GetContactSupportInfo(string languageKey)
        {
            string resource = string.Format($"{CONTACT_SERVICE_ENDPOINT}?languageKey={languageKey}");
            return Get<object>(resource);
        }

        /// <summary>
        /// POST request for the contact service endpoint to Sends contact email to support
        /// </summary>
        /// <param name="contactSupportPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse SendMediaResearchRequest(ContactSupportPostData contactSupportPostData)
        {
            return Post<object>(CONTACT_SERVICE_ENDPOINT, contactSupportPostData);
        }
    }
}
