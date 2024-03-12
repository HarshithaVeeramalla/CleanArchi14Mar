using System.Diagnostics;
using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(UserManager<AppUser> userManager,
            TokenService tokenService, IConfigurationBuilder builder = null,
                IConfiguration configuration = null)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _builder = builder;
            _configuration = configuration;
        }

        public async Task<UserProfileDTO> SignIn(string email, string password)
        {
            try
            {
                var findUser = await _userManager.FindByEmailAsync(email);
                var resPassword = await _userManager.CheckPasswordAsync(findUser, password);

                if (resPassword == true)
                {
                    return new UserProfileDTO
                    {
                        Id = findUser.Id,
                        Email = findUser.Email,
                        UserName = findUser.UserName,
                        Token = _tokenService.CreateToken(findUser)
                    };
                }
                return null;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task<int> CreateUser(NewUserDTO profile)
        {
            try
            {
                using var context = new WorklogDbContext(_builder);
                var token = new TokenService(_configuration);
                if (profile == null) return -1;

                var customer = new Customer(Guid.NewGuid())
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Address = profile.Address,
                };

                context.Customers.Add(customer);
                
                var result = await context.SaveChangesAsync();

                var user = new AppUser
                {
                    CustomerId = customer.Id,
                    UserName = profile.UserName,
                    Email = profile.Email,
                    EmailConfirmed = profile.EmailConfirmed,
                    LockoutEnabled = profile.LockoutEnabled,
                    PhoneNumber = profile.PhoneNumber,
                    AccessFailedCount = profile.AccessFailedCount,
                };

                await _userManager.CreateAsync(user, profile.Password);

                return result;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }

            return 0;
        }

        public IUsersRepository _usersRepository;
        public UserManager<AppUser> _userManager;
        public WorklogDbContext _context { get; }
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationBuilder _builder;
    }
}