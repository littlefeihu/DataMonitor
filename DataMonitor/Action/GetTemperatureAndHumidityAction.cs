using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Message
{

    /// <summary>
    /// 获取实时温湿度数据
    /// </summary>
    public class GetTemperatureAndHumidityAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "01FF"; }
        }

        public void Excute(byte[] body)
        {
            var data = new DataParser(body);

            Console.WriteLine("温度{0},湿度{1}", data.Temperature, data.Humidity);
        }
    }
}
