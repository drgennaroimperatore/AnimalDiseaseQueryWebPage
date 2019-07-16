using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticDataVisualiser.Models
{

    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }

    public class AdminPageViewModel
    {
        public string currentUser { get; set; }
        public List<AppUserViewModel> registeredUsers { get; set; }

        
    }
}