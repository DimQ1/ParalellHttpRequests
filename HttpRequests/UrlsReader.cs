using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequests
{
    public class UrlsReader
    {
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
    }
}
