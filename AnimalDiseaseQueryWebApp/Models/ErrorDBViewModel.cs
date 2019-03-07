using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalDiseaseQueryWebApp.Models
{
    public class ErrorDBViewModel
    {
        public ErrorDBViewModel(string m)
        {
            Message = m;
        }
        public String Message { get; set; }
    }
}