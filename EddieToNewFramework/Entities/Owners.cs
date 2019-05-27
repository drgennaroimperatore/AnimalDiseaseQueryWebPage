using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Owners
    {
        public Owners()
        {
            Patients = new HashSet<Patients>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }

        public virtual ICollection<Patients> Patients { get; set; }
    }
}
