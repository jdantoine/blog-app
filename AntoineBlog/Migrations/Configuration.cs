namespace AntoineBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    internal sealed class Configuration : DbMigrationsConfiguration<AntoineBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AntoineBlog.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
                roleManager.Create(new IdentityRole { Name = "Admin" });

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "jdantoine@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jdantoine@gmail.com",
                    Email = "jdantoine@gmail.com", //Entity Framework assumes username and email are the same in order to have a unique field
                    DisplayName = "Judner Antoine"
                }, "JDBlog!");
            }

            var userId = userManager.FindByEmail("jdantoine@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            //FOR MODERATOR

            if (!context.Roles.Any(r => r.Name == "Moderator"))
                roleManager.Create(new IdentityRole { Name = "Moderator" });

            if (!context.Users.Any(u => u.Email == "moderatora@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderatora@coderfoundry.com",
                    Email = "moderator@coderfoundry.com", //Entity Framework assumes username and email are the same in order to have a unique field
                    DisplayName = "CF Moderator"
                }, "Password-1");
            }

            userId = userManager.FindByEmail("moderatora@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");

            //GUEST ADMIN
            if (!context.Roles.Any(r => r.Name == "GuestAdmin"))
                roleManager.Create(new IdentityRole { Name = "GuestAdmin" });

            if (!context.Users.Any(u => u.Email == "guest@aguest.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "guest@aguest.com",
                    Email = "guest@aguest.com",
                    DisplayName = "Guest Admin"
                }, "Password-1");
            }

            userId = userManager.FindByEmail("guest@aguest.com").Id;
            userManager.AddToRole(userId, "GuestAdmin");


        }
    }
}
