using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public class AuditEntity<T> : IAudit
    {
        public T Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        public long CreateById { get; set; }
        public long UpdateById { get; set; }
    }
}
