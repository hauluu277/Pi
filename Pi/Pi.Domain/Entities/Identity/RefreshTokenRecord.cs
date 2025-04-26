using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities.Identity
{
    public class RefreshTokenRecord
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired => ExpiryDate <= DateTime.Now;
    }
}
