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
using System.Xml;

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
            string pathJson = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\locations.xml");

            WB = app.Workbooks.Open(path);
            worksheet = WB.Worksheets[1];
            usedCells = worksheet.UsedRange;
            Console.WriteLine("Used Rows: " + usedCells.Count);
            // readSheet(); // use default values
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true
               
            };
            using (XmlWriter writer = XmlWriter.Create(pathJson,xmlWriterSettings))
            {
               
                writer.WriteStartElement("Locations");
                 read(writer);
                writer.WriteEndElement();
              
                writer.Flush();
                writer.Close();
            }


            //save json to disk

            WB.Close();

            Marshal.ReleaseComObject(WB);
            Marshal.ReleaseComObject(app);

        }


    
        

        private Dictionary<LocationData, List<LocationData>> read(XmlWriter writer)
        {
            Dictionary<LocationData, List<LocationData>> graph = new Dictionary<LocationData, List<LocationData>>();
            object[,] valueArray = (object[,])usedCells.get_Value(
                                       XlRangeValueDataType.xlRangeValueDefault);
            int rowsLength = valueArray.GetLength(0);
            int columnsLength = valueArray.GetLength(1);

            
            getChildren(writer,2, valueArray, -99);

         
              
           // }
            return graph;
        }

        private void getChildren(XmlWriter writer, int parentRow,object[,]vals, double parentID)
        {
            List<LocationData> locs = new List<LocationData>();
            int rowsLength = vals.GetLength(0);
            int index = parentRow;
            
                                   
            while (index<=rowsLength)
            {
                double id = (double)vals[index, ID_COLUMN];
                double pid = (double)vals[index, PARENT_COLUMN];
                string label = (string)vals[index, LABEL_COLUMN];
                string type = (string)vals[index, TYPE_COLUMN];
              

                if (pid == parentID)
                {
                    writer.WriteStartElement(type);
                    writer.WriteAttributeString("ID", ((int)id).ToString());
                    writer.WriteAttributeString("parent_ID", ((int)pid).ToString());
                    writer.WriteAttributeString("name", label);

               
                    getChildren(writer, index, vals, id);

                    writer.WriteEndElement();
                }
                index++;
            }
                          
           
        }
        
    }
}
                
               
            


          
