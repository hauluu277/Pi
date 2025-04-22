using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public class Operations : BaseEntity<long>
    {
        public long ModuleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public short OrderBy { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public virtual Modules Modules { get; set; }

        public virtual ICollection<RoleOperations> RoleOperations { get; set; } 

    }
}
