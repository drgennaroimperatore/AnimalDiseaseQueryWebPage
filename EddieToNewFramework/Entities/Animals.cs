using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Animals
    {
        public Animals()
        {
            Likelihoods = new HashSet<Likelihoods>();
            Patients = new HashSet<Patients>();
            PriorsDiseases = new HashSet<PriorsDiseases>();
            SignCores = new HashSet<SignCores>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }

        public virtual ICollection<Likelihoods> Likelihoods { get; set; }
        public virtual ICollection<Patients> Patients { get; set; }
        public virtual ICollection<PriorsDiseases> PriorsDiseases { get; set; }
        public virtual ICollection<SignCores> SignCores { get; set; }
    }
}
