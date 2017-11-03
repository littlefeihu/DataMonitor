using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    public class GetDataCommand : Command
    {
        //25 09 01 FF 00 66 94
        string _hexString;
        public GetDataCommand(string hexString)
        {
            _hexString = hexString;
        }
        public byte[] GetCommandBytes()
        {
            var checkSum = DataHelper.GetHexCheckSum(DataHelper.HexStrTobyte(_hexString), false);
            return DataHelper.HexStrTobyte(_hexString + checkSum);
        }
    }
}
