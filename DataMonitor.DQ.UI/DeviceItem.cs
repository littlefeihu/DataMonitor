using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.UI
{
    public class DeviceItem
    {

        public DeviceItem(Device device, MyTcpSocketClient client)
        {
            this.Device = device;
            this.Client = client;
        }

        public Device Device { get; private set; }


        public MyTcpSocketClient Client { get; private set; }



    }
}
