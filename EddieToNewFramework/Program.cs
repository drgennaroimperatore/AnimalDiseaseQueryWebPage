using System;

namespace EddieToNewFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            // AmirTechEddieTod3fBridge b = new AmirTechEddieTod3fBridge("1.0");
            // b.Initialise();
            // b.TransposeContents();

            VetEddieTod3FBridge v2d3f = new VetEddieTod3FBridge("1.0f");
            v2d3f.Initialise();
             v2d3f.TransposeContents();
            //v2d3f.GetLatestImportedCase();

            Console.ReadLine();
        }
    }
}
