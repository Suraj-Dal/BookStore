using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IAddressML
    {
        public bool AddAddress(AddressModel addressModel, int UserID);
        public bool UpdateAddress(AddressModel addressModel, int AddressID);
        public IEnumerable<GetAddressModel> GetAddress(int UserID);
    }
}
