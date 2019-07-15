
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
        public ActionResult Index(UserManagement context, AdminPageViewModel model)
        {
            // var users = context.Users.ToList();
            // var userRoles = context.UserRoles.ToList();

            var rawSQL = "Select UserName, Name" +
                "From AspNetUsers, AspNetRoles, AspNetUserRoles Where AspNetUsers.Id = AspNetUserRoles.UserId AND AspNetRoles.ID = AspNetUserRoles.RoleId ";

            var result = context.Database.SqlQuery<AppUserViewModel>(rawSQL);
            model.registeredUsers = new List<AppUserViewModel>();

            foreach (var q in result)
            {
                model.registeredUsers.Add(new AppUserViewModel { Name = q.Name, UserName = q.UserName });
            }


            
                           
            

                  return View(model);
        }
    }
}