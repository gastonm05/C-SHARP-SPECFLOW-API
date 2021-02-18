using System.Linq;
using CCC_API.Services.Common.db;
using CCC_API.Services.Common.Support;
using Dapper;

namespace CCC_API.Services.News.DB
{
    public class NewsDbService : BaseDbService
    {
        private readonly int _companyId;

        public NewsDbService(int companyId) : base(new CompanyService().GetConnectionToCompanyDb(companyId))
        {
            _companyId = companyId;
        }

        /// <summary>
        /// Deletes existing news for the company.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int DeleteNewsForCompany(int limit = 50)
        {
            var sql = $"SELECT TOP({limit}) * FROM zzNews_{_companyId} WHERE CompanyID = {_companyId}";
            var result = Connection.Query(sql).Count();

            // Check if any results, just to be safe if someone mess with query
            if (result > 0)
            {
                var deleteSql = $"DELETE TOP({limit}) FROM zzNews_{_companyId} WHERE CompanyID = {_companyId}";
                var deleteResult = Connection.Execute(deleteSql);
                return deleteResult;
            }

            return result;
        }
    }
}
