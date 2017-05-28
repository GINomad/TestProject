namespace TestProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "MasterUser@mail.com",
                    PasswordHash = password,
                    Email = "MasterUser@mail.com",
                });
            userManager.UpdateSecurityStamp("1");
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "MasterAdmin"))
            {
                var role = new IdentityRole { Name = "MasterAdmin" };
                RoleManager.Create(role);
            }
            context.SaveChanges();
            
            userManager.AddToRole("1", "MasterAdmin");

            password = passwordHash.HashPassword("Password@1234");
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "User1@mail.com",
                    PasswordHash = password,
                    Email = "User1@mail.com",
                });
            userManager.UpdateSecurityStamp("2");
            password = passwordHash.HashPassword("Password@12345");
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "User2@mail.com",
                    PasswordHash = password,
                    Email = "User2@mail.com",
                });
            userManager.UpdateSecurityStamp("3");           
        }
    }
}
