using CCC_Infrastructure.Utils;
using System;
using System.Data.SqlClient;

namespace CCC_API.Services.Common.db
{
    public abstract class BaseDbService : IDisposable
    {
        protected SqlConnection Connection { get; }

        protected BaseDbService(SqlConnection connection)
        {
            Connection = connection;
        }
        
        public void Dispose()
        {
            var v = Connection?.AsExceptional().ThenExecute(c =>
            {
                c.Dispose();
                return c;
            });
            if (v != null && v.HasException)
                Console.WriteLine("Closing connection issue: " + v.Exception);

        }
    }
}
