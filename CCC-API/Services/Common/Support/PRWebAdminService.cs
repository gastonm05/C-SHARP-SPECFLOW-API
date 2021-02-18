using CCC_Infrastructure.Utils;
using System;
using System.Data.SqlClient;

namespace CCC_API.Services.Common.Support
{
    public class PRWebAdminService
    {
        public static string sqlPRWebAdminUrl = TestSettings.GetConfigValue("sqlPRWebAdminUrl");

        SqlConnection connection = null;

        /// <summary>
        /// Connect to the PRWEb Admin DB directly.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnectionToPRWebAdminDb()
        {
            try
            {
                connection = new SqlConnection(sqlPRWebAdminUrl);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to get conection info for prwebAdmin db " + e.Message);
            }
            return connection;
        }

        public void TearDown()
        {
            if (connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error during teardown of DbPRWebAdminService " + e.Message);
                }
            }
        }
    }
}
