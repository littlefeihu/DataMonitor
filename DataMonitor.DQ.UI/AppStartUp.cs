using Cowboy.Sockets;
using DataMonitor.DQ.BusinessLayer;
using DataMonitor.DQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.UI
{
    public class AppStartUp
    {

        public static List<DeviceItem> DeviceItems = new List<DeviceItem>();

        public static List<MyTcpSocketClient> Clients = new List<MyTcpSocketClient>();

        public static void Start()
        {
            var alldevices = DeviceService.GetAllDevices();

            var getwayAddresses = alldevices.Select(o => new { o.IPAddress, o.Port }).Distinct();

            foreach (var getwayItem in getwayAddresses)
            {
                var client = new MyTcpSocketClient(getwayItem.IPAddress, getwayItem.Port);

                client.ConnectToServer();

                Clients.Add(client);

                foreach (var device in alldevices.Where(o => o.Port == getwayItem.Port && o.IPAddress == getwayItem.IPAddress))
                {
                    DeviceItems.Add(new DeviceItem(device, client));
                }
            }
        }

        public static void ShutDown()
        {
            foreach (var client in Clients)
            {
                client.ShutDown();
            }
        }


    }
}
