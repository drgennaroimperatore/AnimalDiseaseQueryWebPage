using System;
using System.Collections.Generic;

namespace EddieToNewFramework.ve_entities
{
    public partial class AppDiseaseRank
    {
        public int Pk { get; set; }
        public int CaseId { get; set; }
        public int Rank { get; set; }
        public int DiseaseId { get; set; }
        public double Percentage { get; set; }
    }
}
