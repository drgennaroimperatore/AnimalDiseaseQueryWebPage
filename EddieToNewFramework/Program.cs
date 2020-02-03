using System;

namespace EddieToNewFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            AmirTechEddieTod3fBridge b = new AmirTechEddieTod3fBridge("1.0");
            b.Initialise();
            b.TransposeContents();

            Console.ReadLine();
        }
    }
}
