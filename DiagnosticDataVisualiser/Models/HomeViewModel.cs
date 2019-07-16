using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticDataVisualiser.Models
{
    public class HomeViewModel
    {
        public List<string> SpeciesInEddie { get; set; } 
        public string BuildVersion { get; set; }
        public bool isAdmin { get; set; }
    }
}