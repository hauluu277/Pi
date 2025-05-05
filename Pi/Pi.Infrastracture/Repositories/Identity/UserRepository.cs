using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Infrastracture.Data;
using Pi.Infrastracture.Repositories.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Repositories.Identity
{
    public class UserRepository : GenericRepository<Users>, IUsersRepository
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
