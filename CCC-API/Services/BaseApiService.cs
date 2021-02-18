using CCC_API.Utils;
using System;

namespace CCC_API.Services
{
    public class BaseApiService : CCC_Infrastructure.API.Services.BaseApiService
    {
        public override Uri BaseDomainUri => ApiEndpoints.UriBaseDomain;
    }
}
