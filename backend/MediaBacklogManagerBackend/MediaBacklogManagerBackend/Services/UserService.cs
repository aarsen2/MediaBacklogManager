using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MediaBacklogManagerBackend.Services
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserService(AppDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ReadUserDto?> GetCurrentUser(ClaimsPrincipal userPrincipal)
        {

            var user = await _userManager.GetUserAsync(userPrincipal);

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new ReadUserDto
            {
                Id = user.Id,
                Username = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Roles = roles.ToList()
            };
        }


        public async Task<string> GetCurrentUserId(ClaimsPrincipal userPrincipal)
        {

            var user = await _userManager.GetUserAsync(userPrincipal);

            if (user == null)
                return "-1";

            var roles = await _userManager.GetRolesAsync(user);

            return user.Id ?? "-1";
        }

        internal async Task<User> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}


