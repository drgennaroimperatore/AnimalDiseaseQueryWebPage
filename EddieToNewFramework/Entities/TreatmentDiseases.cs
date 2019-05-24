using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class TreatmentDiseases
    {
        public int TreatmentId { get; set; }
        public int DiseaseId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Treatments Treatment { get; set; }
    }
}
