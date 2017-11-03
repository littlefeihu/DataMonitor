using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    /// <summary>
    /// 读湿度报警值上限下限
    /// </summary>
    public class ReadHumidityMsgAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "0A02"; }
        }

        /// <summary>
        /// 3C 04 0A 02 04 C8 00 70 03 66 F1
        /// </summary>
        /// <param name="body"></param>
        public void Excute(byte[] body)
        {
            byte[] temp = new byte[2];
            Array.Copy(body, 0, temp, 0, 2);
            var low = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray())) * 0.1;

            Array.Copy(body, 2, temp, 0, 2);
            var high = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray())) * 0.1;

            Console.WriteLine("湿度报警值下限{0},湿度报警值上限{1}", low, high);
        }
    }
}
