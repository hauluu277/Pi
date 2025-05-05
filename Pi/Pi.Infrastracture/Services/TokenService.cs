using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Domain;
using Pi.Infrastracture.Repositories.Authentication;
using Pi.Infrastracture.Repositories.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Pi.Domain.Interfaces.Identity;

namespace Pi.Infrastracture.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserOperationsRepository _userOperationsRepository;
        private readonly IRoleOperationsRepository _roleOperationsRepository;
        private readonly UserManager<Users> _userManager;
        private readonly IUsersRepository _usersRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(IConfiguration configuration, IUserOperationsRepository userOperationsRepository, IRoleOperationsRepository roleOperationsRepository, UserManager<Users> userManager, IUsersRepository usersRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _userOperationsRepository = userOperationsRepository;
            _roleOperationsRepository = roleOperationsRepository;
            _userManager = userManager;
            _usersRepository = usersRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<(string accessToken, DateTime expiration, string refreshToken)> RefreshToken(string refeshToken)
        {
            if (string.IsNullOrEmpty(refeshToken)) throw new Exception("refesh token not valid");
            var getToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.Token == refeshToken && !x.IsExpired);
            if (getToken == null) throw new Exception("Expired refresh token");
            var user = await _userManager.FindByIdAsync(getToken.UserId.ToString());
            if (user == null) throw new Exception("User Invalid");
            return await GenerateToken(user);
        }


        public async Task<(string accessToken, DateTime expiration, string refreshToken)> GenerateToken(Users users)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:SecretKey"]));
            var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"]));
            var refreshTokenExpriedDays = DateTime.Now.AddDays(int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"]));


            var jwtId = Guid.NewGuid().ToString();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,users.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,users.Email),
                new Claim(JwtRegisteredClaimNames.Jti,jwtId),
                new Claim("role",""),
            };

            //lấy danh sách thao tác of user
            var listOperation = new List<string>();
            var listOperationOfUser = await _userOperationsRepository.GetOperationByUserIdAsync(users.Id);
            if (listOperationOfUser.Any()) listOperation.AddRange(listOperationOfUser);

            var listOperationOfRole = await _roleOperationsRepository.GetOperationsByUserAsync(users.Id);
            if (listOperationOfRole.Any()) listOperation.AddRange(listOperationOfRole);
            listOperation = listOperation.Distinct().ToList();

            claims.AddRange(listOperation.Select(permission => new Claim("Permission", permission)));


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creads
                );
            var genToken = new JwtSecurityTokenHandler().WriteToken(token);
            var stringToken = GenerateRefreshToken();
            //update infor refresh token of user
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.ExpiryDate = refreshTokenExpriedDays;
            refreshToken.UserId = users.Id;
            refreshToken.JwtId = jwtId;
            refreshToken.Token = stringToken;
            await _refreshTokenRepository.SaveAsync(refreshToken);


            return (genToken, expires, stringToken);

        }

        public ClaimsPrincipal? ReadToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuser"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero// tránh delay time gây expried

                }, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
