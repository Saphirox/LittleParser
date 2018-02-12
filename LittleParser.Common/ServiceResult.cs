namespace LittleParser.Common
{
    public class ServiceResult : IResult
    {
        public ServiceResult()
        {
            Status = ResultStatus.Error;
        }

        public bool IsSuccessed => Status == ResultStatus.Success;

        public string ErrorMessage { get; set; }
        public ResultStatus Status { get; set; }
    }

    public class ServiceResult<TDto> : ServiceResult, IDtoResult<TDto> where TDto : class
    {
        private TDto _serviceResult;

        public TDto Result
        {
            get => _serviceResult;
            set => _serviceResult = value;
        }
    };
}