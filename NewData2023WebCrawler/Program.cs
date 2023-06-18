using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewData2023WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting....");
           
            

                EddieScraper.PostAsync().Wait();
            
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
