using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class AdminML : IAdminML
    {
        private readonly IAdminRL iadminRL;
        public AdminML(IAdminRL iadminRL)
        {
            this.iadminRL = iadminRL;
        }

        public string AdminLogin(LoginModel loginModel)
        {
            try
            {
                return iadminRL.AdminLogin(loginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
