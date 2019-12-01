using System;
using System.Net;
using System.Text;
using SuperSocket.ProtoBase;

namespace WindowsFormsApp1.service
{
    class socketClient
    {
        SuperSocket.ClientEngine.EasyClient client;
        /// <summary>
        /// 定义服务端的ip地址和端口，以及接收数据的头和尾，只有在头和尾之间的数据才算有效数据
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">服务端口</param>
        /// <param name="startFilter">数据头</param>
        /// <param name="endFilter">数据尾</param>
        public socketClient(string ip, int port, string startFilter, string endFilter)
        {
            this.ip = ip;
            this.port = port;
            if (!string.IsNullOrEmpty(startFilter)) this.startFilter = startFilter;
            if (!string.IsNullOrEmpty(endFilter)) this.endFilter = endFilter;
            client = new SuperSocket.ClientEngine.EasyClient();
            client.Initialize(new MyBeginEndMarkReceiveFilter(this.startFilter, this.endFilter), onReceived);
        }
        string ip;
        int port;
        string startFilter = "!!!";
        string endFilter = "###";
        bool cycleSend = false;
        /// <summary>
        /// 要发送到服务端的数据
        /// </summary>
        public string data { get; set; } = "hello,this is super client\r\n";
        public void startComm()
        {//开始循环发送数据
            cycleSend = true;
            System.Threading.Thread _thread = new System.Threading.Thread(sendData);
            _thread.IsBackground = true;
            _thread.Start();
        }
        public void sendData()
        {//采用线程间隔一秒发送数据，防止界面卡死
            while (cycleSend)
            {
                if (!client.IsConnected)
                {
                    connectToServer(ip, port);
                }
                if (client.IsConnected)
                {
                    client.Send(Encoding.ASCII.GetBytes("hello,this is super client\r\n"));
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void stopComm()
        {//停止循环发送数据
            cycleSend = false;
        }
        public async void connectToServer(string ip, int port)
        {//连接到服务端
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), port));
            if (connected)
            {
                //发送连接信息
                try
                {
                    client.Send(Encoding.ASCII.GetBytes("build connection"));
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }   
        }
        public System.EventHandler newReceived;
        private void onReceived(StringPackageInfo stringPackageInfo)
        {//当读取到数据，触发一个事件，方便外部接收数据
            if (newReceived != null)
            {
                newReceived(stringPackageInfo.Body, new EventArgs());
            }
        }
    }
}
