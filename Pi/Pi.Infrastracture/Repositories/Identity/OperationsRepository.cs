using Pi.Domain.Entities;
using Pi.Domain.Interfaces.Identity;
using Pi.Infrastracture.Data;
using Pi.Infrastracture.Repositories.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Repositories.Identity
{
    public class OperationsRepository : GenericRepository<Operations>, IOperationsRepository
    {
        public OperationsRepository(PiContext context) : base(context)
        {
        }
    }
}
