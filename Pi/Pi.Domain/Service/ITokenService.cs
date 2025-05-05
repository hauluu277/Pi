using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain
{
    public interface ITokenService
    {
        Task<(string accessToken, DateTime expiration,string refreshToken)> GenerateToken(Users users);
        ClaimsPrincipal? ReadToken(string token);
        string GenerateRefreshToken();
        Task<(string accessToken, DateTime expiration,string refreshToken)> RefreshToken(string refreshToken);
    }
}
