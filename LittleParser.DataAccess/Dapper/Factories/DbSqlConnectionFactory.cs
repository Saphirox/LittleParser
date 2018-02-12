using System.Data;
using System.Data.SqlClient;
using LittleParser.Common.Constants;

namespace LittleParser.DataAccess.Dapper.Factories
{
    public class DbSqlConnectionFactory : IDbConnectionFactory
    {
        public DbSqlConnectionFactory()
        {
            
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(DbConstants.DB_CONNECTION);
        }
    }
}