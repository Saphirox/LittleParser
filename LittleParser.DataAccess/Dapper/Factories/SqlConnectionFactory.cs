using System.Data;
using System.Data.SqlClient;
using LittleParser.Common.Constants;

namespace LittleParser.DataAccess.Dapper.Factories
{
    /// <summary>
    /// Impementation using MSSQL
    /// </summary>
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(DbConstants.DB_CONNECTION);
        }
    }
}