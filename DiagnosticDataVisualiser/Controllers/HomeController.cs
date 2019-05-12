using DiagnosticDataVisualiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DiagnosticDataVisualiser.Controllers
{
    public class HomeController : Controller
    {

        /*REFERENCE JSON FORMAT
         "cols": [
        {"id":"","label":"Topping","pattern":"","type":"string"},
        {"id":"","label":"Slices","pattern":"","type":"number"}
      ],
  "rows": [
        {"c":[{"v":"Mushrooms","f":null},{"v":3,"f":null}]},
        {"c":[{"v":"Onions","f":null},{"v":1,"f":null}]},
        {"c":[{"v":"Olives","f":null},{"v":1,"f":null}]},
        {"c":[{"v":"Zucchini","f":null},{"v":1,"f":null}]},
        {"c":[{"v":"Pepperoni","f":null},{"v":2,"f":null}]}
      ]
}*/

        public ActionResult Index(Eddie context)
        {
            HomeViewModel model = new HomeViewModel();
            model.SpeciesInEddie = context.species.Select(s => s.speciesName).ToList();
           
            return View(model);
        }

        public JsonResult DrawTestGraph(Eddie context)
        {
            var breeds = context.setCases.GroupBy(m=>m.species).Select(m => new { Name = m.Key, Count = m.Distinct().Count() });
                       

            return Json(breeds);
        }

        public class AnimalDisease
        {
           public string species { get; set; }
           public string userCHDisease { get; set; }
           public int Expr1 { get; set; }

        }

        public JsonResult DrawDiseaseByAnimal(Eddie context, string animalName)
        {
            string an = animalName;
           var query = context.Database.SqlQuery<AnimalDisease>(@"SELECT species, userCHdisease, COUNT(userCHdisease) AS Expr1 FROM caseInfo WHERE(species = @p0) GROUP BY userCHdisease",an).ToList();

            foreach(var q in query)
            {
                Regex rgx = new Regex(":(.*)"); q.userCHDisease = rgx.Replace(q.userCHDisease, "");
            }


            string currentDisease = "";
            List<AnimalDisease> ad = new List<AnimalDisease>();
            int index = -1;
            
            foreach (var q in query )
            {
                AnimalDisease cad = new AnimalDisease();
                if (q.userCHDisease != currentDisease)
                {
                    cad.Expr1 = q.Expr1;
                    cad.userCHDisease = q.userCHDisease;
                    cad.species = q.species;
                    ad.Add(cad);
                    currentDisease = q.userCHDisease;
                    
                    index++;
                }
                else
                {
                    ad[index].Expr1 += q.Expr1;
                }

            }

            
            return Json(ad);
        }

      
    }
}