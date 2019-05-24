using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Signs
    {
        public Signs()
        {
            Likelihoods = new HashSet<Likelihoods>();
            SignCores = new HashSet<SignCores>();
            SignForCases = new HashSet<SignForCases>();
        }

        public int Id { get; set; }
        public int TypeOfValue { get; set; }
        public string Probability { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Likelihoods> Likelihoods { get; set; }
        public virtual ICollection<SignCores> SignCores { get; set; }
        public virtual ICollection<SignForCases> SignForCases { get; set; }
    }
}
