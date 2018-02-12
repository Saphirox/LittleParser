using System.Net;
using System.Net.Http;
using System.Web.Http;
using LittleParser.Common;

namespace LittleParser.Web.Controllers
{
    public class ApiControllerBase : ApiController
    {
            protected HttpResponseMessage ReturnResult<TEntity>(ServiceResult<TEntity> serviceResult) where TEntity : class
            {
                return 
                    serviceResult.IsSuccessed ? Request.CreateResponse(HttpStatusCode.OK, serviceResult.Result) :
                        serviceResult.Status == ResultStatus.Error
                            ? Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceResult.ErrorMessage)
                            : serviceResult.Status == ResultStatus.Forbidden
                                ?  Request.CreateErrorResponse(HttpStatusCode.Forbidden, serviceResult.ErrorMessage) 
                                : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, serviceResult.ErrorMessage);
            }

            protected HttpResponseMessage ReturnResult(ServiceResult serviceResult)
            { 
                return 
                serviceResult.IsSuccessed ? Request.CreateResponse(HttpStatusCode.OK) :
                serviceResult.Status == ResultStatus.Error
                    ? Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceResult.ErrorMessage)
                    : serviceResult.Status == ResultStatus.Forbidden
                        ?  Request.CreateErrorResponse(HttpStatusCode.Forbidden, serviceResult.ErrorMessage) 
                        : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, serviceResult.ErrorMessage);
            }

    }
}