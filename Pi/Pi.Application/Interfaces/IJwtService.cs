using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Users users);
        ClaimsPrincipal ValidateToken(object token);
    }
}
