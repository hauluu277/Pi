using MediatR;
using Microsoft.AspNetCore.Identity;
using Pi.Application.UseCases.Login.Dto;
using Pi.Domain.Entities.Identity;
using Pi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.UseCases.Login.Command
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResult?>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<Users> _userManager;

        public RefreshTokenCommandHandler(ITokenService tokenService, UserManager<Users> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<LoginResult?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _tokenService.RefreshToken(request.RefreshToken);
            if (!string.IsNullOrEmpty(refreshToken.accessToken) && refreshToken.expiration > DateTime.Now)
            {
                return new LoginResult { AccessToken= refreshToken.accessToken,AccessTokenExpiry=refreshToken.expiration,RefreshToken=_tokenService.GenerateRefreshToken() };

            }
            throw new NotImplementedException("Refresh token invalid or expiration Refresh token");
        }
    }
}
