using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CCC_API.Data.Responses.Activities;
using CCC_API.Services.Common.db;
using CCC_API.Services.Common.Support;

namespace CCC_API.Services.Activities.DB
{
    public class ActivitiesDbService : BaseDbService
    {
        public ActivitiesDbService(int companyId) : base(new CompanyService().GetConnectionToCompanyDb(companyId)){}

        public ActivitiesDbService(SqlConnection connection) : base(connection) {}

        /// <summary>
        /// Extracts activities by language.
        /// </summary>
        /// <param name="dataGroupId"></param>
        /// <param name="language"></param>
        /// <returns>IList</returns>
        public IList<ExportActivity> GetAllActivities(int dataGroupId, string language = "English")
        {
            string sqlStatement =
                $@"SELECT actT.Name, PublicationState, COALESCE(Title, ContentSnippet) as Title, PublicationTime, ContentSnippet 
                   FROM [dbo].[PublishActivity] p
                   LEFT JOIN [dbo].[ActivityType] actT ON p.Type = actT.ActivityTypeID 
                   WHERE p.DataGroupId = '{dataGroupId}' 
                   AND actT.LanguageID  
                     IN ('0', (SELECT TOP 1 LanguageID FROM [dbo].[Locale] WHERE Name = '{language}'))  
                   ORDER BY PublicationTime DESC";

            var cmd = new SqlCommand(sqlStatement, Connection);
            var reader = cmd.ExecuteReader();

            IList<ExportActivity> list = new List<ExportActivity>();
            while (reader.Read())
            {
                var type = reader["Name"].ToString();
                var title = reader["Title"].ToString();
                var status = Convert.ToInt16(reader["PublicationState"]);
                var pubTime = reader.GetDateTime(3);

                var act = new ExportActivity
                {
                    Title = title,
                    Status = status,
                    Type = type,
                    DateTime = pubTime
                };
                list.Add(act);
            }
            return list;
        }
    }
}
