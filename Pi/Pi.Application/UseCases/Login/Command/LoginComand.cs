using MediatR;
using Pi.Application.UseCases.Login.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.UseCases.Login.Command
{
    public class LoginComand : IRequest<LoginResult?>
    {
        public LoginRequest Request { get; set; }
    }
}
