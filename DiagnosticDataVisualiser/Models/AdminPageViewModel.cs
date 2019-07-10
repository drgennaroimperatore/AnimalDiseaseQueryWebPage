using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticDataVisualiser.Models
{

    public class AdminPageViewModel
    {
        public string currentUser { get; set; }
        public List<ApplicationUser> registeredUsers { get; set; }
        
    }
}