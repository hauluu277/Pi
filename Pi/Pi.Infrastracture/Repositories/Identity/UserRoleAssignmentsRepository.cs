using Pi.Domain.Entities;
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
    public class UserRoleAssignmentsRepository : GenericRepository<UserRoleAssignments>, IUserRoleAssignmentsRepository
    {
        public UserRoleAssignmentsRepository(PiContext context) : base(context)
        {
        }
    }
}
