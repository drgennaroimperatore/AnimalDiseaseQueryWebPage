using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Patients
    {
        public Patients()
        {
            Cases = new HashSet<Cases>();
            SuspectCases = new HashSet<SuspectCases>();
        }

        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int OwnerId { get; set; }

        public virtual Animals Animal { get; set; }
        public virtual Owners Owner { get; set; }
        public virtual ICollection<Cases> Cases { get; set; }
        public virtual ICollection<SuspectCases> SuspectCases { get; set; }
    }
}
