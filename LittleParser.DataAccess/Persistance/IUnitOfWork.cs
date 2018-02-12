using System;
using LittleParser.DataAccess.Persistance.Repositories;

namespace LittleParser.DataAccess.Persistance
{
    public interface IUnitOfWork
    {
        IApacheLogRepository ApacheLogRepository { get; }

        void Save();
    }
}