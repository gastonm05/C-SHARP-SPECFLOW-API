namespace CCC_API.Services.DbConnection
{
    public class CompanyDatabase
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }

        public CompanyDatabase(string databaseName, string serverName)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
        }
    }
}
