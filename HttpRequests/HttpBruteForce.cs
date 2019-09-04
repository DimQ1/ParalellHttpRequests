﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequests
{
    public class HttpBruteForce
    {
        private readonly int _parralelCount;
        private readonly Stack<string> _urls;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();



        public HttpBruteForce(Stack<string> urls, string pathResult, int parralelCount = 10)
        {
            _parralelCount = parralelCount;
            _urls = urls;
            ServicePointManager.DefaultConnectionLimit = parralelCount;
        }


        public void StartBruteForce()
        {
            List<Task> tasks = new List<Task>();

            while (_urls.Count > 0)
            {
                tasks.Add(brutForceAsync(_urls.Pop()));
                Console.WriteLine(_urls.Count());

                if (tasks.Count > _parralelCount)
                {
                    Task.WaitAny(tasks.ToArray());
                    tasks = CleanFinishTasks(tasks);
                }
            }

            if (tasks.Count > 0)
            {
                tasks = CleanFinishTasks(tasks);
                Task.WaitAll(tasks.ToArray());
            }

        }

        private List<Task> CleanFinishTasks(List<Task> tasks)
        {
            List<Task> cleanTaskList = new List<Task>();

            foreach (var task in tasks)
            {
                if (!task.IsCompleted && !task.IsFaulted)
                {
                    cleanTaskList.Add(task);
                }
            }

            return cleanTaskList;
        }

        private async Task brutForceAsync(string webUrl)
        {
            await Task.Run(async () =>
             {
                 try
                 {
                     WebRequest webRequest = WebRequest.Create(webUrl);
                     webRequest.Method = HttpMethod.Head.ToString();

                     HttpWebResponse webresponse = (await webRequest.GetResponseAsync()) as HttpWebResponse;

                     logger.Info($"ok| {webresponse.StatusCode:D}|{webUrl}");
                 }
                 catch (HttpRequestException e)
                 {
                     logger.Error(e, $"{e.Message}|{webUrl}");
                 }
             });
        }
    }
}
