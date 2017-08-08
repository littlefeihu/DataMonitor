using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Message
{
    /// <summary>
    /// 读温度报警值上限下限
    /// </summary>
    class ReadTemperatureAlarmNumAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "0A01"; }
        }

        //0A 00 DE 03
        public void Excute(byte[] body)
        {
            byte[] temp = new byte[2];
            Array.Copy(body, 0, temp, 0, 2);
            var low = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()))*0.1;

            Array.Copy(body, 2, temp, 0, 2);
            var high = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()))*0.1;

            Console.WriteLine("温度报警值下限{0},温度报警值上限{1}", low, high);
        }
    }
}
