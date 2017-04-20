using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace IPRegister
{
    class Program
    {
        static string Key;
        static string Url;
        static int counter = 0;

        static StreamWriter logFile;
        static void Main(string[] args)
        {
            
            logFile = new StreamWriter("Register.log", true, System.Text.Encoding.UTF8);
            Key = System.Configuration.ConfigurationManager.AppSettings["Key"].ToString();
            Url = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();
            int interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["interval"].ToString());
            logWriting("Get Configuration Information!");
            Console.WriteLine("Start!");
            logWriting("Start!");
            do
            {
                doRegister();
                System.Threading.Thread.Sleep(interval);

            }
            while (true);
        }
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            logWriting("Exit");
            logFile.Close();
        }

        static void doRegister()
        {
            Console.WriteLine("第" + (counter++) + "次触发!" + DateTime.Now.ToString());
            logWriting("第" + counter + "次触发!");
            string getIPUrl = "http://tools.2345.com/api/getip.php?act=getips";
            string getIpStr = HttpGetPost.RequestGet(getIPUrl);
            Regex regex = new Regex(@"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");
            CaptureCollection gc = regex.Match(getIpStr).Captures;
            if (gc.Count > 0)
            {
                string IPStr = gc[gc.Count - 1].Value;

                Console.WriteLine("当前外网IP:" + IPStr);
                string data = "Key=" + Key + "&ip=" + IPStr;
                //throw new NotImplementedException();
                string result = string.Empty;
                try
                {
                    result = HttpGetPost.PostDataToUrl(data, Url);
                    Console.WriteLine("更新结果:" + result);
                    logWriting("更新结果:" + result);

                }
                catch (Exception e)
                {
                    result = e.Message;
                    Console.WriteLine("更新结果:" + result);
                    logWriting("更新结果:" + result);
                    flushException();
                }

            }
            else
            {
                Console.WriteLine("未获得地址:" + getIpStr);
                logWriting("未获得地址:" + getIpStr);
                flushException();
            }
        }

        internal static void logWriting(string logString)
        {
            logFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + logString);
            logFile.Flush();
        }
        static void flushException()
        {
            string result = string.Empty;
            string url = string.Empty;
            UriBuilder ub = new UriBuilder(Url);
            ub.Path = "Default/flushClientException/register";
            try
            {
                result = HttpGetPost.RequestGet(ub.ToString());

            }
            catch (Exception e) { result = e.Message; }

            Console.WriteLine("刷新异常结果:" + result);
            logWriting("刷新异常结果:" + result);
        }
    }
}
