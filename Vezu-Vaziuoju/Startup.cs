using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Vezu_Vaziuoju.Models;

[assembly: OwinStartupAttribute(typeof(Vezu_Vaziuoju.Startup))]
namespace Vezu_Vaziuoju
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // Create default User roles and default Admin user account
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup creating Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                string email = "admin@admin.com";
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Name = "Jonas",
                    LastName = "Jonaitis",
                    Birthdate = DateTime.Parse("1990-05-08"),
                };

                string pass = "Admin1?";

                var chkUser = UserManager.Create(user, pass);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    Admin admin = new Admin
                    {
                        Id = user.Id,
                        UserId = user.Id,
                    };
                    context.Admins.Add(admin);
                    context.SaveChanges();

                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Driver role     
            if (!roleManager.RoleExists("Driver"))
            {
                var role = new IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);

            }

            // creating Passenger role     
            if (!roleManager.RoleExists("Passenger"))
            {
                var role = new IdentityRole();
                role.Name = "Passenger";
                roleManager.Create(role);

            }
        }
    }
}
