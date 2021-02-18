using CCC_API.Data.Responses.Common;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Common
{
    public class GeoService : AuthApiService
    {
        public GeoService(string sessionKey) : base(sessionKey)
        {
        }

        public static string GeoCountriesEndPoint = "/geo/countries";
        public static string GeoPlacesEndPoint = "geo/places";

        public IRestResponse<GeoItems> GetPlaces(string parameter)
        {
            return Get<GeoItems>($"{GeoPlacesEndPoint}?limit=500&name={parameter}");
        }

        /// <summary>
        /// This method returns all the valid countries.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<Country>> GetValidCountries()
        {
            return Get<List<Country>>(GeoCountriesEndPoint);
        }
    }
}
