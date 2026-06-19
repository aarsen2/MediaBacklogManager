using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
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
        private readonly RoleManager<IdentityRole> _roleManager;
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


        public AuthService(UserManager<User> userManager, IConfiguration config, RoleManager<IdentityRole> roleManger)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManger;
        }


        internal async Task<AuthResponse?> login(CredentialDto credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.Username);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(credentials.Username);
                if (user == null)
                {
                    return null;
                }
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, credentials.Password);

            if (!validPassword)
            {
                return null;
            }

            return GenerateAuthResponse(user);



        }

        private AuthResponse GenerateAuthResponse(User user)
        {
            var token = GenerateToken(user);

            //var refreshToken = GenerateRefreshToken(user);

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


        public async Task<AuthResponse> CreateNewAccount(CreateUserDto newUserDto)
        {
            //Checks for user with same username
            Debug.WriteLine("Checking if account with matching username already exists");
            var user = await _userManager.FindByNameAsync(newUserDto.Username);
            var errors = new AuthResponse();

            if (user != null)
            {
                errors.Errors.Add("An account with this username already exists");
            }

            //checks for user with same email
            Debug.WriteLine("Checking if account with matching email already exists");
            user = await _userManager.FindByEmailAsync(newUserDto.Email);
            if (user != null)
            {
                errors.Errors.Add("An account with this email already exists");
            }


            //Creates new user
            user = new User
            {
                UserName = newUserDto.Username,
                DisplayName = newUserDto.DisplayName,
                Email = newUserDto.Email,

                //For Testing. Will be removed once an email confirmation system has been created
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, newUserDto.Password);

            Debug.WriteLine(user);

            //Checks for successful creation
            if (!result.Succeeded)
            {
                errors.Errors.AddRange(result.Errors.Select(e => e.Description).ToList());
            }

            //returns errors if any have been found
            if (errors.Errors.Count > 0)
            {
                return errors;
            }



            //Adds account to the User Role
            if (!await _userManager.IsInRoleAsync(user, "User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }


            //returns auth token
            return GenerateAuthResponse(user);
        }


    }
}
