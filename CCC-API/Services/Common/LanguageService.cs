using CCC_API.Data.Responses.Common;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.Common
{
    /// <summary>
    /// Languages endpoint does not need auth so lets inherit from base
    /// </summary>
    /// <seealso cref="CCC_API.Services.BaseApiService" />
    public class LanguageService : BaseApiService
    {
        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<Language>> GetLanguages() =>
            Request().Get().ToEndPoint("language").Exec<List<Language>>();
        
    }
}
