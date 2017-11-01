using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
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
        public static UserInfo SignIn(string username, string password)
        {

            var user = DataAccess.Db.UserInfos.FirstOrDefault(o => o.UserName == username && o.Password == password);


            return user;
        }


    }
}
