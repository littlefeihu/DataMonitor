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

        public static event Action<List<MyTcpSocketClient>> StartCompleted;

        public static void Start()
        {
            var alldevices = DeviceService.GetAllDevices();

            var getwayAddresses = alldevices.Select(o => new { o.IPAddress, o.Port }).Distinct();

            foreach (var getwayItem in getwayAddresses)
            {
                var client = new MyTcpSocketClient(getwayItem.IPAddress, getwayItem.Port);

                bool success = false;
                do
                {
                    try
                    {
                        client.ConnectToServer();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                } while (success == false);

                Clients.Add(client);

                foreach (var device in alldevices.Where(o => o.Port == getwayItem.Port && o.IPAddress == getwayItem.IPAddress))
                {
                    DeviceItems.Add(new DeviceItem(device, client));
                }
            }
            if (StartCompleted != null)
            {
                StartCompleted(Clients);
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
