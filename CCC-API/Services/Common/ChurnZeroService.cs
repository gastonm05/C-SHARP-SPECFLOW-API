using RestSharp;

namespace CCC_API.Services.Common
{
    public class ChurnZeroService : AuthApiService
    {
        public ChurnZeroService(string sessionKey) : base(sessionKey)  {  }

        public static string ChurnZeroEndpoint = "integrations/churnzero";

        public IRestResponse GetChurnZeroCompany() =>
            Request().Get().ToEndPoint(ChurnZeroEndpoint).Exec();
    }
}
