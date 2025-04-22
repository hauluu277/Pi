using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities.Identity
{
    public class Roles : IdentityRole<long>, IEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RoleOperations> RoleOperations { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        public long CreateById { get; set; }
        public long UpdateById { get; set; }
    }
}
