using Cowboy.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor
{
    public class DataHelper
    {

        /// <summary>
        /// 16进制转10进制
        /// </summary>
        /// <returns></returns>
        public static int ConvertToIntFromHex(string hexString)
        {
            return Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }
        /// <summary>
        /// 10进制转16进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ConvertToHexFromInt(int num)
        {
            return num.ToString("x8");
        }
        /// <summary>
        /// Byte数组转16进制
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 16进制字符串转字节数组   格式为 string sendMessage = "00 01 00 00 00 06 FF 05 00 64 00 00";
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStrTobyte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }

        public static string GetHexCheckSum(byte[] bytes, bool includeCheckSum = true)
        {
            int sum = 0;
            int packagelength = bytes.Length;
            if (includeCheckSum)
            {
                packagelength = bytes.Length - 1;
            }

            for (int i = 0; i < packagelength; i++)
            {
                sum += DataHelper.ConvertToIntFromHex(bytes[i].ToString("X2"));
            }
            var actualCheckSum = DataHelper.ConvertToHexFromInt(sum);

            byte[] checksumBytes = DataHelper.HexStrTobyte(actualCheckSum);

            var temp = checksumBytes[checksumBytes.Length - 1].ToString("X2");

            return temp;
        }

    }
}
