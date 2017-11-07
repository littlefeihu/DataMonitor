using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    public class MsgItem
    {
        byte[] _source;
        int _dataOffset;
        int _dataLength;
        byte[] bytes;
        public MsgItem(byte[] source, int dataOffset, int dataLength)
        {
            this._source = source;
            this._dataLength = dataLength;
            this._dataOffset = dataOffset;
            GetData();
        }

        public string GetHexString()
        {
            return DataHelper.byteToHexStr(GetData());
        }

        private byte[] GetData()
        {
            bytes = new byte[_dataLength];

            Array.Copy(_source, _dataOffset, bytes, 0, _dataLength);

            return bytes;
        }

        public string FrameHeaderHex
        {
            get
            {
                return bytes[0].ToString("X2");
            }
        }
        public string DeviceAddressHex
        {
            get
            {
                return bytes[1].ToString("X2");
            }
        }
        public string CommandHex
        {
            get
            {
                return bytes[2].ToString("X2") + bytes[3].ToString("X2");
            }
        }
        public string BodyLengthHex
        {
            get
            {
                return bytes[4].ToString("X2");
            }
        }
        public string BodyDataHex
        {
            get
            {
                return DataHelper.byteToHexStr(BodyBytes);
            }
        }
        public byte[] BodyBytes
        {
            get
            {
                var length = DataHelper.ConvertToIntFromHex(BodyLengthHex);
                var bodybytes = new byte[length];

                Array.Copy(bytes, 5, bodybytes, 0, length);

                return bodybytes;
            }
        }

        public string FrameFooterHex
        {
            get
            {
                return bytes[_dataLength - 2].ToString("X2");
            }
        }
        public string CheckNumHex
        {
            get
            {
                return bytes[_dataLength - 1].ToString("X2");
            }
        }

        public bool Verify()
        {
            var temp = DataHelper.GetHexCheckSum(bytes);

            return temp == CheckNumHex;

        }

    }
}
