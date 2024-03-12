using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Persistence.Repositories
{
    public class UsersRepositoryIntegrationTests
    {
        private IUsersRepository _actualUsersRepository;
        private IConfigurationBuilder _builder;
        private TokenService _tokenService;
        private List<AppUser> _users;
        private Mock<UserManager<AppUser>> _userManager;


        public UsersRepositoryIntegrationTests()
        {
            _users = new List<AppUser>
            {
                new AppUser {
                    CustomerId = Guid.Parse("d39beb7b-f493-4c13-80af-5d1dce6d5f38"),
                    UserName = "Mark Stevenson",
                    Bio = "Employee",
                    Email = "mstv@bv.com",
                    NormalizedEmail = "mstv@gmail.com",
                },
                new AppUser {
                        CustomerId = Guid.Parse("517af9b3-fdac-4bb8-99fd-d76f55735152"),
                        UserName = "stephencontoso",
                        Bio = "CEO of Contoso Industries",
                        Email = "stephencontoso@gmail.com",
                        NormalizedEmail = "stephencontoso@gmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        PhoneNumber = "0477521402",
                        AccessFailedCount = 3,
                    }
            };

            var store = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<UserManager<AppUser>>(store.Object, null, null,
                null, null, null, null, null, null);
            _builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _tokenService = new TokenService(_builder.Build());

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(_users[1]);

            _userManager.Setup(u => u.CheckPasswordAsync(It.IsAny<AppUser>(), "P@ss1Word"))
                .Returns(Task.FromResult(true));
            
            _userManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), "P@ss1Word"))
                .ReturnsAsync(IdentityResult.Success);
        }

        [Fact]
        public async Task SignIn_UserExistsWhenCalled_ReturnsUserProfile()
        {
            _actualUsersRepository = new UsersRepository(_userManager.Object, _tokenService, _builder);

            var expected = "stephencontoso@gmail.com";
            var actual = await _actualUsersRepository
                .SignIn("stephencontoso@gmail.com", "P@ss1Word");

            Assert.Equal(expected, actual.Email);
        }

        [Fact]
        public async Task CreateUser_WhenCalled_CreatesUserProfile()
        {
            _actualUsersRepository = new UsersRepository(_userManager.Object, _tokenService,
                _builder);

            var newUser = new NewUserDTO
            {
                FirstName = "Marco Rettiene",
                LastName = "Contoso",
                Address = "1 Microsoft Way, Redmond, WA",
                UserName = "marco",
                Email = "marcorettienecontoso@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumber = "0566742134",
                AccessFailedCount = 3
            };

            var expected = 1;
            var actual = await _actualUsersRepository.CreateUser(newUser);

            Assert.Equal(expected, actual);
        }

    }
}