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
        public string FirstName { get; set; }
        public string SetCase { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual ICollection<Patients> Patients { get; set; }
    }
}
