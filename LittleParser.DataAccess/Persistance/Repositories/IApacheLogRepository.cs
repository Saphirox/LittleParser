using System;
using System.Collections.Generic;
using LittleParser.Common;
using LittleParser.Models;
using LittleParser.Models.Entities;

namespace LittleParser.DataAccess.Persistance.Repositories
{
    public interface IApacheLogRepository
    {
        void Add(ApacheLog entity);
        
        IEnumerable<ApacheLog> GetAll(DateTimeOffset start, DateTimeOffset end, long offset, long limit);
        
        IEnumerable<ApacheLog> GetTopHosts(DateTimeOffset start, DateTimeOffset end, long n = 10);

        IEnumerable<ApacheLog> GetTopRoutes(DateTimeOffset start, DateTimeOffset end, long n = 10);
    }
}