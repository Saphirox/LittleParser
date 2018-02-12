using System;
using System.Collections.Generic;
using LittleParser.Common;
using LittleParser.DataAccess.Persistance;
using LittleParser.Models.Entities;

namespace LittleParser.Services.Services.Impl
{
    public class ApacheLogService : ServiceBase, IApacheLogService
    {
        private readonly IUnitOfWork _uow;
        
        public ApacheLogService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ServiceResult AddRange(IEnumerable<ApacheLog> apacheLogs)
        {
            return ReturnResult(() =>
            {
                foreach (var apacheLog in apacheLogs)
                {
                    _uow.ApacheLogRepository.Add(apacheLog);
                }

                _uow.Save();
            });
        }

        public ServiceResult<IEnumerable<ApacheLog>> GetTopHosts(long n, DateTimeOffset? start, DateTimeOffset? end)
        {
            return ReturnResult(() => _uow.ApacheLogRepository.GetTopHosts(start, end, n));
        }

        public ServiceResult<IEnumerable<ApacheLog>> GetTopRoutes(long n, DateTimeOffset? start, DateTimeOffset? end)
        {
            return ReturnResult(() => _uow.ApacheLogRepository.GetTopRoutes(start, end, n));
        }

        public ServiceResult<IEnumerable<ApacheLog>> GetAll(DateTimeOffset? start, DateTimeOffset? end, long offset, long limit)
        {
            return ReturnResult(() =>_uow.ApacheLogRepository.GetAll(start, end, offset, limit));
        }
    }
}