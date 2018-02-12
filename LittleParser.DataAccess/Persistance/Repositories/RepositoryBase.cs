using System.Data;

namespace LittleParser.DataAccess.Persistance.Repositories
{
    public abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        
        protected IDbConnection Connection => Transaction.Connection;

        protected RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}