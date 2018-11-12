using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevAdventCalendarCompetition.Repository.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Test> Test { get; set; }

        public DbSet<TestAnswer> TestAnswer { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var adminUser = new ApplicationUser()
            {
                Id = "1",
                UserName = "devadventcalendar@gmail.com",
                NormalizedUserName = "devadventcalendar@gmail.com",
                Email = "devadventcalendar@gmail.com",
                PasswordHash = "",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true
            };
            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "test123");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "1",
                Name = "Admin"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                UserId = "1",
                RoleId = "1"
            });

            base.OnModelCreating(builder);
        }
    }
}