using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Likelihoods
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int AnimalId { get; set; }
        public int SignId { get; set; }
        public int DiseaseId { get; set; }

        public virtual Animals Animal { get; set; }
        public virtual Diseases Disease { get; set; }
        public virtual Signs Sign { get; set; }
    }
}
