using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Drug
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string Dose { get; set; }
        public string Duration { get; set; }
        public string RootAdmin { get; set; }
        public string Advice { get; set; }
    }
}
