﻿using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.service
{
    public static class SuperSocketServer
    {
        public static AppServer appServer { get; set; }
        public static void Main(string[] args)
        {
            appServer = new AppServer();


            //Setup the appServer
            if (!appServer.Setup(2012)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }


            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            //1.
            appServer.NewSessionConnected += new SessionHandler<AppSession>(appServer_NewSessionConnected);
            appServer.SessionClosed += appServer_NewSessionClosed;

            //2.
            appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer
            appServer.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        //1.
        public static void appServer_NewSessionConnected(AppSession session)
        {
            Console.WriteLine($"服务端得到来自客户端的连接成功");

            var count = appServer.GetAllSessions().Count();
            Console.WriteLine("~~" + count);
            session.Send("Welcome to SuperSocket Telnet Server");
        }

        public static void appServer_NewSessionClosed(AppSession session, CloseReason aaa)
        {
            Console.WriteLine($"服务端 失去 来自客户端的连接" + session.SessionID + aaa.ToString());
            var count = appServer.GetAllSessions().Count();
            Console.WriteLine(count);
        }

        //2.
        public static void appServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            Console.WriteLine(requestInfo.Key);
            session.Send(requestInfo.Body);
        }

    }
}