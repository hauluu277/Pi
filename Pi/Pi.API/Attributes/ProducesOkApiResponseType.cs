using Microsoft.AspNetCore.Mvc;
using Pi.Application.Common.Models;

namespace Pi.API.Attributes
{
    public class ProducesOkApiResponseType<TResponse> : ProducesResponseTypeAttribute
    {
        private ProducesOkApiResponseType(int statusCode) : base(statusCode)
        {
        }
        public ProducesOkApiResponseType() : base(typeof(ApiResult<TResponse>), StatusCodes.Status200OK)
        {

        }
        private ProducesOkApiResponseType(Type type, int statusCode, string contentType, params string[] additionalContentTypes) : base(type, statusCode, contentType, additionalContentTypes)
        {

        }
    }

    public class ProcesOkApiResponseType : ProducesResponseTypeAttribute
    {
        private ProcesOkApiResponseType(int statusCode) : base(statusCode)
        {
        }
        public ProcesOkApiResponseType() : base(typeof(ApiResult), StatusCodes.Status200OK)
        {

        }
        private ProcesOkApiResponseType(Type type, int statusCode, string contentType, params string[] additionalContentTypes) : base(type, statusCode, contentType, additionalContentTypes)
        {

        }
    }
}
