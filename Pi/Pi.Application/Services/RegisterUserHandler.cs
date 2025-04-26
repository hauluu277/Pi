using Pi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.Services
{
    public class RegisterUserHandler
    {
        private readonly IEmailService _emailService;

        public RegisterUserHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        //public async Task<Result> Handle(RegisterUserDto dto)
        //{
        //    // ... đăng ký user
        //    await _emailService.SendAsync(dto.Email, "Welcome!", "Cảm ơn đã đăng ký!");
        //    return Result.Success();
        //}
    }
}
