using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor
{
    public class DataParser
    {
        byte[] _bytes;

        byte[] temp = new byte[2];
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes">body Bytes</param>
        public DataParser(byte[] bytes)
        {
            this._bytes = bytes;
        }
        /// <summary>
        /// 温度
        /// </summary>
        public double Temperature
        {
            get
            {
                Array.Copy(_bytes, temp, 2);
                return ConvertData();
            }
        }
        /// <summary>
        /// 湿度
        /// </summary>
        public double Humidity
        {
            get
            {
                Array.Copy(_bytes, 2, temp, 0, 2);
                return ConvertData();
            }
        }


        private double ConvertData()
        {

            var data = DataHelper.ConvertToIntFromHex(DataHelper.byteToHexStr(temp.Reverse().ToArray()));
            if (data < 10)
            {
                data = data * 1000;
            }
            else if (data < 100)
            {
                data = data * 100;
            }
            else if (data < 1000)
            {
                data = data * 10;
            }
            return data * 0.01;
        }
    }
}
