using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IUserML
    {
        public bool Register(UserRegistrationModel userDetails);
        public string UserLogin(LoginModel loginModel);
        public bool ResetPassword(ResetModel resetModel, string EmailID);
        public string ForgetPassword(string Email);
    }
}
