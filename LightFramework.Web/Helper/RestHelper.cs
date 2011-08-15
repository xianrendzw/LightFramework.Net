using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LightFramework.Web
{
    public class RestHelper
    {
        public static string Get(Uri uri)
        {
            string text;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    text = new StreamReader(responseStream).ReadToEnd();
                }
                response.Close();
                return text;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Post<T>(Uri uri, T dto)
        {
            string text = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/json";
                using (Stream requestStream = request.GetRequestStream())
                {
                    new DataContractJsonSerializer(dto.GetType()).WriteObject(requestStream, dto);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    text = new StreamReader(responseStream).ReadToEnd();
                }
                response.Close();
                return text;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
