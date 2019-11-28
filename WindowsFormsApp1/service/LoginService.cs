using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JumpKick.HttpLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WindowsFormsApp1.utils;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WindowsFormsApp1.service
{
    class LoginService
    {
        public Boolean login(String loginName, String password)
        {

            string body = HttpUtil.post("http://scs.ponyred.com/api/pac/auth/new_login?username=" + loginName + "&password=" + password);
            Console.WriteLine("=======Util========");
            JObject jObject = (JObject)JsonConvert.DeserializeObject(body);
            int code = int.Parse(jObject["code"].ToString());
            if (code == 0)
            {
                string data = jObject["data"].ToString();
                Console.WriteLine(data);
                JObject dataObj = (JObject)JsonConvert.DeserializeObject(data);
                return true;
            }
            return false;
        }
    }
}
