using CCC_API.Data.Responses.Company;
using CCC_Infrastructure.Utils;
using RestSharp;
using System.Collections.Generic;
using CCC_Infrastructure.API.Utils;
using System.Reflection;

namespace CCC_API.Services.Company
{
    public class CompanyService : BaseApiService
    {
        public static string CompanyIdEndPoint = "company/CompanyId";

        public CompanyService() { }

        private string getXAPIKEYHeaderValue(string filename)
        {
            System.Dynamic.ExpandoObject expando = TestData.DeserializedJsonKeyValuePairs(filename, Assembly.GetExecutingAssembly(), false);
            return ((IDictionary<string, object>)expando)["value"].ToString();
        }

        public IRestResponse<CompanyId> GetCompanyId(string customerId)
        {
            string endpoint = $"{CompanyIdEndPoint}?customerId={customerId}";
            RestBuilder restBuilder = Request().Get().ToEndPoint(endpoint);
            // get X-API-KEY header value. Varies for each environment.
            string xApiKey  = getXAPIKEYHeaderValue("CompanyIdXAPIKEYHeader.json");
            restBuilder.AddHeader("X-API-KEY", xApiKey);
            return restBuilder.Exec<CompanyId>();
        }

        public IRestResponse<CompanyIdUnauthorized> GetCompanyIdUnauthorized(string customerId)
        {
            string endpoint = $"{CompanyIdEndPoint}?customerId={customerId}";
            RestBuilder restBuilder = Request().Get().ToEndPoint(endpoint);
            // get X-API-KEY header value. Varies for each environment.
            string xApiKey = getXAPIKEYHeaderValue("CompanyIdXAPIKEYHeader.json");
            restBuilder.AddHeader("X-API-KEY", xApiKey);
            return restBuilder.Exec<CompanyIdUnauthorized>();
        }
    }   
}