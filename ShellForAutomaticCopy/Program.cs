using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellForAutomaticCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            //run the veteddie copier first
            Process process = new Process();
           // process.StartInfo.WorkingDirectory = @"H:\AutomaticScripts\Copier";
            process.StartInfo.FileName = @"C:\Users\gkb11185\Source\Repos\AnimalDiseaseQueryWebPage\VetEddieAutomaticCopier\bin\Release\VetEddieAutomaticCopier";
           // process.StartInfo.Arguments = "/c DIR"; // Note the /c command (*)
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
           
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            Console.ReadLine();
        }
    }
}
