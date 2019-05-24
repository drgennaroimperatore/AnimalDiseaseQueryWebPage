using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AnimalDiseaseQueryWebApp.Models
{
    public partial class ADDB : DbContext
    {
        public ADDB()
            : base("name=ADDB")
        {
        }


        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Sign> Signs { get; set; }
        public virtual DbSet<PriorsDiseases> PriorsDiseases { get; set; }
        public virtual DbSet<Likelihood> Likelihoods { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<SignCore> SignCore { get; set; }

        /*cases data extensions*/
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<SignForCase> SignsForCases {get; set;}
        public virtual DbSet<ResultForCase> ResultForCases { get; set; }

        public virtual DbSet<SuspectCase> SuspectCases { get; set; }
    }

    public partial class Animal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animal()
        {
            this.Likelihoods = new HashSet<Likelihood>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }

        
        public virtual ICollection<Likelihood> Likelihoods { get; set; }

        
    }

    public partial class Disease
    {
        
        public Disease()
        {
            this.Likelihoods = new HashSet<Likelihood>();
            this.Treatments = new HashSet<Treatment>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        
        


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likelihood> Likelihoods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Treatment> Treatments { get; set; }

        
    }

    public partial class PriorsDiseases
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Disease")]
        public int DiseaseID { get; set; }
        [ForeignKey("Animal")]
        public int AnimalID { get; set; } 
       
        
        public string Probability { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual Disease Disease { get; set; }
    }


    public partial class Likelihood
    {

        public int Id { get; set; }
        public string Value { get; set; }
        public int AnimalId { get; set; }
        public int SignId { get; set; }
        public int DiseaseId { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual Sign Sign { get; set; }
        public virtual Disease Disease { get; set; }
    }
   
    public partial class Sign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sign()
        {
            this.Likelihoods = new HashSet<Likelihood>();
        }

        public int Id { get; set; }
        public SignTypes Type_of_Value { get; set; }
        public string Probability { get; set; }
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likelihood> Likelihoods { get; set; }
    }

    public enum SignTypes : int
    {
        OBSERVATIONAL = 0,
        NUMERICAL = 1
    }

    public partial class Treatment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Treatment()
        {
            this.Diseases = new HashSet<Disease>();
        }

        public int Id { get; set; }
        public string Info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disease> Diseases { get; set; }
    }

    public class SignCore
    {
        public SignCore() { }

        public SignCore(int aid, int sid)
        {
            SignID = sid;
            AnimalID = aid;
        }

        [Key]
        public int ID { get; set; }
        public int SignID { get; set; }
        public int AnimalID { get; set; }

        public virtual Sign Sign { get; set; }
        public virtual Animal Animal { get; set; }
    }

    /*Tables required for the logging/importing of cases*/

    public class Owner
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SetCase { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
    }

    public class Patient
    {
        [Key]
        public int ID { get; set; }
        public int AnimalID { get; set; }
        public int OwnerID { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual Owner Owner { get; set; }
    }


    public class Case
    {
        [Key]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public DateTime DateOfCaseObserved { get; set;}
        public DateTime DateOfCaseLogged { get; set;}
        public string Location { get; set; }
        public int DiseaseChosenByUserID { get; set; }
        public int RankOfDiseaseChosenByUser { get; set;}
        public float LikelihoodOfDiseaseChosenByUser { get; set;}
        public int DiseasePredictedByAppID { get; set; }
        public float LikelihoodOfDiseasePredictedByApp { get; set;}
        public int TreatmentChosenByUserID { get; set; }
        public string Comments { get; set; }

        //info relevant to copying old cases
        public string OriginDBName { get; set; }
        public string OriginTableName { get; set; }
        public int OriginID { get; set; }

        //version info so we can trace which app was used
        public string ApplicationVersion { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Disease DiseaseChosenByUser { get; set; }
        public virtual Disease DiseasePredictedByApp { get; set; }
        public virtual Treatment TreatmentChosenByUser { get; set; }

    }

    public class SuspectCase
    {
        [Key]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public DateTime DateOfCaseObserved { get; set; }
        public DateTime DateOfCaseLogged { get; set; }
        public string Location { get; set; }
        public int DiseaseChosenByUserID { get; set; }
        public int RankOfDiseaseChosenByUser { get; set; }
        public float LikelihoodOfDiseaseChosenByUser { get; set; }
        public int DiseasePredictedByAppID { get; set; }
        public float LikelihoodOfDiseasePredictedByApp { get; set; }
        public int TreatmentChosenByUserID { get; set; }
        public string Comments { get; set; }

        //info relevant to copying old cases
        public string OriginDBName { get; set; }
        public string OriginTableName { get; set; }
        public int OriginID { get; set; }

        //version info so we can trace which app was used
        public string ApplicationVersion { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Disease DiseaseChosenByUser { get; set; }
        public virtual Disease DiseasePredictedByApp { get; set; }
        public virtual Treatment TreatmentChosenByUser { get; set; }

    }

    public class SignForCase
    {
        [Key]
        public int ID { get; set; }
        public int SignID { get; set; }

        public virtual Sign Sign { get; set; }
    }

    public class ResultForCase
    {
        [Key]
        public int ID { get; set; }
        public int DiseaseID { get; set; }
        public float PredictedLikelihoodOfDisease { get; set; }

        public virtual Disease Disease { get; set; }

        //Do we include this??
    }


}



