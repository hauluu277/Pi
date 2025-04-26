using MediatR;
using Microsoft.AspNetCore.Identity;
using Pi.Application.UseCases.Login.Dto;
using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.UseCases.Login.Command
{
    public class LoginCommandHandler : IRequestHandler<LoginComand, LoginResult>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Users> _userManager;


        public LoginCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<LoginResult> Handle(LoginComand request, CancellationToken cancellationToken)
        {
            var repoUser = _unitOfWork.GetRepository<IUserRepository>();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if(isPasswordValid)
                {
                    var accessToken = _tokenService.GenerateToken(user);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    await _re
                }
                else
                {

                }
            }

            throw new NotImplementedException();
        }
    }
}
