using CCC_API.Services.DbConnection;
using System;
using System.Data.SqlClient;
using CCC_Infrastructure.Utils;

namespace CCC_API.Services.Common.Support
{
    public class CompanyService
    {
        private readonly string _sqlSiteManagerConnectionString;

        private readonly SiteManagerService _siteManagerService;

        public CompanyService(SiteManagerService siteManagerService, string sitemanagerConnectionString)
        {
            _siteManagerService = siteManagerService;
            _sqlSiteManagerConnectionString = sitemanagerConnectionString;
        }

        public CompanyService()
        {
            _siteManagerService = new SiteManagerService();
            _sqlSiteManagerConnectionString = TestSettings.GetConfigValue("SitemanagerDbUrl");
        }
        
        /// <summary>
        /// Provides connection to a company by company id.
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public SqlConnection GetConnectionToCompanyDb(int companyId)
        {
            var dbInfo = _siteManagerService.GetDatabaseInfoForCompany(companyId);
            return GetConnectionToCompanyDb(dbInfo);
        }

        /// <summary>
        /// Provides connection to a company by company name.
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public SqlConnection GetConnectionToCompanyDbByName(string companyName)
        {
            var dbInfo = _siteManagerService.GetDatabaseInfoForCompanyByName(companyName);
            return GetConnectionToCompanyDb(dbInfo);
        }

        /// <summary>
        /// Provides connection to a company by dbInfo.
        /// </summary>
        /// <param name="dbInfo"></param>
        /// <returns></returns>
        public SqlConnection GetConnectionToCompanyDb(CompanyDatabase dbInfo)
        {
            if (dbInfo == null)
            {
                throw new ArgumentException(Err.Msg(nameof(dbInfo)));
            }

            var connectionUrl = "Server={0}; database={1};User Id={2};Password={3}";

            var serverName = dbInfo.ServerName;
            var fullServerName = ".vocusdr.com";
            if (serverName.Contains(fullServerName))
            {
                fullServerName = serverName;
                serverName = serverName.Replace(".vocusdr.com", "");
            }
            else
            {
                fullServerName = serverName + fullServerName;
            }

            var connectionConf = new SqlConnectionStringBuilder(_sqlSiteManagerConnectionString);
            var sqlUser = connectionConf.UserID;
            var sqlPassword = connectionConf.Password;
            
            //connect using sql login credentials: vpr
            try
            {
                var connection = new SqlConnection(string.Format(connectionUrl, fullServerName, dbInfo.DatabaseName, sqlUser, sqlPassword));
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get conection info for company using vpr " + ex.Message);
                //retry below using vprServerName as user
                var connection = new SqlConnection(string.Format(connectionUrl, fullServerName, dbInfo.DatabaseName, sqlUser + serverName, sqlPassword));
                connection.Open();
                return connection;
            }
        }
    }
}
