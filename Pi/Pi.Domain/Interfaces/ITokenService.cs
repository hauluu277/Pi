using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Users users);
        ClaimsPrincipal? ReadToken(string token);
        string GenerateRefreshToken();
    }
}
