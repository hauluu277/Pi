using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public class UserOperations : BaseEntity<long>
    {
        [ForeignKey(nameof(Users))]
        public long UserId { get; set; }
        [ForeignKey(nameof(Operations))]
        public long OperationId { get; set; }
    }
}
