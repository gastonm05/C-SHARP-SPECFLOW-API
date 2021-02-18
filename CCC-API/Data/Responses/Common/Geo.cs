using System.Collections.Generic;

namespace CCC_API.Data.Responses.Common
{
    /// <summary>
    /// Class represents Country objects returned from api/v1/geo/countries
    /// </summary>
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LanguageId { get; set; }
    }

    /// <summary>
    /// Class represents objects returned from api/v1/geo endpoint
    /// </summary>
    public class GeoItems
    {
        public List<Place> Items { get; set; }
    }

    /// <summary>
    /// Class represents Place objects returned from api/v1/geo/places
    /// </summary>
    public class Place
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
