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
            lr.generateXML();
            

            Console.ReadLine();

        }

        
    }

        
}
