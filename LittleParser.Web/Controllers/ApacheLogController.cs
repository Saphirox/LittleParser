using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using LittleParser.Common;
using LittleParser.Models.Entities;
using LittleParser.Services.Services;

namespace LittleParser.Web.Controllers
{
    [RoutePrefix("api/parser")]
    public class ApacheLogController : ApiControllerBase
    {
        private readonly IApacheLogService _apacheLogService;
        
        public ApacheLogController(IApacheLogService apacheLogService)
        {
            this._apacheLogService = apacheLogService;
        }

        [Route("add-range")]
        public HttpResponseMessage AddRange(IEnumerable<ApacheLog> apacheLogs)
        {
            var result = _apacheLogService.AddRange(apacheLogs);

            return ReturnResult(result);
        }

        [Route("get-all")]
        public HttpResponseMessage GetAll(DateTimeOffset start = default, DateTimeOffset end = default, long offset = 0, long limit = 10)
        {
            var result = _apacheLogService.GetAll(start, end, offset, limit);

            return ReturnResult(result);
        }

        [Route("get-top-hosts")]
        public HttpResponseMessage GetTopHosts(long n = 10, DateTimeOffset start = default, DateTimeOffset end = default)
        {
            var result = _apacheLogService.GetTopHosts(n, start, end);

            return ReturnResult(result);
        }

        [Route("get-top-routes")]
        public HttpResponseMessage GetTopRoutes(long n = 10, DateTimeOffset start = default, DateTimeOffset end = default)
        {
            var result = _apacheLogService.GetTopRoutes(n, start, end);

            return ReturnResult(result);
        }
    }
}