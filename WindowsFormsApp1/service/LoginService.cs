using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpKick.HttpLib;
namespace WindowsFormsApp1.service
{
    class LoginService
    {
        public String login(String loginName, String password) {
   
            
            Console.WriteLine("===========httpBegin==========");
            Http.Post("http://scs.ponyred.com/api/pac/auth/new_login?username=" + loginName + "&password" + password)
                .Headers(new { XRequestedWith = "XMLHttpRequest", UserAgent = "scs-system#########" })
                .OnSuccess(result =>
                {
                    Console.WriteLine("===========success==========");
                    Console.WriteLine(result);
                }).OnFail(result =>
                {
                     Console.WriteLine("===========fail==========");
                    Console.WriteLine(result);
                }).Go();
            return "loginName:" + loginName + " password:" + password;
        }
    }
}
