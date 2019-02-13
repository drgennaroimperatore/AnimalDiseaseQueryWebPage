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

        public HashSet<String> animalNames = new HashSet<string>();

        public List<SignTypes> TypesOfValue = Enum.GetValues(typeof(SignTypes)).Cast<SignTypes>().ToList();
        
        //to do other tables
    }

    public enum Observational_Values {PRESENT, NOT_PRESENT, NOT_OBSERVED};

    
}