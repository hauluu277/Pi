using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities
{
    public interface IAudit
    {
        string CreatedBy { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        string UpdatedBy { get; set; }
        long CreateById { get; set; }
        long UpdateById { get; set; }
    }
}
