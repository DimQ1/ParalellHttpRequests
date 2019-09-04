using System;
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
                tasks.Add(LowLevelHttpRequest.BrutForceAsync(_urls.Pop()));
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
       
    }
}
