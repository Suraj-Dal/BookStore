using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class UserML : IUserML
    {
        private readonly IUserRL iuserRL;
        public UserML(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public bool Register(UserRegistrationModel userDetails)
        {
            try
            {
                return iuserRL.Register(userDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UserLogin(LoginModel loginModel)
        {
            try
            {
                return iuserRL.UserLogin(loginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(ResetModel resetModel, string EmailID)
        {
            try
            {
                return iuserRL.ResetPassword(resetModel, EmailID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgetPassword(string Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
