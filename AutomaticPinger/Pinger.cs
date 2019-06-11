using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Timers;
namespace AutomaticPinger
{
    public static class Pinger
    {
        private static Timer timer = null;


        public static int ScheduleTask(int min)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000 * 60 * min;


            timer.Start();
            timer.Elapsed += new ElapsedEventHandler(PingSite);
            return min;

        }

        private static async void PingSite(object source, ElapsedEventArgs e)
        {
            await PingSiteAsync();
        }

        private static async System.Threading.Tasks.Task PingSiteAsync()
        {
            Console.WriteLine("Pinging Site");

            Dictionary<string, string> values = new Dictionary<string, string>();

            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://datavisualiser.d3f.world/Home", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
        }
    }




}
