
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
             var users = context.AspNetUsers.ToList();
            // var userRoles = context.UserRoles.ToList();

            string rawSQL = @"Select UserName, Name " +
                "From AspNetUsers, AspNetUserRoles, AspNetRoles " +
                "Where AspNetUsers.Id = AspNetUserRoles.UserId AND AspNetRoles.Id = AspNetUserRoles.RoleId ";

            var result = context.Database.SqlQuery<AppUserViewModel>(rawSQL);
            model.registeredUsers = new List<AppUserViewModel>();



            foreach (var u in users)
            {
                AppUserViewModel uvm = new AppUserViewModel();
                uvm.UserName = u.UserName;
                var r = result.Where(x => x.UserName == u.UserName);

                if (r.Count() > 0)
                {
                    //model.registeredUsers.Add(new AppUserViewModel { Name = q.Name, UserName = q.UserName });

                    uvm.Name = r.First().Name;

                }
                else
                {
                    uvm.Name = "Unassigned";
                }

                model.registeredUsers.Add(uvm);
            }


            
                           
            

                  return View(model);
        }
    }
}