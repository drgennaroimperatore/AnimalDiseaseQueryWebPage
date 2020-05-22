using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetEddieAutomaticCopier
{
    class Program
    {
        static void Main(string[] args)
        {
            Copier c = new Copier();
            c.CopyNewCases();
            Console.ReadLine();
        }
    }
}
