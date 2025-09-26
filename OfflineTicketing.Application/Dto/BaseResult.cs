using System.Net;

namespace OfflineTicketing.Application.Dto
{
    public class BaseResult<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        public HttpStatusCode Code { get; }

        private BaseResult(bool isSuccess, T value, string error, HttpStatusCode code)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            Code = code;
        }

        public static BaseResult<T> Success(T value) => new BaseResult<T>(true, value, string.Empty, HttpStatusCode.OK);
        public static BaseResult<T> Failure(string error, HttpStatusCode code = HttpStatusCode.BadRequest) => new BaseResult<T>(false, default(T), error, code);
    }
}
