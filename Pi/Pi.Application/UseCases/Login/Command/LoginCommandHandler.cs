using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Pi.Application.UseCases.Login.Dto;
using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.UseCases.Login.Command
{
    public class LoginCommandHandler : IRequestHandler<LoginComand, LoginResult?>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<Users> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResult> Handle(LoginComand request, CancellationToken cancellationToken)
        {
            var repoRefreshToken = _unitOfWork.GetRepository<IRefreshTokenRepository>();

            var user = await _userManager.FindByNameAsync(request.Request.UserName);
            if (user != null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Request.Password);
                if (isPasswordValid)
                {
                    //Tạo accesstoken
                    //var accessTokenExpiredMinutes = DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:AccessTokenExpireMinutes"]));
                    var getAccessToken = await _tokenService.GenerateToken(user);




                    return new LoginResult { AccessToken = getAccessToken.accessToken, RefreshToken = getAccessToken.refreshToken, AccessTokenExpiry = getAccessToken.expiration };
                }
            }

            throw new NotImplementedException("Tài khoản hoặc mật khẩu không hợp lệ");
        }
    }
}
