using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WeatherApp.Web.Models;

namespace WeatherApp.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeatherApp.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeatherApp.Web.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
        }
        bool AddUserAndRole(WeatherApp.Web.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole(Config.AdminRole));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "admin@example.com",
            };
            ir = um.Create(user, "Warszawa2017!");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, Config.AdminRole);
            return ir.Succeeded;
        }
    }
}
