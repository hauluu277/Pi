using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public class UserRoleAssignments : BaseEntity<long>
    {
        public long IdUser { get; set; }
        public long RoleId { get; set; }

    }
}
