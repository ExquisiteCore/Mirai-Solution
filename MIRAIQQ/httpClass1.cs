using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MIRAIQQ
{
    public class HttpClient
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">目标地址</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            string reslut = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Proxy = null;
                wbRequest.Method = "GET";
                wbRequest.Timeout = 5000;
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        reslut = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
            return reslut;
        }
    }
}
