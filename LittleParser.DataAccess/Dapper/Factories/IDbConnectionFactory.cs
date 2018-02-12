using System.Data;

namespace LittleParser.DataAccess.Dapper.Factories
{
    /// <summary>
    /// Factory for a creating connection depending on ado.net provider
    /// </summary>
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}