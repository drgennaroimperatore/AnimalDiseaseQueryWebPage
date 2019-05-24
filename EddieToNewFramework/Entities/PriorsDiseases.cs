using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class PriorsDiseases
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int AnimalId { get; set; }
        public string Probability { get; set; }

        public virtual Animals Animal { get; set; }
        public virtual Diseases Disease { get; set; }
    }
}
