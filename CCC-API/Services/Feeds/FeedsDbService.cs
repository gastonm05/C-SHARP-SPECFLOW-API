using CCC_API.Services.Common.db;
using CCC_API.Services.Common.Support;
using Dapper;
using System;

namespace CCC_API.Services.Feeds
{
    public class FeedsDbService : BaseDbService
    {
        private readonly int _companyId;

        public FeedsDbService(int companyId) : base(new CompanyService().GetConnectionToCompanyDb(companyId))
        {
            _companyId = companyId;
        }

        /// <summary>
        /// Gets the news count by feed id that occured between the start and end datetimes.
        /// </summary>
        /// <param name="start">The start datetime to begin looking for news.</param>
        /// <param name="end">The end datetime to begin looking for news.</param>
        /// <param name="feedId">The feed identifier.</param>
        /// <returns></returns>
        public int GetNewsCount(string feedId, DateTime start, DateTime end)
        {
            var sql = $"SELECT COUNT(1) FROM zzNews_{_companyId} WHERE ExternalId LIKE '%VNOD{feedId}%'" +
                      $" AND CreationDate > '{start.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                      $" AND CreationDate < '{end.ToString("yyyy-MM-dd HH:mm:ss")}'";
            return Connection.ExecuteScalar<int>(sql);
        }
    }
}
