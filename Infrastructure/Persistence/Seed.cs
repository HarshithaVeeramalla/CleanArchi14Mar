using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedData(WorklogDbContext context, 
            UserManager<AppUser> userManager, IConfiguration configuration)
        {
            var token = new TokenService(configuration);
            await Seed.SeedClientTypes(context);
            await Seed.SeedJobs(context);
            await Seed.SeedCustomers(context);
            await Seed.SeedUsers(context, userManager, token);
            await context.SaveChangesAsync();
        }

        private static async Task SeedJobs(WorklogDbContext context)
        {
            if (context.Jobs.Any()) return;
            var list = new List<Job> {
                new Job(Guid.NewGuid()) {
                    Name = "Repair the kitchen sink",
                    Address = "22 Parramatta Rd, Zetland",
                    Description = "The kitchen sink is clogged and it takes 24 hours for the water to subside.",
                    Phone = "0422897209",
                    Notes = "Ring me when you get here."
                },
                new Job(Guid.NewGuid())
                {
                    Name = "Repair the shower head",
                    Address = "26 Delhi St, Lidcombe",
                    Description = "The shower head is broken.",
                    Phone = "0422893339",
                    Notes = ""
                },

                new Job(Guid.NewGuid())
                {
                    Name = "Repair the garage lock",
                    Address = "27 Churchill Crescent, Crows Nest",
                    Description = "The garage door lock is broken and does not lock.",
                    Phone = "0412397209",
                    Notes = "The door pin is 1234"
                },

                new Job(Guid.NewGuid())
                {
                    Name = "Repair the air con",
                    Address = "27 Churchill Crescent, Crows Nest",
                    Description = "The aircon does not cool the room.",
                    Phone = "0412397112",
                    Notes = "Please ring the doorbell"
                }
            };

            await context.Jobs.AddRangeAsync(list);
        }

        private static async Task SeedCustomers(WorklogDbContext context)
        {
            if (context.Customers.Any()) return;
            
            
            var customer = new Customer(Guid.Parse("517af9b3-fdac-4bb8-99fd-d76f55735152"))
            {
                FirstName = "Stephen Ettiene",
                LastName = "Contoso",
                Address = "1 Microsoft Way, Redmond, WA",
                LoginCode = ""
            };
            await context.Customers.AddAsync(customer);
        }

        private static async Task SeedClientTypes(WorklogDbContext context)
        {

            if (!context.ClientTypes.Any())
            {
                var clientTypes = new List<ClientType>() {
                    new ClientType(Guid.NewGuid()) { Name = "Tradesperson" },
                    new ClientType(Guid.NewGuid()) { Name = "Customer" },
                };

                await context.ClientTypes.AddRangeAsync(clientTypes);
            }
        }

        private static async Task SeedUsers(WorklogDbContext context, 
            UserManager<AppUser> userManager, TokenService token)
        {
            if (userManager.Users.Any()) return;
            
            var users = new List<AppUser>
            {
                new AppUser {
                    CustomerId = Guid.Parse("517af9b3-fdac-4bb8-99fd-d76f55735152"),
                    UserName = "stephencontoso",
                    Bio = "CEO of Contoso Industries",
                    Email = "stephencontoso@gmail.com",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    PhoneNumber = "0477521402", 
                    AccessFailedCount = 3,
                }
            };

            foreach(var u in users) 
            {
                u.PasswordHash = token.CreateToken(u);
                await userManager.CreateAsync(u, "P@ss1Word"); 
            }
        }
    }
}