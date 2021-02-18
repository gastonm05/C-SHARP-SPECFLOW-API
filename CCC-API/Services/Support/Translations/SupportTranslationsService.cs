using RestSharp;
using System;

namespace CCC_API.Services
{
    /// <summary>
    /// A service for /support/translations
    /// The service requires two parameters to fetch the translations from the correct table
    ///   sourceId : 0 for client and 1 for server
    ///   release : release version
    /// There is a single endpoint for all environments.
    /// </summary>
    public class SupportTranslationsService : BaseApiService
    {
        public SupportTranslationsService() : base() {}

        public static string SupportTranslationsBaseUrl = "http://172.16.124.108:86/api/v1/";

        private string GenerateResourceURL(string sourceId, string release)
        {
            string sourceIdValue = "";
            if(sourceId == "client")
            {
                sourceIdValue = "0";
            }
            else if (sourceId == "server")
            {
                sourceIdValue = "1";
            }
            else
            {
                throw new ArgumentException($"sourceId '{sourceId}' is an invalid value. Valid values are: 'client', 'server'");
            }
            return $"support/translations?sourceId={sourceIdValue}&release={release}";
        }

        public string GetTranslations(string sourceId, string release)
        {
            Uri uri = new Uri(SupportTranslationsBaseUrl);
            string resourceUrl = GenerateResourceURL(sourceId, release);
            IRestClient client = new RestClient(SupportTranslationsBaseUrl);
            IRestRequest request = new RestRequest(resourceUrl, Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
