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

       public  HashSet<String> animalNames = new HashSet<string>();
       
        //to do other tables
    }

    
}