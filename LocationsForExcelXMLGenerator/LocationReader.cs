using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;

namespace LocationsForExcelXMLGenerator
{
    class LocationReader
    {
        const int ID_COLUMN = 1;
        const int TYPE_COLUMN = 3;
        const int LABEL_COLUMN = 2;
        const int PARENT_COLUMN = 4;

        const string ID_RANGE = "A";
        const string TYPE_RANGE = "C";

        Excel.Workbook WB = null;
        Excel.Worksheet worksheet = null;
        Excel.Range usedCells = null;
        static Excel.Application app = new Excel.Application();

        public void generateXML()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\eth.xlsx");


            WB = app.Workbooks.Open(path);
            worksheet = WB.Worksheets[1];
            usedCells = worksheet.UsedRange;
            Console.WriteLine("Used Rows: " + usedCells.Count);
            // readSheet(); // use default values
            read();
        }


        private void readSheet(string label = "Region", double parentID = -99, int searchLimit = int.MaxValue)
        {
            Console.WriteLine(parentID);
           

            try
            {

                Excel.Range test = usedCells.Columns[4];
                Excel.Range currentRegion = test.Find(parentID, Missing.Value,
      Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole,
      Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, true,
      Missing.Value, Missing.Value);
            

                Excel.Range firstFind = null;

                int cnt = 0;

                while (currentRegion != null)
                {
                    //// Keep track of the first range you find. 
                    if (firstFind == null)
                    {
                        firstFind = currentRegion;
                    }

                    // If you didn't move to a new range, you are done.
                    else if (currentRegion.get_Address(Excel.XlReferenceStyle.xlA1)
                          == firstFind.get_Address(Excel.XlReferenceStyle.xlA1))
                    {
                        break;
                    }
                    double id = ((worksheet.Cells[currentRegion.Row, ID_COLUMN] as Excel.Range).Value);
                    string lab = ((worksheet.Cells[currentRegion.Row, LABEL_COLUMN] as Excel.Range).Value);
                    double pID = ((worksheet.Cells[currentRegion.Row, PARENT_COLUMN] as Excel.Range).Value);
                    string type = ((worksheet.Cells[currentRegion.Row, TYPE_COLUMN] as Excel.Range).Value);
                    Console.WriteLine("ID: " + id + " " + "parentID:" + " " + pID);
                   
                    LocationData locationData = new LocationData(id, label, "Reg", pID);
                    Console.WriteLine(lab);

                    
                        currentRegion = test.FindNext(currentRegion);
                    

                    
                  readSheet("Zone", id, usedCells.Count);
                    




                    /* if(type.Equals("Zone"))
                     {
                         readSheet("Woreda", id);
                     }*/
                   
                    Console.WriteLine("Loop: " + cnt++);
                }
            
            

                   
                       

                
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                

            }
           
        }

        private Dictionary<LocationData, List<LocationData>> read()
        {
            Dictionary<LocationData, List<LocationData>> graph = new Dictionary<LocationData, List<LocationData>>();
            object[,] valueArray = (object[,])usedCells.get_Value(
                                       XlRangeValueDataType.xlRangeValueDefault);
            int rowsLength = valueArray.GetLength(0);
            int columnsLength = valueArray.GetLength(1);
          

            for (int r = 2; r <= rowsLength; r++)
            {
                double id =  (double)valueArray[r, ID_COLUMN];
                string label = (string)valueArray[r, LABEL_COLUMN];
                string type = (string)valueArray[r, TYPE_COLUMN];
                double parent = (double)valueArray[r, PARENT_COLUMN];
                
            
                Console.WriteLine("Label:" + label + " Type:" + type);
                List<LocationData> children = getChildren(r, valueArray, id);
                graph.Add(new LocationData(id, label, type, parent), children);
               foreach (LocationData ld in children )
                {
                    graph.Add(ld, getChildren(r, valueArray, ld.id));
                }

               
            }
            return graph;
        }

        private List<LocationData> getChildren(int parentRow,object[,]vals, double parentID)
        {
            List<LocationData> locs = new List<LocationData>();
            int rowsLength = vals.GetLength(0);
            int index = parentRow+1;
            string firstLabel = null;
         
            while (index <= rowsLength)
            {

                double id = (double)vals[index, ID_COLUMN];
                double pid = (double)vals[index, PARENT_COLUMN];
                string label = (string)vals[index, LABEL_COLUMN];
                string type = (string)vals[index, TYPE_COLUMN];
               
               
                if (pid==parentID)
                    locs.Add(new LocationData(id, label, type, pid));
                index++;
            }    
   

            return locs;
        }
        
    }
}
                
               
            


          
