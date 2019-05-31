﻿using DiagnosticDataVisualiser.Models;
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
            //comment
            HomeViewModel model = new HomeViewModel();
            model.SpeciesInEddie = context.species.Select(s => s.speciesName).ToList();
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            model.BuildVersion = version.ToString();

            return View(model);
        }

        public List<string> GetDiseaseNames(Eddie context)
        {
            return context.diseases.OrderBy(x => x.diseaseName).Select(x => x.diseaseName).ToList();


        }

        public class DiseaseName
        {
            public string userCHDisease { get; set; }
        }

        public List<string> GetDiseaseNames (Eddie context, string species)
        {
            string rawsql = "SELECT userCHdisease " +
                "FROM " +
                "(SELECT species,userCHdisease  " +
                "FROM setCase " +
                "UNION ALL " +
                "SELECT species,userCHdisease " +
                "FROM caseInfo) " +
                "derivedtbl_1 " + 
                "WHERE(species = @p0) " +
                "GROUP BY userCHdisease " +
                "ORDER BY userCHdisease";

            var query = context.Database.SqlQuery<DiseaseName>(rawsql, species);

            HashSet<string> temp = new HashSet<string>();
            

            foreach (var q in query)
            {
                Regex rgx = new Regex(":(.*)");
                temp.Add (rgx.Replace(q.userCHDisease, "").TrimEnd());
            }

            

            return (temp.ToList());

        }

        public class AnimalCount
        {
            public string species { get; set; }
            public int Expr1 { get; set; }
        }

        public JsonResult DrawTestGraph(Eddie context)
        {
            const string rawSql = "SELECT        species, COUNT(species) AS Expr1 " +
                "FROM(SELECT species  " +
                "FROM setCase UNION ALL SELECT species FROM caseInfo) " +
                "derivedtbl_1 " +
                "GROUP BY species";

            var query = context.Database.SqlQuery<AnimalCount>(rawSql).ToList();


            return Json(query);
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
            //const string rawSql = @"SELECT species, userCHdisease, COUNT(userCHdisease) AS Expr1 FROM caseInfo WHERE(species = @p0) GROUP BY userCHdisease";
            const string rawSql = @"SELECT        species, userCHdisease, COUNT(userCHdisease) AS Expr1
                                FROM            (SELECT        species, userCHdisease
                                FROM            setCase
                               UNION ALL
                                SELECT        species, userCHdisease
                                FROM            caseInfo) derivedtbl_1
                                WHERE        (species = @p0)
                                GROUP BY userCHdisease";
            var query = context.Database.SqlQuery<AnimalDisease>(rawSql, an).ToList();

            foreach (var q in query)
            {
                Regex rgx = new Regex(":(.*)"); q.userCHDisease = rgx.Replace(q.userCHDisease, "");
            }


            string currentDisease = "";
            List<AnimalDisease> ad = new List<AnimalDisease>();
            int index = -1;

            foreach (var q in query)
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
        public JsonResult DrawCasesBySpeciesAndYear(Eddie context, string year)
        {

            HistogramData histogramData = new HistogramData();

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            List<string> annotations = new List<string>();
            histogramData.Arr = new List<List<string>>();
            annotations.Add("Species");
            List<string> species = context.species.OrderBy(x => x.speciesName).Select(x => x.speciesName).ToList(); // very important that the order matches in both queries
            annotations.AddRange(species);



            for (int i = 0; i < months.Length; i++)
            {
                List<string> dataRow = new List<string>();
                dataRow.Add(months[i]);
                const string rawSql =
                     @"SELECT
	species.speciesName as Name ,
	COUNT(species) AS Dcount
FROM
	(
	SELECT
		setCase.species,
		DATE
	FROM
		setCase
		WHERE
	date LIKE CONCAT('%', @p0, '/', @p1)
	UNION ALL
SELECT
	caseInfo.species,
	DATE
FROM
	caseInfo
	   WHERE
	date LIKE CONCAT('%', @p0, '/', @p1)
) der
RIGHT JOIN species
ON
	der.species = species.speciesName

GROUP BY
	species.speciesName
ORDER BY
	species.speciesName";

                string month = "";
                if (i < 9)
                {
                    month = "0" + (i + 1).ToString();
                }
                else
                    month = (i + 1).ToString();

                object[] p = { month, year };
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
                }


                dataRow.Add(" "); //add the annotations padding
                histogramData.Arr.Add(dataRow);
            } // for loop
            histogramData.Annotations = annotations;

            return Json(histogramData);
        }

        [HttpPost]
        public JsonResult DrawDiseaseByAnimalAndDate(Eddie context, string animalName, string year)
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
                  @"SELECT  species, userChdisease, COUNT(*) AS dcount FROM 
                                (SELECT        species, userCHdisease, date
                                FROM            setCase
                               UNION ALL
                                SELECT        species, userCHdisease, date
                                FROM            caseInfo) derivedtbl_1 " +
                  "WHERE species = @p0 AND date LIKE CONCAT('%',@p1,'/',@p2)" +
                  "group by userChdisease " +
                  "ORDER BY userChdisease";

                string month = "";
                if (i < 9)
                {
                    month = "0" + (i + 1).ToString();
                }
                else
                    month = (i + 1).ToString();

                object[] p = { an, month, year };
                var query = context.Database.SqlQuery<DiseaseByDate>(rawSql, p).ToList();

                foreach (var q in query)
                {
                    Regex rgx = new Regex(":(.*)"); q.userChDisease = rgx.Replace(q.userChDisease, "");
                }

                string currentDisease = "";
                List<DiseaseByDate> ad = new List<DiseaseByDate>();
                int index = -1;

                foreach (var q in query)
                {
                    DiseaseByDate cad = new DiseaseByDate();
                    if (q.userChDisease != currentDisease)
                    {
                        cad.Date = q.Date;
                        cad.DCount = q.DCount;
                        cad.userChDisease = q.userChDisease;
                        cad.Species = q.Species;
                        ad.Add(cad);
                        currentDisease = q.userChDisease;

                        index++;
                    }
                    else
                    {
                        ad[index].DCount += q.DCount;
                    }

                }
                result.Add(months[i], ad);
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
                    foreach (var dn in GetDiseaseNames(context,an))
                    {
                        dataList.Add("0");
                    }
                }
                else // if we do add the results for the disease we have
                {

                    foreach (var dn in GetDiseaseNames(context,an))
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

            histogramData.Annotations = GetDiseaseNames(context,an);
            histogramData.Annotations.Insert(0, "Diseases");


            return Json(histogramData);
        }


    }


}