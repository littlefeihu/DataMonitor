using Cowboy.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor
{
    class Program
    {
        static TcpSocketClient _client;

        static void Main(string[] args)
        {
            ConnectToServer();
            Console.WriteLine("TCP客户端已连接到服务器");
            Console.WriteLine("现在可以给服务器发送指令了");
            while (true)
            {
                try
                {
                    string text = Console.ReadLine();
                    if (text == "quit")
                    {
                        break;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(text))
                        {
                            //获取温度湿度 250301FF0066
                            //获取采集保存间隔250308DD0066
                            //获取历史总包数历史总条数250306AA0066



                            //修改节点地址
                            //选中设备,获取设备硬件软件版本号25080CAA0066
                            //设置设备地址25070255010B66

                            //读温度报警值25030A010066
                            //读湿度报警值25030A020066

                            //写温度报警值25030A01040A00DE0366
                            //写湿度报警值25030A0204C800700366


                            //读温湿度报警状态25030ABB0066
                          


                            //读取保存间隔250308DD0066
                            //设置保存间隔 写分钟   记录间隔 存储容量
                            //250308DD0305E80366
                            //设置保存间隔 写小时
                            //250308DD0401DC050066

                            //清空历史记录
                            //250309FF0066


                            //读取时间
                            //250008CC0066
                            //设置时间
                            //250008BB0617071618303066
                            //下载历史数据250306AA020100
                            //设置温度报警25030AAA010166
                            //设置湿度报警25030AAA010266
                            //设置温湿度报警25030AAA010366
                            //关闭温湿度报警25030AAA010066

                            text = "250506AA0066";
                        }
                        var cmdbytes = new GetDataCommand(text).GetCommandBytes();

                        _client.Send(cmdbytes);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            _client.Shutdown();
            Console.WriteLine("TCP客户端已经从服务器断开了");

            Console.ReadKey();
        }

        private static void ConnectToServer()
        {
            var config = new TcpSocketClientConfiguration();
            config.SendTimeout = TimeSpan.FromSeconds(2);
            config.FrameBuilder = new RawBufferFrameBuilder();
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.233"), 10001);

            _client = new TcpSocketClient(remoteEP, config);
            _client.ServerConnected += client_ServerConnected;
            _client.ServerDisconnected += client_ServerDisconnected;
            _client.ServerDataReceived += client_ServerDataReceived;
            _client.Connect();
        }

        static void client_ServerConnected(object sender, TcpServerConnectedEventArgs e)
        {
            Console.WriteLine(string.Format("已连接至服务器 {0} .", e.RemoteEndPoint));
        }

        static void client_ServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {
            Console.WriteLine(string.Format("已从服务器{0}断开连接.", e.RemoteEndPoint));
        }

        static void client_ServerDataReceived(object sender, TcpServerDataReceivedEventArgs e)
        {
            var msgItem = new MsgItem(e.Data, e.DataOffset, e.DataLength);
            Console.WriteLine(msgItem.GetHexString());
            //var data = new DataParser(msgItem.BodyBytes);
            // Console.WriteLine("设备上传温度{0}，湿度{1}", data.Temperature, data.Humidity);
        }


    }
}
