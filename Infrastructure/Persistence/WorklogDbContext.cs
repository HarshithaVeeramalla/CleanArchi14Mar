using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class WorklogDbContext : IdentityDbContext<AppUser>
    {
        public WorklogDbContext(IConfigurationBuilder builder)
        {
            _config = builder
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer(_config.GetConnectionString("SqlServerConnection"));
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .HasColumnName("CustomerId");
            modelBuilder.Entity<Tradesperson>()
                .Property(c => c.Id)
                .HasColumnName("TradespersonId");
            modelBuilder.Entity<Job>()
                .Property(c => c.Id)
                .HasColumnName("JobId");
            modelBuilder.Entity<ClientType>()
                .Property(c => c.Id)
                .HasColumnName("ClientTypeId");
            modelBuilder.Entity<AppUser>()
                .Property(c => c.CustomerId)
                .HasColumnName("CustomerId");
            modelBuilder
                .Entity<IdentityUserLogin<string>>()
                .HasKey(key => key.UserId);
        }

        private IConfigurationRoot _config;
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Tradesperson> Tradespersons { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
    }
}