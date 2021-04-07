using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebsitesDownloadHW
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSitesDownloadTimeHW();
        }

        static void WebSitesDownloadTimeHW()
        {
            string[] addresses = new string[] { "https://soundcloud.com", "https://rotter.net", "https://www.walla.co.il",
                                                "https://www.ynet.co.il", "https://www.yahoo.com", "https://www.youtube.com",
                                                "https://www.jango.com", "https://www.msn.com", "https://www.facebook.com"};
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine();
            //}
            ParallelDownload(addresses);
            //Console.WriteLine("--------------");
            //SerialDownload(addresses);
            //Console.WriteLine("Done!");
            //Console.WriteLine();
        }

        static void SerialDownload(string[] addresses)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"{DateTime.Now} SerialDownload started:");

            foreach (string address in addresses)
            {
                DownloadWebsiteMeasure(address);
            }

            stopwatch.Stop();
            Console.WriteLine($"{DateTime.Now}: SerialDownload ended and took {stopwatch.Elapsed}.");
        }

        static void ParallelDownload(string[] addresses)
        {
            Stopwatch stopwatch = new Stopwatch();

            List<Action> actions = new List<Action>();

            foreach (string address in addresses)
            {
                actions.Add(() => DownloadWebsiteMeasure(address));
            }

            stopwatch.Start();
            Console.WriteLine($"{DateTime.Now} ParallelDownload started:");

            Parallel.For(0, addresses.Length, i => actions[i]());

            stopwatch.Stop();
            Console.WriteLine($"{DateTime.Now}: ParallelDownload ended and took {stopwatch.Elapsed}.");
        }

        static void ParallelForEachDownload(string[] addresses)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Console.WriteLine($"{DateTime.Now} ParallelDownload started:");

            Parallel.ForEach<string>(addresses, (address) => DownloadWebsiteMeasure(address));

            stopwatch.Stop();
            Console.WriteLine($"{DateTime.Now}: ParallelDownload ended and took {stopwatch.Elapsed}.");
        }

        static void DownloadWebsiteMeasure(string address)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(address);
            int siteSize = Encoding.Unicode.GetByteCount(html);

            stopwatch.Stop();
            Console.WriteLine($"Address: {address}, Size: {siteSize}, Download Duration: {stopwatch.Elapsed}");
        }

        static void SomeNewFunction()
        {
            Console.WriteLine("I do nothing!");
        }
    }
}
