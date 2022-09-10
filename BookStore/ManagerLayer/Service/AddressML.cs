using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class AddressML : IAddressML
    {
        private readonly IAddressRL iaddressRL;
        public AddressML(IAddressRL iaddressRL)
        {
            this.iaddressRL = iaddressRL;
        }

        public bool AddAddress(AddressModel addressModel, int UserID)
        {
            try
            {
                return iaddressRL.AddAddress(addressModel, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAddress(AddressModel addressModel, int AddressID)
        {
            try
            {
                return iaddressRL.UpdateAddress(addressModel, AddressID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GetAddressModel> GetAddress(int UserID)
        {
            try
            {
                return iaddressRL.GetAddress(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
