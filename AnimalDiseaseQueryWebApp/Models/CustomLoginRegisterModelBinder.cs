using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalDiseaseQueryWebApp.Models
{
    public class CustomLoginRegisterModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CustomLoginRegisterViewModel modelToReturn = new CustomLoginRegisterViewModel();
            if (bindingContext.ModelType == typeof(CustomLoginRegisterViewModel))
            {
                //EXAMPLE CODE
              HttpRequestBase request = controllerContext.HttpContext.Request;
                /*
                               int frmStudentId = Convert.ToInt32(request.Form.Get("StudentId").ToString());  
                               string frmAddress = request.Form.Get("Address");  
                               string frmCity = request.Form.Get("City");  
                               string frmState = request.Form.Get("State");      
                           */

                //int loginFormID = Convert.ToInt32(request.Form.Get("loginform").ToString());
                string email = request.Form.Get("login_email");
                string password = request.Form.Get("login_password");
                bool rememberMe = Convert.ToBoolean(request.Form.Get("login_rememberMe"));

                string rEmail = request.Form.Get("register_email");
                string rPassword = request.Form.Get("register_password");
                string rConfirmPassword =request.Form.Get("register_c_password");

                modelToReturn = new CustomLoginRegisterViewModel
                {
                    LoginViewModel = new LoginViewModel
                    {
                        Email = email,
                        Password = password,
                        RememberMe = rememberMe
                    },

                    RegisterViewModel = new RegisterViewModel
                    {
                        Email = rEmail,
                        Password = rPassword,
                        ConfirmPassword = rConfirmPassword
                    }

                    
                };
            }

            return modelToReturn;
        }

       
    }
}