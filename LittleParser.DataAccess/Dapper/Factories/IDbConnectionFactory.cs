using System.Data;

namespace LittleParser.DataAccess.Dapper.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}