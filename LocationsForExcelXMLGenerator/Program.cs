using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LocationsForExcelXMLGenerator
{
    class Program
    {
      
           
        static void Main(string[] args)
        {
            LocationReader lr = new LocationReader();

            Console.WriteLine("Generating locations.xml...");
            lr.generateXML();

            Console.WriteLine("Press key to exit...");
            Console.ReadLine();

        }

        
    }

        
}
