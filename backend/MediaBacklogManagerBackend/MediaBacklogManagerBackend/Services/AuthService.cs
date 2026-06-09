using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediaBacklogManagerBackend.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private int AccountTimeOut = 3600;
        private int expirationOffset = 1;
        private DateTime expiration
        {
            get
            {
                return DateTime.UtcNow.AddHours(expirationOffset);
            }
        }


        public AuthService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }


        internal async Task<AuthResponse> login(CredentialDto credentials)
        {
            Console.WriteLine(credentials.ToString());
            var user = await _userManager.FindByNameAsync(credentials.Username);
            Console.WriteLine("User");
            Console.WriteLine(user);

            if (user == null)
            {
                return null;
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, credentials.Password);
            Console.WriteLine("Valid Password");
            Console.WriteLine(validPassword);

            if (!validPassword)
            {
                return null;
            }

            
            var token = GenerateToken(user);
            //var refreshToken = GenerateRefreshToken(user);
            Console.WriteLine("token");
            Console.WriteLine(token);
            return new AuthResponse
            {
                Token = token,
                ExpiresIn = AccountTimeOut,
            };

        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
