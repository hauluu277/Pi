using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Infrastracture.Data;
using Pi.Infrastracture.Repositories.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Repositories.Authentication
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly PiContext _context;

        public RefreshTokenRepository(PiContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {
            return await FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task SaveAsync(RefreshToken token)
        {

            if (token.Id > 0)
            {
                Update(token);
            }
            else
            {
                await AddAsync(token);
            }
        }


    }
}
