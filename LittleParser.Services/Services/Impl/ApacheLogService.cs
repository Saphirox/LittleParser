using System;
using System.Collections.Generic;
using System.Linq;
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

        public ServiceResult<IEnumerable<string>> GetTopHosts(long n, DateTimeOffset? start, DateTimeOffset? end)
        {
            start = start ?? DateTimeOffset.MinValue;
            end = end ?? DateTimeOffset.MaxValue;

            return ReturnResult(() => _uow.ApacheLogRepository.GetTopHosts(start.Value, end.Value, n).Select(c => c.Host));
        }

        public ServiceResult<IEnumerable<string>> GetTopRoutes(long n, DateTimeOffset? start, DateTimeOffset? end)
        {
            start = start ?? DateTimeOffset.MinValue;
            end = end ?? DateTimeOffset.MaxValue;

            return ReturnResult(() => _uow.ApacheLogRepository.GetTopRoutes(start.Value, end.Value, n).Select(c => c.Route));
        }

        public ServiceResult<IEnumerable<ApacheLog>> GetAll(DateTimeOffset? start, DateTimeOffset? end, long offset, long limit)
        {
            start = start ?? DateTimeOffset.MinValue;
            end = end ?? DateTimeOffset.MaxValue;

            return ReturnResult(() =>_uow.ApacheLogRepository.GetAll(start.Value, end.Value, offset, limit));
        }
    }
}