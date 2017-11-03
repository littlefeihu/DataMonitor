using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    public class ReadSaveIntervalAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "08DD"; }
        }
        //3C 08 08 DD 04 01 DC 05 00 66 75
        //3C 08 08 DD 03 01 DC 05  66 75
        public void Excute(byte[] body)
        {
            int time = DataHelper.ConvertToIntFromHex(body[0].ToString("X2"));
            string unit = "";
            if (body.Length == 3)
            {
                unit = "分钟";
            }
            else if (body.Length == 4)
            {
                unit = "小时";
            }
            Console.WriteLine("温湿度节点保存间隔{0}{1}", time, unit);
        }
    }
}
