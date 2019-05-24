using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Treatments
    {
        public Treatments()
        {
            Cases = new HashSet<Cases>();
            SuspectCases = new HashSet<SuspectCases>();
            TreatmentDiseases = new HashSet<TreatmentDiseases>();
        }

        public int Id { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Cases> Cases { get; set; }
        public virtual ICollection<SuspectCases> SuspectCases { get; set; }
        public virtual ICollection<TreatmentDiseases> TreatmentDiseases { get; set; }
    }
}
