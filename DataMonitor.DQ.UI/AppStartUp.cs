using Cowboy.Sockets;
using DataMonitor.DQ.BusinessLayer;
using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using DataMonitor.DQ.Infrastructure.DTO;
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

        public static Queue<RealtimeRecord> RecordTasks = new Queue<RealtimeRecord>();

        public static event Action<List<MyTcpSocketClient>> StartCompleted;

        private static bool isRuning = false;

        public static List<Device> Devices = new List<Device>();

        private static object addTaskLock = new object();

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


        public static void AddRecordTask(RealtimeRecord record)
        {
            lock (addTaskLock)
            {
                RecordTasks.Enqueue(record);

                if (!isRuning)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            isRuning = true;
                            IList<MonitoringRecord> records = new List<MonitoringRecord>();
                            do
                            {
                                var realtimeRecord = RecordTasks.Dequeue();
                                var deviceitem = DeviceItems.FirstOrDefault(o => o.Device.DeviceNum == realtimeRecord.DeviceAddressHex);
                                records.Add(new MonitoringRecord
                                {
                                    CreateOn = DateTime.Now,
                                    CreateOnStr = string.Format("{0:yyyy年MM月dd日HH时mm分}", DateTime.Now),
                                    DeviceId = deviceitem.Device.Id,
                                    DeviceName = deviceitem.Device.DeviceName,
                                    DeviceNum = deviceitem.Device.DeviceNum,
                                    Humidity = realtimeRecord.Humidity,
                                    Temperature = realtimeRecord.Temperature,
                                    Latitude = "",
                                    Longitude = ""
                                });

                                if (records.Count > 50)
                                {
                                    break;
                                }

                            } while (RecordTasks.Count > 0);

                            MonitoringRecordService.SaveRecords(records);
                        }
                        catch (Exception ex)
                        {
                            isRuning = false;
                        }
                    });
                }
            }
        }



    }
}
