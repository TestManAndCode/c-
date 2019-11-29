using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1.service
{
    public static class SuperSocketClient
    {
        public static Socket socketClient { get; set; }
        public  static void link()
        {
            //创建实例
            socketClient = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint point = new IPEndPoint(ip, 2012);
            //进行连接
            socketClient.Connect(point);


            //不停的接收服务器端发送的消息
            Thread thread = new Thread(Recive);
            thread.IsBackground = true;
            thread.Start();


            ////不停的给服务器发送数据
            Thread thread2 = new Thread(Send);
            thread2.IsBackground = true;
            thread2.Start();

            Console.ReadKey();

        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="o"></param>
        public static void Recive()
        {
            //  为什么用telnet客户端可以，但这个就不行。
            while (true)
            {
                //获取发送过来的消息
                byte[] buffer = new byte[1024 * 1024 * 2];
                var effective = socketClient.Receive(buffer);
                if (effective == 0)
                {
                    break;
                }
                var str = Encoding.UTF8.GetString(buffer, 0, effective);
                Console.WriteLine("来自服务器 --- " + str);
                Thread.Sleep(2000);
            }
        }


        public static void Send()
        {
            int i = 0;
            int sum = 0;
            while (true)
            {
                i++;
                sum += i;
                var buffter = Encoding.UTF8.GetBytes($"ADD {sum} {sum + 1}" + "\r\n");
                var temp = socketClient.Send(buffter);
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }

        }
    }
}
