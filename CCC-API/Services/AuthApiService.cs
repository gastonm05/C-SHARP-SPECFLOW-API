using CCC_API.Utils;
using System;

namespace CCC_API.Services
{
    public class AuthApiService : CCC_Infrastructure.API.Services.AuthApiService
    {
        
        public AuthApiService(string sessionKey) : base(sessionKey)
        {
        }

        public override Uri BaseDomainUri => ApiEndpoints.UriBaseDomain;
    }
}
