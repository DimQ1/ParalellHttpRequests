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

            ConfigureLogger();
            
            var urls = GetUrls($"{System.IO.Directory.GetCurrentDirectory()}\\Hosts.txt");


            HttpBruteForce httpBruteForce = new HttpBruteForce(urls, "", 2000);

            httpBruteForce.StartBruteForce();

        }


        public static Stack<string> GetUrls(string path)
        {
            var returnStack = new Stack<string>();
            var lines = System.IO.File.ReadAllLines(path);
            var filterUrlCollection = lines.Where(l => !l.StartsWith("#"))
                 .Select(l =>
                 {
                     var items = l.Split(' ');
                     if (items.Length > 1)
                     { return items[1]; }
                     else { return null; }
                 })
                 .Where(l => l != null);
            foreach (var item in filterUrlCollection)
            {
                returnStack.Push($"https://{item}");
                returnStack.Push($"http://{item}");
            }
            return returnStack;
        }


        static void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = "file.txt",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
            };
            //var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            // config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Error, logfile);



            // Apply config           
            NLog.LogManager.Configuration = config;
        }
    }
}
