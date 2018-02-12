using LittleSocialNetwork.Common.Definitions.Enums;

namespace LittleSocialNetwork.Common.Definitions.Results
{
    public interface IResult
    {
        bool IsSuccessed { get; }
        string ErrorMessage { get; set; }
        ResultStatus Status { get; set; }
    }

    public static class ResultExtensions
    {
        public static void UpdateFrom(this IResult result, IResult source)
        {
            result.Status = source.Status;
            result.ErrorMessage = source.ErrorMessage;
        }
    } 
}