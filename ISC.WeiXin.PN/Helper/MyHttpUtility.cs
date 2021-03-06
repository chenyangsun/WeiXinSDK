﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISC.WeiXin.PN.Helper
{
    /// <summary>
    /// 处理服务器端，发送Get,Post请求
    /// </summary>
    public class MyHttpUtility
    {
        /// <summary>
        /// 发送请求a=b&c=d
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="data">数据a=b&c=d</param>
        public static string Post(string url, string data)
        {
            return Post(url, "application/x-www-form-urlencoded", data);
        }

        /// <summary>
        /// 发送post请求(json）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data">json字符串</param>
        /// <returns></returns>
        public static string PostJson(string url, string data)
        {
            return Post(url, "application/json", data);
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string Post(string url, string contentType, string requestData)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            byte[] postBytes = null;
            request.ContentType = contentType;
            postBytes = Encoding.UTF8.GetBytes(requestData);
            request.ContentLength = postBytes.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            return Get(url, "application/x-www-form-urlencoded");
        }

        

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string Get(string url, string contentType)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = contentType;
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 请求文件,返回byte数组，get请求
        /// </summary>
        /// <param name="url"></param>
        public static byte[] SendGetFile(string url)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = contentType;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream reader = response.GetResponseStream())
                    {
                        byte[] buff = new byte[reader.Length];
                        reader.Read(buff, 0, buff.Length);
                        return buff;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
