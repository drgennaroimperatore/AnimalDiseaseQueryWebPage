using System;
using System.Collections.Generic;

namespace EddieToNewFramework.ve_entities
{
    public partial class Probabilities
    {
        public int ProbabilityId { get; set; }
        public int SpecDisMainId { get; set; }
        public int SymptomId { get; set; }
        public float Probability { get; set; }
    }
}
