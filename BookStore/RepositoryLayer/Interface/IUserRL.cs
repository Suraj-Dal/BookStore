using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public bool Register(UserRegistrationModel userDetails);
        public string UserLogin(LoginModel loginModel);
        public bool ResetPassword(ResetModel resetModel, string EmailID);
        public string ForgetPassword(string Email);
    }
}
