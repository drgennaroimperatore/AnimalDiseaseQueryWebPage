using AnimalDiseaseQueryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace AnimalDiseaseQueryWebApp.Controllers
{
    public class DiagnoseController : Controller
    {
        Dictionary<Animal, List<Sign>> signsForAnimal = new Dictionary<Animal, List<Sign>>();
        
        // GET: Diagnose
        public ActionResult Index(ADDB context, DiagnoseViewModel model)
        {
           
            model.animals = context.Animals.ToList();

            if(context.SignCore.Count()==0)
                LoadSignsMasterList(context); //load the signcore table if the signcore table is empty

            return View(model);
        }

        public void LoadSignsMasterList(ADDB context)
        {
            string extension = ".xlsx";
            string filename = "master_list";
            string path = Server.MapPath(@"~/Files/" + filename + extension);

            try
            {
                Excel.Application app = new Excel.Application();

                Excel.Workbook WB = app.Workbooks.Open(path);

                Excel.Worksheet signsWorkSheet = WB.Worksheets["Signs core"];

                Excel.Range usedCells = signsWorkSheet.UsedRange;
                object[,] valueArray = (object[,])usedCells.get_Value(
                                        XlRangeValueDataType.xlRangeValueDefault);
                int rowsLength = valueArray.GetLength(0);
                int columnsLength = valueArray.GetLength(1);

                for (int c = 2; c <= columnsLength; c++)
                {

                    string name = (string)valueArray[1, c];
                    if (name == null || name.Equals("Comment"))
                        continue;
                    name = name.ToUpper(); // name needs to be uppercase
                    List<int> ids = new List<int>();

                    var animals = context.Animals.Where(n => n.Name.Contains(name)).ToList();

                    foreach (Animal a in animals)
                    {
                       

                        for (int r = 2; r <= rowsLength; r++)
                        {
                            string signName = (string)valueArray[r, 1];
                            if (signName == null)
                                continue;
                            string columnValue = (string)valueArray[r, c];
                            if (columnValue == null )
                                continue;
                            if (columnValue.Equals("X"))
                            {

                               
                                var signList = context.Signs.Where(s => s.Name.Contains(signName.ToUpper())).ToList();
                                if (signList.Count() > 0)
                                {
                                    Sign sign = signList[0];

                                    context.SignCore.Add(new SignCore(a.Id, sign.Id));
                                }

                            }
                        }
                        
                    }
                }

                //close the workbook and the app 
                WB.Close();

                Marshal.ReleaseComObject(WB);
                Marshal.ReleaseComObject(app);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        [HttpPost]
        public ActionResult RenderSignsPartial (ADDB context, int animalID)
        {
            var signcore = context.SignCore.Where(sc => sc.AnimalID == animalID);

            List<Sign> model = new List<Sign>();
            foreach (SignCore sc in signcore)
                model.Add(context.Signs.Find(sc.SignID));

            return PartialView("_SignsList", model);
        }

    }

   
}