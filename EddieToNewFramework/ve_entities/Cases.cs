using System;
using System.Collections.Generic;

namespace EddieToNewFramework.ve_entities
{
    public partial class Cases
    {
        public int CaseId { get; set; }
        public DateTime Date { get; set; }
        public string OwnerName { get; set; }
        public string AnimalId { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public double Latt { get; set; }
        public double Longt { get; set; }
        public string Species { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        public int UcDisease { get; set; }
        public int UcDiseaseOther { get; set; }
        public string CommentDisease { get; set; }
        public string CommentTreatment { get; set; }
        public string ImeiId { get; set; }
        public int LabConfirm { get; set; }
        public int LabResult { get; set; }
    }
}
