using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class DiseaseRank
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int Rank { get; set; }
        public string DiseaseName { get; set; }
        public string Percentage { get; set; }
    }
}
