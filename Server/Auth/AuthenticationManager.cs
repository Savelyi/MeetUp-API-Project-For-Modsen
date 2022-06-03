using Contracts;
using Entities.DTO.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Auth
{
    public class AuthenticationManager : IAuthenticationManager
    {
        readonly UserManager<User> _userManager;
        User _user;
        public AuthenticationManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateToken()
        {

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, _user.UserName),
                    new Claim("Id",_user.Id)
                };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            ClaimsIdentity identity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<bool> ValidateUser(UserForSignInDto user)
        {
            _user = await _userManager.FindByNameAsync(user.UserName);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, user.Password));
        }
    }
}
