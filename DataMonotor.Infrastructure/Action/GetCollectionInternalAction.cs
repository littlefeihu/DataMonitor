using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    /// <summary>
    /// 获取采集保存间隔
    /// </summary>
    public class GetCollectionInternalAction : IBaseAction
    {
        //01 DC 05 00
        public void Excute(byte[] body)
        {
            //采集保存间隔
            int saveInterval = DataHelper.ConvertToIntFromHex(body[0].ToString("X2"));
            //历史配置容量
            byte[] temp = new byte[3];
            Array.Copy(body, 1, temp, 0, 3);
            int hisCapacity = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp));

            Console.WriteLine("采集保存间隔{0}分钟,历史配置容量{1}条", saveInterval, hisCapacity);

        }





        public string CommandHex
        {
            get { return "08DD"; }
        }
    }
}
