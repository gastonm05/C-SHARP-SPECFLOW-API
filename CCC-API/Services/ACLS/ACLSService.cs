using CCC_API.Data.Responses.ACLS;
using RestSharp;

namespace CCC_API.Services.ACLS
{
    public class ACLSService : AuthApiService
    {
        public static string ACLSEndPoint = "acls";

        public ACLSService(string sessionKey) : base(sessionKey) { }

        public IRestResponse<ACLSView> GetACLS()
        {
            return Get<ACLSView>(ACLSEndPoint);
        }
    }   
}