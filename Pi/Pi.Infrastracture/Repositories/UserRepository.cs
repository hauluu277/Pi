using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        private readonly PiContext _context;
        public UserRepository(PiContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Users> GetByUserNameAsync(string userName)
        {
            return await FirstOrDefaultAsync(x => x.UserName.Equals(userName));
        }
    }
}
