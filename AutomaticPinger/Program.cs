using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticPinger
{
    class Program
    {
        static void Main(string[] args)
        {
            Pinger.ScheduleTask(1);
            Console.ReadLine();
        }
    }
}
