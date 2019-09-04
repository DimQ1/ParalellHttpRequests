using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequests
{
    public class LowLevelHttpRequest
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static async Task BrutForceAsync(string webUrl)
        {
            await Task.Run(async () =>
            {
                WebRequest webRequest = WebRequest.Create(webUrl);
                try
                {
                    webRequest.Method = "HEAD";

                    HttpWebResponse webresponse = (await webRequest.GetResponseAsync()) as HttpWebResponse;

                    logger.Info($"ok| {webresponse.StatusCode:D}|{webUrl}");

                    webresponse.Close();
                }
                catch (Exception e)
                {
                    logger.Error(e, $"{e.Message}|{webUrl}");
                }
            });
        }
    }
}
