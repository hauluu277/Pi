using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetByUserNameAsync(string userName);
    }
}
