using System;
using System.Data;
using LittleParser.DataAccess.Dapper.Factories;
using LittleParser.DataAccess.Dapper.Repositories;
using LittleParser.DataAccess.Persistance;
using LittleParser.DataAccess.Persistance.Repositories;

namespace LittleParser.DataAccess.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        private IApacheLogRepository _apacheLogRepository;

        public UnitOfWork(IDbConnectionFactory connection)
        {
            _connection = connection.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public IApacheLogRepository ApacheLogRepository => _apacheLogRepository ?? (_apacheLogRepository = new ApacheLogRepository(_transaction));

        public void Save()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception e)
            {
                _transaction.Rollback();

                throw new DataException("Transaction failed", e);
            }
            finally
            {
                _transaction.Dispose();

                _transaction = _connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
        }
    }
}