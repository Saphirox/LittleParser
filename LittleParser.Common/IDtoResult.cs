using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleSocialNetwork.Common.Definitions.Results
{
    public interface IDtoResult<TDto> : IResult where TDto: class
    {
        TDto Result { get; set; }
    }

    public static class DtoResultExtensions
    {
        public static void UpdateFrom<TSource, TDest>(this IDtoResult<TDest> result, IDtoResult<TSource> source, Func<TSource, TDest> transformer) where TDest : class where TSource : class
        {
            result.Status = source.Status;
            result.ErrorMessage = source.ErrorMessage;
            result.Result = transformer(source.Result);
        }

        public static void UpdateFrom<TSource, TDest>(this IDtoResult<IEnumerable<TDest>> result, IDtoResult<IEnumerable<TSource>> source, Func<TSource, TDest> transformer)
        {
            result.Status = source.Status;
            result.ErrorMessage = source.ErrorMessage;
            result.Result = source.Result.Select(transformer);
        }
    }
}