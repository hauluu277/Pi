using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public class Modules : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public short OrderBy { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Operations> Operations { get; set; }
    }
}
