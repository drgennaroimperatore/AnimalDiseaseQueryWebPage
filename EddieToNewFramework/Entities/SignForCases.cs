using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class SignForCases
    {
        public int Id { get; set; }
        public int SignId { get; set; }
        public int SignPresence { get; set; }

        public virtual Signs Sign { get; set; }
    }
}
