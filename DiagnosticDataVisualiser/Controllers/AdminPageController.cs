using AspNet.Identity.MySQL;
using DiagnosticDataVisualiser.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiagnosticDataVisualiser.Controllers
{
    public class AdminPageController : Controller
    {
        // GET: AdminPage
        public ActionResult Index(/*Eddie context,*/ AdminPageViewModel model)
        {
           // var users = context.Users.ToList();
           // var userRoles = context.UserRoles.ToList();

            model.registeredUsers = new List<AppUserViewModel>();
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            

            /*
            foreach (var u in users)
            {
                var au = new AppUserViewModel();
                au.Email = u.Email;
                var userId = u.Id;
                
                
                model.registeredUsers.Add(au);
                
            }*/


            return View(model);
        }
    }
}