using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.BusinessLayer
{
    public class AuthenticationService
    {
        private bool SignIn(string username, string password)
        {
            var encryptedpwd = SecurityHelper.Md5Encode(password);
            var user = DataAccess.Db.UserInfos.FirstOrDefault(o => o.UserName == username && o.Password == encryptedpwd);

            return user != null;
        }
    }
}
