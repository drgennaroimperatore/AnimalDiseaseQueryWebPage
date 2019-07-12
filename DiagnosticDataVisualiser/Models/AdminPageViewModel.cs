using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticDataVisualiser.Models
{

    public class AppUserViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class AdminPageViewModel
    {
        public string currentUser { get; set; }
        public List<AppUserViewModel> registeredUsers { get; set; }

        
    }
}