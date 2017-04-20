using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Dialer
{
    static class Program
    {
        static StreamWriter logFile;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            logFile = new StreamWriter("Dialer.log",true,System.Text.Encoding.UTF8);
            //getRemoteVpn();
            Application.ApplicationExit += Application_ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            logWriting("start!");
            logWriting("Run MainForm!");
            Application.Run(new MainForm());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            logWriting("Exit");
            logFile.Close();
        }
        internal static void logWriting(string logString)
        {
            logFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" - "+logString);
        }
        internal static List<vpn_Entry> getRemoteVpn()
        {
            string Url = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();
            string _keys = System.Configuration.ConfigurationManager.AppSettings["Keys"].ToString();
            string data = "Keys=" + _keys;
            string result = string.Empty;
            List<vpn_Entry> r = new List<vpn_Entry>();
            try
            {
                result = HttpGetPost.PostDataToUrl(data, Url);
                Program.logWriting(result);
                 r = JsonConvert.DeserializeObject<List<vpn_Entry>>(result);
            }
            catch (Exception e)
            {
                logWriting("服务器不可达！" + e.Message);
            }
            return r;
        }
    }
}