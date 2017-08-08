using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.Message
{
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


        }
    }
}
