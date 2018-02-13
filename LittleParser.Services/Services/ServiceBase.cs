using System;
using System.Linq.Expressions;
using LittleParser.Common;

namespace LittleParser.Services.Services
{
    /// <summary>
    /// Helper for transport state and result between Web layer and Business logic layer
    /// </summary>
    public abstract class ServiceBase
    {
        protected ServiceResult<TEntity> ReturnResult<TEntity>(Func<TEntity> expression) where TEntity : class
        {
            var result = new ServiceResult<TEntity>();

            try
            {
                result.Result = expression();
                result.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Status = ResultStatus.ServerError;
            }
            
            return result;
        }

        protected ServiceResult ReturnResult(Action expression)
        {
            var result = new ServiceResult();

            try
            {
                expression();
                result.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Status = ResultStatus.ServerError;
            }

            return result;
        }
    }
}