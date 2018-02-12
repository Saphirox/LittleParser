using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using LittleParser.DataAccess.Persistance.Repositories;
using LittleParser.Models;
using LittleParser.Models.Entities;

namespace LittleParser.DataAccess.Dapper.Repositories
{
    public class ApacheLogRepository : RepositoryBase, IApacheLogRepository
    {
        public ApacheLogRepository(IDbTransaction dbTransaction) : base (dbTransaction)
        {}

        private const string GetAllStatement =
            @"SELECT * FROM ApacheLogs 
              WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
              ORDER BY DateTimeOffset ASC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

        private const string AddStatement =
            @"INSERT INTO ApacheLogs(Host, Route, StatusCode, DateTimeOffset, ContentSize, Geolocation, QueryParameters) 
                        VALUES(@Host, @Route, @StatusCode, @DateTimeOffset, @ContentSize, @Geolocation, @QueryParameters);";

        private const string GetTopHostStatement = @"
            SELECT TOP(@N) [Host] FROM ApacheLogs WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
            ORDER BY [DateTimeOffset] DESC";

        private const string GetTopRoutesStatement = @"
            SELECT TOP(@N) [Route] FROM ApacheLogs WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
            ORDER BY [DateTimeOffset] DESC";
        
        public void Add(ApacheLog entity) =>
            Connection.Execute(AddStatement, entity, Transaction);

        public IEnumerable<ApacheLog> GetAll(DateTimeOffset? start, DateTimeOffset? end, long offset, long limit)
        {
            start = start ?? DateTimeOffset.MinValue;
            end = end ?? DateTimeOffset.MaxValue;
            
            var @params = new {Start = start.Value, End = end.Value, Offset = offset, Limit = limit};

            return Connection.Query<ApacheLog>(GetAllStatement, @params, Transaction);
        }

        public IEnumerable<ApacheLog> GetTopHosts(DateTimeOffset? start, DateTimeOffset? end, long n)
        {
            start = start ?? DateTimeOffset.MinValue;
            end = end ?? DateTimeOffset.MaxValue;
            
            var @params = new {Start = start.Value, End = end.Value, N = n};
            
            return Connection.Query<ApacheLog>(GetTopHostStatement, @params, Transaction);
        }

        public IEnumerable<ApacheLog> GetTopRoutes(DateTimeOffset? start, DateTimeOffset? end, long n)
        {
            start = start ?? DateTimeOffset.MinValue;
            end = end ?? DateTimeOffset.MaxValue;
            
            var @params = new {Start = start.Value, End = end.Value, N = n};
            
            return Connection.Query<ApacheLog>(GetTopRoutesStatement, @params, Transaction);
        }
    }
}