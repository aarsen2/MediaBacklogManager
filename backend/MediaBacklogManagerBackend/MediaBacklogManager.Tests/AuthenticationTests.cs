using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBacklogManager.Tests
{
    public class AuthenticationTests
    {
        private readonly ServiceProvider DependencyProvidor;
        private readonly AuthService AuthService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public AuthenticationTests()
        {
            DependencyProvidor = TestDIBuilder.Build();

            AuthService = DependencyProvidor.GetRequiredService<AuthService>();
            _userManager = DependencyProvidor.GetRequiredService<UserManager<User>>();
            RoleManager = DependencyProvidor.GetRequiredService<RoleManager<IdentityRole>>();
        }


        private async Task InitialSetup()
        {

            //sets up basic things like a test user and a user role

            //creates a base User Role
            await RoleManager.CreateAsync(new IdentityRole("User"));

            var newUser = new CreateUserDto
            {
                Username = "test",
                DisplayName = "test user",
                Email = "testuser@test.com",
                Password = "Password123!"
            };

            await AuthService.CreateNewAccount(newUser);
        }

        [Fact]
        public async Task RegisterUser()
        {

            await InitialSetup();

            //Test User Creation
            var newUser = new CreateUserDto
            {
                Username = "testUser",
                DisplayName = "User Test",
                Email = "test@test.com",
                Password = "Password123!"
            };
            //returns an auth response
            var result = await AuthService.CreateNewAccount(newUser);
            //auth response should not be null
            Assert.NotNull(result);
            //token should not be null
            Assert.NotNull(result.Token);
            //should have no errors
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task BlankLogin()
        {
            await InitialSetup();
            var credentials = new CredentialDto
            {
                Password = "",
                Username = "",
            };

            var results = await AuthService.Login(credentials);
            //Should Return null
            Assert.Null(results);
        }

        [Fact]
        public async Task DuplicateUsername()
        {
            await InitialSetup();

            //Test User Creation
            var newUser = new CreateUserDto
            {
                Username = "test",
                DisplayName = "test user2",
                Email = "testuser2@test.com",
                Password = "Password123!"
            };
            //returns an auth response
            var result = await AuthService.CreateNewAccount(newUser);

            //auth response should not be null
            Assert.NotNull(result);

            //auth response should have errors.
            Assert.NotEmpty(result.Errors);


            //Token should be null
            Assert.Null(result.Token);

            //Should contain the username error
            Assert.Contains(result.Errors, e => e.Contains("An account with this username already exists"));
        }
        [Fact]
        public async Task DuplicateEmail()
        {
            await InitialSetup();
            //Test User Creation
            var newUser = new CreateUserDto
            {
                Username = "testUser2",
                DisplayName = "User Test2",
                Email = "testuser@test.com",
                Password = "Password123!"
            };
            //returns an auth response
            var result = await AuthService.CreateNewAccount(newUser);

            //auth response should not be null
            Assert.NotNull(result);

            //auth response should have errors.
            Assert.NotEmpty(result.Errors);

            //Token should be null
            Assert.Null(result.Token);

            //Should contain the email error
            Assert.Contains(result.Errors, e => e.Contains("An account with this email already exists"));
        }
        [Fact]
        public async Task BlankPassword()
        {
            await InitialSetup();
            //Test User Creation
            var newUser = new CreateUserDto
            {
                Username = "testUser",
                DisplayName = "User Test",
                Email = "test@test.com",
                Password = ""
            };
            //returns an auth response
            var result = await AuthService.CreateNewAccount(newUser);

            //auth response should not be null
            Assert.NotNull(result);

            //Token should be null
            Assert.Null(result.Token);

            //auth response should have errors.
            Assert.NotEmpty(result.Errors);

            
        }
        [Fact]
        public async Task BadPassword()
        {
            await InitialSetup();
            //Test User Creation
            var newUser = new CreateUserDto
            {
                Username = "testUser",
                DisplayName = "User Test",
                Email = "test@test.com",
                Password = "badpass"
            };
            //returns an auth response
            var result = await AuthService.CreateNewAccount(newUser);

            //auth response should not be null
            Assert.NotNull(result);

            //Token should be null
            Assert.Null(result.Token);

            //auth response should have errors.
            Assert.NotEmpty(result.Errors);
        }
    }
}
