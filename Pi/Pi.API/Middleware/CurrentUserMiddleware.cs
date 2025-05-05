using Azure.Core;
using Pi.Domain;

namespace Pi.API.Middleware
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public CurrentUserMiddleware(ITokenService tokenService, RequestDelegate next)
        {
            _tokenService = tokenService;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                var pricipal=_tokenService.ReadToken(token);    
                if(pricipal != null)
                {
                    context.Items["User"]=pricipal;
                }
            }
             await _next(context);
        }
    }
}
