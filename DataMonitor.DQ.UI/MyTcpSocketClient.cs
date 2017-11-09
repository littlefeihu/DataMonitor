using Cowboy.Sockets;
using DataMonitor.DQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.UI
{
    public class MyTcpSocketClient
    {
        public TcpSocketClient Client;


        public event Action<string, string> ServerConnected;
        public event Action<string, string> ServerDisconnected;
        public event Action<string, string, string> ServerDataReceived;
        private List<IBaseAction> actions = new List<IBaseAction>();

        public string Ip;
        public string Port;

        public MyTcpSocketClient(string ip, string port)
        {
            this.Ip = ip;
            this.Port = port;
            actions.Add(new GetTemperatureAndHumidityAction());
            actions.Add(new DownloadHistoryDataAction());
        }


        public void Send(string cmdText)
        {
            var cmdbytes = new GetDataCommand(cmdText).GetCommandBytes();
            if (Client != null)
            {
                Client.Send(cmdbytes);
            }
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <returns></returns>
        public TcpSocketClient ConnectToServer()
        {
            var config = new TcpSocketClientConfiguration();
            config.SendTimeout = TimeSpan.FromSeconds(2);
            config.FrameBuilder = new RawBufferFrameBuilder();
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(Port));

            Client = new TcpSocketClient(remoteEP, config);
            Client.ServerConnected += client_ServerConnected;
            Client.ServerDisconnected += client_ServerDisconnected;
            Client.ServerDataReceived += client_ServerDataReceived;
            Client.Connect();

            return Client;
        }


        public void ShutDown()
        {
            Client.Shutdown();
        }

        void client_ServerConnected(object sender, TcpServerConnectedEventArgs e)
        {
            if (ServerConnected != null)
            {
                ServerConnected(e.RemoteEndPoint.Address.ToString(), e.RemoteEndPoint.Port.ToString());
            }
            Console.WriteLine(string.Format("已连接至服务器 {0} .", e.RemoteEndPoint));
        }

        void client_ServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {
            if (ServerDisconnected != null)
            {
                ServerDisconnected(e.RemoteEndPoint.Address.ToString(), e.RemoteEndPoint.Port.ToString());
            }
            Console.WriteLine(string.Format("已从服务器{0}断开连接.", e.RemoteEndPoint));
        }

        void client_ServerDataReceived(object sender, TcpServerDataReceivedEventArgs e)
        {
            var msgItem = new MsgItem(e.Data, e.DataOffset, e.DataLength);

            if (msgItem.CommandHex == "06AA")
            {//历史数据处理
                if (msgItem.BodyLengthHex == "06")
                {//包数解析
                    new GetPackageCountAction().Excute(msgItem.BodyBytes);
                }
                else
                {//包内容解析
                    new DownloadHistoryDataAction().Excute(msgItem.BodyBytes);
                }
            }

            if (ServerDataReceived != null)
            {
                if (msgItem.CommandHex == "01FF")
                {
                    //数据接收
                    var data = new DataParser(msgItem.BodyBytes);

                    ServerDataReceived(msgItem.DeviceAddressHex, data.Temperature.ToString(), data.Humidity.ToString());

                }

            }
        }
    }
}
