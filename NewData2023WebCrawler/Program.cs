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



            //EddieScraper.PostAsync().Wait();
            // var data= EddieScraper.DeserialiseJsonFile("sheep");
            EddieScraper.CleanJsonFile("sheep");
            
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
