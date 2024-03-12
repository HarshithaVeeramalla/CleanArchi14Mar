using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Infrastructure.Identity;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
            IConfiguration config)
        {
            var configuration = config.GetSection("TokenKey");
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Value));
           
            services.AddDbContext<WorklogDbContext>(ServiceLifetime.Scoped);
            services.AddTransient<IJobsRepository, JobsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IConfigurationBuilder, ConfigurationBuilder>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                });
            services.AddScoped<TokenService>();
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<WorklogDbContext>();

            return services;
        }
    }
}
