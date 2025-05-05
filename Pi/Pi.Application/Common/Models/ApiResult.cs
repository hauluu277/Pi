using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.Common.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public ApiResult() { }
        public ApiResult(bool isSuccess, string message = null, List<string> errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors ?? new List<string>();
        }
        public static ApiResult Success(string messsage)
        {
            return new ApiResult(true, messsage);
        }

        public static ApiResult Failure(string messsage, List<string> errors = null)
        {
            return new ApiResult(false, messsage, errors);
        }
    }
    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
        public ApiResult() { }
        public ApiResult(bool isSuccess, string message = null, List<string> errors = null, T data = default) : base(isSuccess, message, errors)
        {
            Data = data;
        }

        public static ApiResult<T> Success(T data, string messgae = null)
        {
            return new ApiResult<T>(true, messgae, null, data);
        }

        public static ApiResult<T> Failure(string message, List<string> errors = null)
        {
            return new ApiResult<T>(false, message, errors, default);
        }
    }
}
