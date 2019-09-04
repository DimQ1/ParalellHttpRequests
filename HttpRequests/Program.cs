using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Configure();
            var urlsFilePath = $"{System.IO.Directory.GetCurrentDirectory()}\\Hosts.txt";
            var urls = UrlsReader.GetUrls(urlsFilePath);

            HttpBruteForce httpBruteForce = new HttpBruteForce(urls, "", 2000);

            httpBruteForce.StartBruteForce();
        }
    }
}
