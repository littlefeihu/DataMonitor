using Cowboy.Sockets;
using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DTO;
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

            #region 历史数据接收
            if (msgItem.CommandHex == "06AA")
            {//历史数据处理
                if (msgItem.BodyLengthHex == "06")
                {//包数解析（测点设备上有多少个包多少条记录）

                    //new GetPackageCountAction().Excute(msgItem.BodyBytes);
                    //历史总条数
                    byte[] temp = new byte[2];
                    Array.Copy(msgItem.BodyBytes, 0, temp, 0, 2);
                    int hisTotal = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()));
                    //历史总包数
                    Array.Copy(msgItem.BodyBytes, 2, temp, 0, 2);
                    int hisPackageTotal = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()));

                    Console.WriteLine("历史总条数{0},历史总包数{1}", hisTotal, hisPackageTotal);

                    for (int i = 0; i < hisPackageTotal; i++)
                    {
                        string cmdText = string.Format("25{0}06AA0066", msgItem.DeviceAddressHex);
                        var cmdbytes = new GetDataCommand(cmdText).GetCommandBytes();
                        Client.Send(cmdbytes);
                    }
                }
                else
                {//包内容解析（具体包内容解析）
                    var hisRecords = ParsePackageContent(msgItem);
                }
            }
            #endregion


            #region 实时数据接收
            if (ServerDataReceived != null)
            {
                if (msgItem.CommandHex == "01FF")
                {
                    //数据接收
                    var data = new DataParser(msgItem.BodyBytes);

                    ServerDataReceived(msgItem.DeviceAddressHex, data.Temperature.ToString(), data.Humidity.ToString());

                    AppStartUp.AddRecordTask(new Infrastructure.DTO.RealtimeRecord
                    {
                        DeviceAddressHex = msgItem.DeviceAddressHex,
                        Humidity = data.Humidity,
                        Temperature = data.Temperature
                    });
                }

            }
            #endregion
        }



        private IEnumerable<HisRecord> ParsePackageContent(MsgItem item)
        {
            var body = item.BodyBytes;
            //测试数据解析
            //body = DataHelper.HexStrTobyte("0100 17 08 07 14 48 33 01 E9 01 17 08 07 14 49 33 01 EA 01 17 08 07 14 50 33 01 EA 01 17 08 07 14 51 33 01 EA0117080714523301EC01".Replace(" ", ""));
            //包索引
            byte[] temp = new byte[2];
            Array.Copy(body, 0, temp, 0, 2);
            int packageIndex = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp));
            byte[] recordBytes = new byte[body.Length - 2];
            int recordLength = 9;
            //构造历史记录byte数组
            Array.Copy(body, 2, recordBytes, 0, body.Length - 2);
            List<byte[]> records = new List<byte[]>();
            for (int i = 0; i < recordBytes.Length; i += recordLength)
            {
                temp = new byte[recordLength];
                Array.Copy(recordBytes, i, temp, 0, recordLength);
                records.Add(temp);
            }

            //打印历史温湿度记录信息
            foreach (var record in records)
            {
                //17 08 07 14 48 33 01 E9 01
                temp = new byte[5];
                Array.Copy(record, 0, temp, 0, 5);
                string year = temp[0].ToString("X2");
                string month = temp[1].ToString("X2");
                string day = temp[2].ToString("X2");
                string hour = temp[3].ToString("X2");
                string minute = temp[4].ToString("X2");
                //温度
                temp = new byte[2];
                Array.Copy(record, 5, temp, 0, 2);
                var temperature = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray())) * 0.1;
                //湿度
                Array.Copy(record, 7, temp, 0, 2);
                var humidity = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray())) * 0.1;


                var dateStr = string.Format("{0}年{1}月{2}日{3}时{4}分", year, month, day, hour, minute);

                yield return new HisRecord
                  {
                      Temperature = decimal.Parse(temperature.ToString()),
                      Humidity = decimal.Parse(humidity.ToString()),
                      DatetimeStr = dateStr,
                      DeviceAddressHex = item.DeviceAddressHex
                  };
            }


        }




    }
}
