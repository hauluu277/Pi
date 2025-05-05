using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Configurations.Authentication
{
    public class JwtSettings
    {
        public string IssUser { get; set; }
        public string Audience { get; set; }
        public string Secretkey { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
}
