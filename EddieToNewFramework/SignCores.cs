using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class SignCores
    {
        public int Id { get; set; }
        public int SignId { get; set; }
        public int AnimalId { get; set; }

        public virtual Animals Animal { get; set; }
        public virtual Signs Sign { get; set; }
    }
}
