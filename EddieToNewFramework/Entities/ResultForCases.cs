using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class ResultForCases
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int CaseId { get; set; }
        public float PredictedLikelihoodOfDisease { get; set; }

        public virtual Cases Case { get; set; }
        public virtual Diseases Disease { get; set; }
    }
}
