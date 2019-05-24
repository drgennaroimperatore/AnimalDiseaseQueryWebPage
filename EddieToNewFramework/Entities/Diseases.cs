using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Diseases
    {
        public Diseases()
        {
            CasesDiseaseChosenByUser = new HashSet<Cases>();
            CasesDiseasePredictedByApp = new HashSet<Cases>();
            Likelihoods = new HashSet<Likelihoods>();
            PriorsDiseases = new HashSet<PriorsDiseases>();
            ResultForCases = new HashSet<ResultForCases>();
            SuspectCasesDiseaseChosenByUser = new HashSet<SuspectCases>();
            SuspectCasesDiseasePredictedByApp = new HashSet<SuspectCases>();
            TreatmentDiseases = new HashSet<TreatmentDiseases>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cases> CasesDiseaseChosenByUser { get; set; }
        public virtual ICollection<Cases> CasesDiseasePredictedByApp { get; set; }
        public virtual ICollection<Likelihoods> Likelihoods { get; set; }
        public virtual ICollection<PriorsDiseases> PriorsDiseases { get; set; }
        public virtual ICollection<ResultForCases> ResultForCases { get; set; }
        public virtual ICollection<SuspectCases> SuspectCasesDiseaseChosenByUser { get; set; }
        public virtual ICollection<SuspectCases> SuspectCasesDiseasePredictedByApp { get; set; }
        public virtual ICollection<TreatmentDiseases> TreatmentDiseases { get; set; }
    }
}
