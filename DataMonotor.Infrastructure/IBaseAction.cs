using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    public interface IBaseAction
    {
        string CommandHex
        {
            get;
        }
        void Excute(byte[] body);
    }
}
