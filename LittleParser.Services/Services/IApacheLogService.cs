using System;
using System.Collections.Generic;
using LittleParser.Common;
using LittleParser.Models.Entities;

namespace LittleParser.Services.Services
{
    /// <summary>
    /// Service for interacting with DB ApacheLog entity
    /// </summary>
    public interface IApacheLogService
    {
        ServiceResult<IEnumerable<string>> GetTopHosts(long n, DateTimeOffset? start, DateTimeOffset? end);
        
        ServiceResult<IEnumerable<string>> GetTopRoutes(long n, DateTimeOffset? start, DateTimeOffset? end);

        ServiceResult<IEnumerable<ApacheLog>> GetAll(DateTimeOffset? start, DateTimeOffset? end, long offset = 0, long limit = 10);

        ServiceResult AddRange(IEnumerable<ApacheLog> apacheLogs);
    }
}