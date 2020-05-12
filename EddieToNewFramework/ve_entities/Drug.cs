using System;
using System.Collections.Generic;

namespace EddieToNewFramework.ve_entities
{
    public partial class Drug
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public string Dose { get; set; }
        public string Duration { get; set; }
        public string RootAdm { get; set; }
        public string Advice { get; set; }
    }
}
