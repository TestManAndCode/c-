using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JumpKick.HttpLib;

namespace WindowsFormsApp1.utils
{
    public static class HttpUtil
    {
        public static void get() { 
        }

        public static string post(string url) {
            HttpWebRequest request =(HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = 30000;
            string body = "{\"code\":500}";
            try {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                body = sr.ReadToEnd();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
            return body;
        }
    }
}
