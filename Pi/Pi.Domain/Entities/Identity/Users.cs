using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities.Identity
{
    public class Users : IdentityUser<long>, IEntity<long>
    {
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        public long CreateById { get; set; }
        public long UpdateById { get; set; }

        public virtual ICollection<RoleOperations> RoleOperations { get; set; }
    }
}
