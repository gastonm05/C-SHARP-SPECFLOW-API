using System;
using CCC_API.Data.Responses;
using RestSharp;
using CCC_API.Data.Responses.PrNewswire;
using CCC_API.Services;

namespace CCC_API.Steps.PrNewswire
{
    public class DistributionVisibilityReportUrlService : AuthApiService
    {
        public static string DistributionVisibilityReportEndPoint = "prnewswire/distribution/VisibilityReportsUrl";

        public DistributionVisibilityReportUrlService(string sessionKey) : base(sessionKey) { }

        public IRestResponse<DistributionVisibilityReportUrl> GetOMCLink()        {
            return Get<DistributionVisibilityReportUrl>(DistributionVisibilityReportEndPoint);
        }
        /// <summary>
        /// Returns a bool value if url passed it's valid or not
        /// </summary>
        /// <param name="urlString"></param>        
        /// <returns>bool</returns>
        internal bool IsValidUrl(string urlString)
        {
            Uri uri;
            return Uri.TryCreate(urlString, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                 || uri.Scheme == Uri.UriSchemeHttps
                 || uri.Scheme == Uri.UriSchemeFtp
                 || uri.Scheme == Uri.UriSchemeMailto
                    );
        }
    }
}