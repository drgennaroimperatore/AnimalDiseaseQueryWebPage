using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimalDiseaseQueryWebApp.Models
{
    public class EditDBViewModel
    {
        public List<Animal> animals;
        public List<Disease> diseases;
        public List<Sign> signs;
        public List<Likelihood> likelihoods;
        public List<PriorsDiseases> priorsDiseases;

        public HashSet<String> animalNames = new HashSet<string>();
        public string logMessage = "";
        

        public List<SignTypes> TypesOfValue = Enum.GetValues(typeof(SignTypes)).Cast<SignTypes>().ToList();

        public List<PriorsDiseases> GetAllPriorsForAnimal (Animal animal)
        {

            return priorsDiseases.Where(m => m.AnimalID == animal.Id).ToList();
        }

        public string GetProbabilityForDisease(Disease d, Animal a)
        {
            return priorsDiseases.Where(m => m.DiseaseID == d.Id && m.AnimalID== a.Id).ToList()[0].Probability;
        }
        
        //to do other tables

      
    }

    public enum Observational_Values {PRESENT, NOT_PRESENT, NOT_OBSERVED};

    
}