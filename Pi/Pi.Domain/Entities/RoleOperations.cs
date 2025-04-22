using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public class RoleOperations : BaseEntity<long>
    {
        [ForeignKey(nameof(Roles))]
        public long RoleId { get; set; }
        [ForeignKey(nameof(Users))]
        public long OperationId { get; set; }
        public virtual Operations Operations{ get; set; }    
        public virtual Roles Roles { get; set; }    
    }
}
