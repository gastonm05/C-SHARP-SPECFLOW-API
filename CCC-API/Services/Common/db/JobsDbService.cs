using System.Collections.Generic;
using CCC_API.Services.Common.Support;
using Dapper;

namespace CCC_API.Services.Common.db
{
    public class JobsDbService : BaseDbService
    {
        private int _companyId;

        public JobsDbService(int companyId) : base(new CompanyService().GetConnectionToCompanyDb(companyId))
        {
            _companyId = companyId;
        }

        /// <summary>
        /// Get jobs from JobGeneric table. 
        /// </summary>
        /// <param name="userAccountID"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetRecentJobScheduleJobs(int userAccountID)
        {
            var sqlStatement =
                $@"SELECT * FROM [dbo].JobSchedule js 
                   JOIN [dbo].JobGeneric jg ON js.JobActionId = jg.JobActionId 
                   JOIN [dbo].UserAccount u ON u.UserAccountID = js.UserAccountID
                   WHERE js.UserAccountID = {userAccountID}";

           return Connection.Query(sqlStatement);
        }
    }
}
