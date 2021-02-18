using System.Net;
using CCC_API.Data.Responses.Activities;
using RestSharp;

namespace CCC_API.Services.Activities
{
    /// <summary>
    /// Create New > New Activity.
    /// </summary>
    public class CustomActivityService : AuthApiService
    {
        public const string CustomActivityUri = "activity/custom";

        public CustomActivityService(string sessionKey) : base(sessionKey){}
        
        /// <summary>
        /// Creates activity in C3.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="expHttpStatusCode"></param>
        public void PostActivity(CustomActivity data, HttpStatusCode expHttpStatusCode = HttpStatusCode.Created) =>
            Request().Post().Data(data).ToEndPoint(CustomActivityUri).ExecCheck(expHttpStatusCode);
        
        /// <summary>
        /// Get activity info in C3.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomActivity GetActivity(int id) => 
            Request().Get().ToEndPoint(CustomActivityUri + "/" + id).ExecCheck<CustomActivity>();

        /// <summary>
        /// Deletes activity in C3.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteActivity(int id) =>
            Request().Delete().ToEndPoint(CustomActivityUri + "/" + id).Exec();
    }
}
