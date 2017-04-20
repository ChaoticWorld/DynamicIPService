using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Dialer
{
    class HttpGetPost
    {
        //Get请求方式
        internal static string RequestGet(string Url)
        {
            string PageStr = string.Empty;//用于存放还回的html
            Uri url = new Uri(Url);//Uri类 提供统一资源标识符 (URI) 的对象表示形式和对 URI 各部分的轻松访问。就是处理url地址
            try
            {
                HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(url);//根据url地址创建HTTpWebRequest对象
                //参数设置
                HttpWebResponse response = (HttpWebResponse)httprequest.GetResponse();//使用HttpWebResponse获取请求的还回值
                Stream steam = response.GetResponseStream();//从还回对象中获取数据流
                StreamReader reader = new StreamReader(steam, Encoding.GetEncoding("gb2312"));//读取数据Encoding.GetEncoding("gb2312")指编码是gb2312，不让中文会乱码的
                PageStr = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception e)
            {
                PageStr += e.Message;
            }
            return PageStr;
        }

        /// <summary>
        /// Post data到url
        /// </summary>
        /// <param name="data">要post的数据</param>
        /// <param name="url">目标url</param>
        /// <returns>服务器响应</returns>
        internal static string PostDataToUrl(string data, string url)
        {
            string sRequestEncoding = "ascii";
            Encoding encoding = Encoding.GetEncoding(sRequestEncoding);
            byte[] bytesToPost = encoding.GetBytes(data);
            return PostDataToUrl(bytesToPost, url);
        }

        /// <summary>
        /// Post data到url
        /// </summary>
        /// <param name="data">要post的数据</param>
        /// <param name="url">目标url</param>
        /// <returns>服务器响应</returns>
        static string PostDataToUrl(byte[] data, string url)
        {
            #region 创建httpWebRequest对象
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                throw new ApplicationException(
                    string.Format("Invalid url string: {0}", url)
                   );
            }
            #endregion

            string sUserAgent =
              "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            string sContentType =
              "application/x-www-form-urlencoded";
            #region 填充httpWebRequest的基本信息
            httpRequest.UserAgent = sUserAgent;
            httpRequest.ContentType = sContentType;
            httpRequest.Method = "POST";
            #endregion

            #region 填充要post的内容
            httpRequest.ContentLength = data.Length;
            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            #endregion

            #region 发送post请求到服务器并读取服务器返回信息
            Stream responseStream;
            try
            {
                responseStream = httpRequest.GetResponse().GetResponseStream();
            }
            catch (Exception e)
            {
                // log error
                Console.WriteLine(
                    string.Format("POST操作发生异常：{0}", e.Message)
                    );
                throw e;
            }
            #endregion

            #region 读取服务器返回信息
            string stringResponse = string.Empty;
            string sResponseEncoding = "gb2312";
            using (StreamReader responseReader =
                new StreamReader(responseStream, Encoding.GetEncoding(sResponseEncoding)))
            {
                stringResponse = responseReader.ReadToEnd();
            }
            responseStream.Close();
            #endregion
            return stringResponse;
        }

    }
}
