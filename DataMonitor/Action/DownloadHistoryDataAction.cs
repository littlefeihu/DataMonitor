using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Message
{

    /// <summary>
    /// 下载历史温湿度数据
    /// </summary>
    public class DownloadHistoryDataAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "06AA"; }
        }


        //3C 04 06 AA 2F 01 00 17 08 07 14 48 33 01 E9 01 17 08 07 14 49 33 01 EA 01 17 08 07 14 50 33 01 EA 01 17 08 07 14 51 33 01 EA 01 17 08 07 14 52 33 01 EC 01 66 C8
        //0100 17 08 07 14 48 33 01 E9 01 17 08 07 14 49 33 01 EA 01 17 08 07 14 50 33 01 EA 01 17 08 07 14 51 33 01 EA 01 17 08 07 14 52 33 01 EC 01
        public void Excute(byte[] body)
        {
            //包索引
            byte[] temp = new byte[2];
            Array.Copy(body, 0, temp, 0, 2);
            int packageIndex = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp));
            byte[] recordBytes = new byte[body.Length - 2];

            Array.Copy(body, 2, recordBytes, 0, body.Length - 2);

            for (int i = 0; i < recordBytes.Length; i++)
            {

            }

            //17 08 07 14 48 33 01 E9 01

            temp = new byte[5];
            Array.Copy(body, 2, temp, 0, 5);
            string year = temp[0].ToString("X2");
            string month = temp[1].ToString("X2");
            string day = temp[2].ToString("X2");
            string hour = temp[3].ToString("X2");
            string minute = temp[4].ToString("X2");

            temp = new byte[2];
            Array.Copy(body, 7, temp, 0, 2);
            //温度
            var temperature = int.Parse(temp[0].ToString("X2") + temp[1].ToString("X2")) * 0.01;
            Array.Copy(body, 9, temp, 0, 2);
            var humidity = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray())) * 0.1;

            Console.WriteLine("{0}年{1}月{2}日{3}时{4}分,温度{5}，湿度{6}", year, month, day, hour, minute, temperature, humidity);

        }
    }
}
