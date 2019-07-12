using AspNet.Identity.MySQL;
using DiagnosticDataVisualiser.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiagnosticDataVisualiser.Startup))]
namespace DiagnosticDataVisualiser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //roleManager.Delete( roleManager.FindByName("Admin"));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                

               //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "crawford.revie@strath.ac.uk";
                user.Email = "crawford.revie@strath.ac.uk";

                string userPWD = "HealerOfBov1ne$";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }

                 user = new ApplicationUser();

                user.UserName = "gennaro.imperatore@strath.ac.uk";
                user.Email = "gennaro.imperatore@strath.ac.uk";

                 userPWD = "HealerOfBov1ne$";
                
               
                 chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Pending role    
            if (!roleManager.RoleExists("Pending"))
            {
                var role = new IdentityRole(); 
                role.Name = "Pending";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Approved"))
            {
                var role = new IdentityRole();
                role.Name = "Approved";
                roleManager.Create(role);

            }
        }
    }
}
