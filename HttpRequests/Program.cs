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

            var urls = new Stack<string>();

            for(var i=0; i<100; i++)
            {
                urls.Push("https://ya.ru");
                urls.Push("https://yandex.ru");
                urls.Push("https://google.com");
                urls.Push("https://google.ru");
                urls.Push("https://google.by");
            }

            HttpBruteForce httpBruteForce = new HttpBruteForce(urls, "", 200);

            httpBruteForce.StartBruteForce();

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
