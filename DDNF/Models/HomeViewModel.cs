﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDNF.Models
{
    public class HomeViewModel
    {
        public List<string> SpeciesInEddie { get; set; }
        public int TotalCases { get; set; }
        public string BuildVersion { get; set; }
    }
}