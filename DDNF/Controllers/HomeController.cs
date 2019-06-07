using DDNF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDNF.Controllers
{
    public class HomeController : Controller
    {
        #region NEW METHODS
        public Animal GetAnimalBasedOnID(ADDB context, int id)
        {
            return context.Animals.Find(id);
        }

        public String GetAnimalNameFromPatientID(ADDB context, int patientID)
        {
            string rawSql = @"Select DISTINCT Animals.Name from Animals,Patients where Animals.Id = (SELECT Patients.AnimalID FROM Patients WHERE Patients.ID=@p0)";
            var query = context.Database.SqlQuery<String>(rawSql, patientID).First();
            return query;
        }
        #endregion

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

        public ActionResult Index(ADDB context)
        {
            //comment
            HomeViewModel model = new HomeViewModel();
            model.SpeciesInEddie = context.Animals.Select(s => s.Name).Distinct().ToList();
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            model.BuildVersion = version.ToString();

            return View(model);
        }



        public class AnimalCount
        {
            public string species { get; set; }
            public int Expr1 { get; set; }
        }

        public JsonResult DrawTestGraph(ADDB context)
        {
            const string rawSql = "SELECT DISTINCT Animals.Name AS species, COUNT(Cases.PatientID) as Expr1 " +
                "FROM Animals, Patients, Cases" +
                " WHERE Patients.ID = Cases.PatientID AND Patients.AnimalID = Animals.Id " +
                "GROUP by Animals.Name";

            var query = context.Database.SqlQuery<AnimalCount>(rawSql).ToList();




            return Json(query);
        }

        public class AnimalDisease
        {

            public string userCHDisease { get; set; }
            public int Expr1 { get; set; }

        }

        public JsonResult DrawDiseaseByAnimal(ADDB context, string animalName)
        {
            string an = animalName;

            string rawSql = @"SELECT DISTINCT Diseases.Name AS userCHdisease, COUNT(Cases.DiseaseChosenByUserID) as Expr1 FROM Animals, Patients, Cases,Diseases WHERE Diseases.Id= Cases.DiseaseChosenByUserID AND Patients.ID = Cases.PatientID AND Patients.AnimalID = Animals.Id AND Animals.Name= @p0 GROUP by Diseases.Name";

            object[] p = { an };
            var query = context.Database.SqlQuery<AnimalDisease>(rawSql, p).ToList();






            return Json(query);
        }

        public class DiseaseByDate
        {
            public string Species { get; set; }
            public string userChDisease { get; set; }
            public int DCount { get; set; }
            public string Date { get; set; }
        }

        public class HistogramData
        {
            public List<string> Annotations { get; set; }
            public List<List<string>> Arr { get; set; }

        }

        public class CasesByMonth
        {
            public string Name { get; set; }
            public string DCount { get; set; }
        }
        [HttpPost]
        public JsonResult DrawCasesBySpeciesAndYear(ADDB context, string year)
        {

            HistogramData histogramData = new HistogramData();

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            List<string> annotations = new List<string>();
            histogramData.Arr = new List<List<string>>();
            annotations.Add("Species");
            List<string> species = context.Animals.Select(x => x.Name).Distinct().OrderBy(x => x).ToList(); // very important that the order matches in both queries
            annotations.AddRange(species);



            for (int i = 0; i < months.Length; i++)
            {
                List<string> dataRow = new List<string>();
                dataRow.Add(months[i]);
                const string rawSql = "SELECT DISTINCT Animals.Name AS Name, COUNT(Cases.PatientID) AS Dcount" +
                    " FROM Animals, Patients, Cases " +
                    "WHERE Patients.ID = Cases.PatientID AND " +
                    "Patients.AnimalID = Animals.Id " +
                    "AND " +
                    "MONTH(Cases.DateOfCaseObserved) = @p0 " +
                    "AND " +
                    "YEAR(Cases.DateOfCaseObserved) = @p1 " +
                    "GROUP BY Animals.Name " +
                    "ORDER BY Animals.Name";


                object[] p = { i + 1, year };
                var query = context.Database.SqlQuery<CasesByMonth>(rawSql, p).ToList();

                if (query.Count == 0)
                {
                    foreach (var s in species) // if we have no results for this month set all results to 0
                    {
                        dataRow.Add("0");
                    }
                }
                else
                {

                    foreach (CasesByMonth cbm in query) // if we have results for the month populate with real data
                    {
                        dataRow.Add(cbm.DCount.ToString());
                    }

                    //temporary fix until i figure out the query for the right join
                    //if there is a missing species find out which one it is and add a 0 count at the appropriate index position
                    if (species.Count > query.Count)
                    {
                        List<string> speciesInQuery = query.Select(x => x.Name).ToList();
                        List<string> missingElements = species.Except(speciesInQuery).ToList();

                        foreach (string missing in missingElements)
                        {
                            int indexOfInsertion = species.IndexOf(missing) + 1; // insert with an offset of one as the first element is the month
                            dataRow.Insert(indexOfInsertion, "0");

                        }


                    }
                }


                dataRow.Add(" "); //add the annotations padding
                histogramData.Arr.Add(dataRow);
            } // for loop
            histogramData.Annotations = annotations;

            return Json(histogramData);
        }

        [HttpPost]
        public JsonResult DrawDiseaseByAnimalAndDate(ADDB context, string animalName, string year)
        {
            string an = animalName;
            HistogramData histogramData = new HistogramData();

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Dictionary<string, List<DiseaseByDate>> result = new Dictionary<string, List<DiseaseByDate>>();

            for (int i = 0; i < months.Length; i++)
            {

                /*const string rawSql =
                    @"SELECT  species, userChdisease, COUNT(*) AS dcount FROM caseInfo " +
                    "WHERE species = @p0 AND date LIKE CONCAT('%',@p1,'/',@p2)" +
                    "group by userChdisease "+
                    "ORDER BY userChdisease";*/

                const string rawSql =
                  @"SELECT Diseases.Name as userCHdisease, COUNT(*) AS Dcount
                    FROM Diseases
                    JOIN Cases ON Cases.DiseaseChosenByUserID= Diseases.Id AND YEAR (Cases.DateOfCaseObserved)=@p0 AND MONTH(Cases.DateOfCaseObserved)=@p1
                    JOIN Patients ON Cases.PatientID =Patients.ID
                    JOIN Animals ON Patients.AnimalID = Animals.Id AND Animals.Name= @p2
                    GROUP BY Diseases.Name";

                object[] p = { year, i + 1, an };

                var currentMonthDiseases = context.Database.SqlQuery<DiseaseByDate>(rawSql, p).ToList();
                result.Add(months[i], currentMonthDiseases);
            }
            histogramData.Arr = new List<List<string>>();
            foreach (var m in months)
            {
                List<string> dataList = new List<string>();
                dataList.Add(m);
                var diseasesForMonth = result[m];
                var diseaseNamesForMonth = diseasesForMonth.Select(diseasebyDate => diseasebyDate.userChDisease.TrimEnd()).ToList();

                if (result[m].Count == 0) // if we have no results for that month just add 0 values
                {
                    foreach (var dn in GetDiseaseNames(context, an))
                    {
                        dataList.Add("0");
                    }
                }
                else // if we do add the results for the disease we have
                {

                    foreach (var dn in GetDiseaseNames(context, an))
                    {
                        if (diseaseNamesForMonth.Contains(dn))
                        {
                            string valueToAdd = diseasesForMonth.Where(x => x.userChDisease.TrimEnd().Equals(dn)).First().DCount.ToString();
                            dataList.Add(valueToAdd);
                        }
                        else
                        {
                            dataList.Add("0");
                        }

                    }



                    //add the data list to the bdimensional
                };
                dataList.Add(" ");//annotation padding
                histogramData.Arr.Add(dataList);
            }

            histogramData.Annotations = GetDiseaseNames(context, an);
            histogramData.Annotations.Insert(0, "Diseases");


            return Json(histogramData);
        }





        public class DiseaseName
        {
            public string userCHDisease { get; set; }
        }

        private List<string> GetDiseaseNames(ADDB context, string an)
        {
            string rawSql = "SELECT Diseases.Name AS userCHDisease " +
                 "FROM Diseases " +
                 "JOIN Cases ON Cases.DiseaseChosenByUserID = Diseases.Id " +
                 "JOIN Patients ON Patients.ID = Cases.PatientID " +
                 "JOIN Animals ON Patients.AnimalID = Animals.ID AND Animals.Name = @p0 " +
                 "GROUP BY Diseases.Name " +
                 "ORDER BY Diseases.Name";

            return context.Database.SqlQuery<DiseaseName>(rawSql, an).Select(x => x.userCHDisease).ToList();


        }
    }
}