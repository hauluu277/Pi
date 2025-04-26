using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.UseCases.Login.Command
{
    public class LoginComand : IRequest<LoginResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
