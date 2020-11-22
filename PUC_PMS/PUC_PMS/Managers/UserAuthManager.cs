using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Gateways;
namespace PUC_PMS.Managers
{
    public class UserAuthManager
    {
        private LogInGateway LogInGateway;
        public UserAuthManager()
        {
            LogInGateway = new LogInGateway();
        }

        // check authentication
        public LogIn CheckAuthentication(LogIn logIn, string masterPassword)
        {
            LogIn user = LogInGateway.UserLogin(logIn.Roll,logIn.Type);

            if (user != null)
            {
                int salt = user.Salt;
                string storedpassword = user.Password;

                Password pwd = new Password(logIn.Password, salt);
                if (pwd.ComputeSaltedHash() == storedpassword || masterPassword == logIn.Password)
                {
                    return user;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}