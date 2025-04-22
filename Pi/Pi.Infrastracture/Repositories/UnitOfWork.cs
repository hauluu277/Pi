using Pi.Domain.Interface;
using Pi.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PiContext _context;


        public UnitOfWork(PiContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
