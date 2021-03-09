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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML GENERATOR 1.0");
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\eth.xlsx");

                Excel.Application app = new Excel.Application();

                Excel.Workbook WB = app.Workbooks.Open(path);

                Excel.Worksheet worksheet = WB.Worksheets[1];
                Excel.Range usedCells = worksheet.UsedRange;
                Excel.Range currentRegion= usedCells.Find("Region", Missing.Value,
    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
    Missing.Value, Missing.Value);


                /*
                                object[,] valueArray = (object[,])regionRange.get_Value(
                                                        XlRangeValueDataType.xlRangeValueDefault);
                                int rowsLength = valueArray.GetLength(0);
                                int columnsLength = valueArray.GetLength(1);

                                Console.WriteLine(columnsLength);
                                Console.WriteLine(rowsLength);*/


           
                      Console.WriteLine(currentRegion.Row);
                Excel.Range firstFind = null;

                while (currentRegion != null)
                {
                    // Keep track of the first range you find. 
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
                    double id = ((worksheet.Cells[currentRegion.Row, 1] as Excel.Range).Value);
                    double parentID = ((worksheet.Cells[currentRegion.Row, 4] as Excel.Range).Value);
                    Console.WriteLine("ID: " + id + " " + "parentID:" + " " + parentID);
                    currentRegion = usedCells.FindNext(currentRegion);
                }


                    //close the workbook and the app 
                    WB.Close();
                Marshal.ReleaseComObject(WB);
                Marshal.ReleaseComObject(app);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
            finally
            {
               
            }

            Console.ReadLine();
        }

        static void findZonesForRegion()
        {

        }

        static void findWoredasForZone()
        {

        }

        static void findKebelesForWoreda()
        {

        }
    }
}
