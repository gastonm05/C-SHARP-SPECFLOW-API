using System;
using CCC_API.Services.DbConnection;
using CCC_Infrastructure.Utils;
using System.Data.SqlClient;
using Dapper;

namespace CCC_API.Services.Common.Support
{
    /// <summary>
    /// Provide a means to look up the SQL connectionString information for a particular company.
    /// </summary>
    public class SiteManagerService
    {
        private static readonly string sqlSiteManagerUrl = TestSettings.GetConfigValue("SitemanagerDbUrl");

        private const string BaseGetCompanyDbQuery = 
            "SELECT  DB.Name AS DatabaseName, Server.Name AS ServerName " +
                "FROM dbo.CompanyIDDbGroupIdXRef " +
                "INNER JOIN dbo.CompanyLoginNameDbGroupIdXRef ON CompanyLoginNameDbGroupIdXRef.CustomerID = CompanyIDDbGroupIdXRef.CustomerID " +
                "LEFT JOIN dbo.DBGroup " +
                "ON DBGroup.DBGroupID = CompanyIDDbGroupIdXRef.DbGroupID " +
                "LEFT JOIN dbo.DB " +
                "ON DB.DbGroupID = DBGroup.DBGroupID " +
                "LEFT JOIN dbo.Application " +
                "ON Application.ApplicationID = CompanyIDDbGroupIdXRef.ApplicationID " +
                "LEFT JOIN dbo.Site " +
                "ON Application.SiteID = Site.SiteID " +
                "LEFT JOIN dbo.Server " +
                "ON DB.ServerID = Server.ServerID " +
                "WHERE DB.StatusID = 1 AND Db.DbTypeID=1 ";

        /// <summary>
        /// Provides company information by company Id.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public CompanyDatabase GetDatabaseInfoForCompany(int companyId, int applicationId = 0)
        {
            var sqlStatement = BaseGetCompanyDbQuery + $"AND CompanyIDDbGroupIdXRef.CompanyID = {companyId} ";
            if (applicationId != 0)
            {
                sqlStatement += $" AND CompanyIDDbGroupIdXRef.ApplicationID = {applicationId} ";
            }

            using (var connection = new SqlConnection(sqlSiteManagerUrl))
            {
                dynamic data = connection.QueryFirst(sqlStatement);
                return new CompanyDatabase(data.DatabaseName?.ToString(), data.ServerName.ToString());
            }
        }

        /// <summary>
        /// Provides company database information by company name.
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public CompanyDatabase GetDatabaseInfoForCompanyByName(string companyName, int applicationId = 0)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                throw new ArgumentException(Err.Msg(nameof(companyName)));
            }

            var sqlStatement = BaseGetCompanyDbQuery +
                               $" AND CompanyLoginNameDbGroupIdXRef.CompanyLoginName = '{companyName}' ";

            if (applicationId != 0)
            {
                sqlStatement += $" AND CompanyIDDbGroupIdXRef.ApplicationID = {applicationId}";
            }

            using (var connection = new SqlConnection(sqlSiteManagerUrl))
            {
                dynamic data = connection.QueryFirst(sqlStatement);
                return new CompanyDatabase(data.DatabaseName?.ToString(), data.ServerName.ToString());
            }
        } 
    }
}
