using System;
using System.Collections.Generic;
using LittleParser.Common;
using LittleParser.Models.Entities;

namespace LittleParser.Services.Services
{
    public interface IApacheLogService
    {
        ServiceResult<IEnumerable<ApacheLog>> GetTopHosts(long n = 10, DateTimeOffset start = default, DateTimeOffset end = default);
        
        ServiceResult<IEnumerable<ApacheLog>> GetTopRoutes(long n = 10, DateTimeOffset start = default,
            DateTimeOffset end = default);

        ServiceResult<IEnumerable<ApacheLog>> GetAll(DateTimeOffset start = default, DateTimeOffset end = default, long offset = 0, long limit = 10);

        ServiceResult AddRange(IEnumerable<ApacheLog> apacheLogs);
    }
}