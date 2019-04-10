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
        List<Sign> signsForAnimal = new List<Sign>();

        // GET: Diagnose
        public ActionResult Index(ADDB context, DiagnoseViewModel model)
        {
           
            model.animals = context.Animals.ToList();

            return View(model);
        }

        public void LoadSignsMasterList()
        {
            string extension = ".xlsx";
            string filename = "data";
            string path = Server.MapPath(@"~/Files/" + filename + extension);

            try
            {
                Excel.Application app = new Excel.Application();

                Excel.Workbook WB = app.Workbooks.Open(path);
            }
            catch(Exception e)
            {

            }
        }

        public ActionResult RenderSignsPartial (List <Sign> model)
        {
            return PartialView("", model);
        }

    }

   
}