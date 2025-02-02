﻿using System;
using System.Collections.Generic;

namespace LittleParser.Common
{
    
    /// <summary>
    /// Helper for converting result to an appropriv view
    /// </summary>
    public static class ServiceResultConverter
    {
        public static ServiceResult<TModel> ConvertToResult<TEntity, TModel>(
            this ServiceResult<TEntity> source,
            Func<TEntity, TModel> transformator)
            where TEntity : class where TModel : class
        {
            var serviceResult = new ServiceResult<TModel>();

            if (!source.IsSuccessed)
            {
                serviceResult.UpdateFrom(source);
                return serviceResult;
            }

            serviceResult.UpdateFrom(source, transformator);

            return serviceResult;
        }

        public static ServiceResult<IEnumerable<TModel>> ConvertToResult<TEntity, TModel>(
            this ServiceResult<IEnumerable<TEntity>> source,
            Func<TEntity, TModel> transformator)
            where TEntity : class where TModel : class
        {
            var serviceResult = new ServiceResult<IEnumerable<TModel>>();

            if (!source.IsSuccessed)
            {
                serviceResult.UpdateFrom(source);
                return serviceResult;
            }

            serviceResult.UpdateFrom(source, transformator);

            return serviceResult;
        }
    }
}