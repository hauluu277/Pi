using Pi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Services
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
