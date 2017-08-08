using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Message
{
    public class GetDeviceDatetimeAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "08CC"; }
        }

        //3C 04 08 CC 06 17 16 18 32 34 21 66 4C 
        //17 08 08 15 15 41 66 74 
        public void Excute(byte[] body)
        {
            string year = body[0].ToString("X2");
            string month = body[1].ToString("X2");
            string day = body[2].ToString("X2");
            string hour = body[3].ToString("X2");
            string minute = body[4].ToString("X2");
            string second = body[5].ToString("X2");


            Console.WriteLine("{0}年{1}月{2}日{3}时{4}分{5}秒", year, month, day, hour, minute, second);
        }
    }
}
