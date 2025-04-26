using Pi.Application.Interfaces;
using Pi.Domain.Entities.Identity;
using System.Security.Claims;

namespace Pi.Application.Service.TokenService
{
    public class JwtService:IJwtService
    {

        string IJwtService.GenerateToken(Users users)
        {
            throw new NotImplementedException();
        }


        ClaimsPrincipal IJwtService.ValidateToken(object token)
        {
            throw new NotImplementedException();
        }
    }
}