using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Message
{
    /// <summary>
    /// 获取历史总包数历史总条数
    /// </summary>
    public class GetPackageCountAction : IBaseAction
    {
        public string CommandHex
        {
            get { return "06AA"; }
        }

        /// <summary>
        /// 4B 04 DC 00 2D 00
        /// </summary>
        /// <param name="body"></param>
        public void Excute(byte[] body)
        {
            //历史总条数
            byte[] temp = new byte[2];
            Array.Copy(body, 0, temp, 0, 2);
            int hisTotal = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()));
            //历史总包数
            Array.Copy(body, 2, temp, 0, 2);
            int hisPackageTotal = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()));
            ////包内容长度
            //Array.Copy(body, 4, temp, 0, 2);
            //int hisPackageContentTotal = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp));
            Console.WriteLine("历史总条数{0},历史总包数{1}", hisTotal, hisPackageTotal);

        }
    }
}
