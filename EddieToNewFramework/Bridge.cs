using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EddieToNewFramework
{
    class Bridge
    {
        RefactoredEddieContext eddie = new RefactoredEddieContext();
        TestFrameworkContext ADDB = new TestFrameworkContext();


        public Bridge()
        {
            Console.WriteLine("Bridge console application 1.0");

        }

        public void Initialise()
        {
            Console.WriteLine("Initialisation method");
            int c =eddie.SetCase.Count();
            Console.WriteLine("SetCase Count " + c);
        }
    }

    
}
