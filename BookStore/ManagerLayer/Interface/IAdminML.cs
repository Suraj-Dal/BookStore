using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IAdminML
    {
        public string AdminLogin(LoginModel loginModel);
    }
}
