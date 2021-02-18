using CCC_Infrastructure.Utils;
using System;

namespace CCC_API.Utils
{
    /// <summary>
    /// Responsible for building urls for api endpoints
    /// </summary>
    public class ApiEndpoints
    {
        public static Uri UriBaseDomain = new Uri(TestSettings.GetConfigValue("BaseApiUrl"));

        /// <summary>
        /// Returns full url for api endpoint
        /// </summary>
        /// <param name="uri">api endpoint uri</param>
        /// <returns></returns>
        public static string Get(string uri)
        {
            return $"{UriBaseDomain}{uri}";
        }
    }
}