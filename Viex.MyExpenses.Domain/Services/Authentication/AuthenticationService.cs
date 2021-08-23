using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Contexts.Session;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services.Users;

namespace Viex.MyExpenses.Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<OAuthResponse> SignIn(SignInModel model);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUserService _users;

        public AuthenticationService(IConfiguration config, IUserService users)
        {
            _config = config;
            _users = users;
        }

        public async Task<OAuthResponse> SignIn(SignInModel model)
        {
            if (model.GrantType != "client_credentials")
                throw new InvalidGrantTypeException();

            var user = await _users.Authenticate(new AuthenticateModel
            {
                Email = model.Email,
                Password = model.Password,
            });

            return await CreateOAuthResponse(user);
        }

        private async Task<OAuthResponse> CreateOAuthResponse(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessTokenExpirationMinutes = Convert.ToInt32(_config["Authentication:AccessTokenExpirationMinutes"]);
            var refreshTokenExpirationDays = Convert.ToInt32(_config["Authentication:RefreshTokenExpirationDays"]);

            var token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                expires: DateTime.Now.AddMinutes(accessTokenExpirationMinutes),
                claims: GetClaims(user),
                signingCredentials: credentials
            );

            var refreshToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                expires: DateTime.Now.AddDays(refreshTokenExpirationDays),
                claims: new List<Claim> { new Claim(JwtRegisteredClaimNames.Email, user.Email) },
                signingCredentials: credentials
            );

            return new OAuthResponse
            {
                Access_token = await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token)),
                Expires_in = accessTokenExpirationMinutes * 60,
                Token_type = "Bearer",
                Refresh_token = await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(refreshToken)),
            };
        }

        private IEnumerable<Claim> GetClaims(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                
                // Custom claims
                new Claim(CustomClaims.UserId, user.UserId.ToString(), ClaimValueTypes.String),
            };

            return claims;
        }
    }
}
