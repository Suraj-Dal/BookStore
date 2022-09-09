using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class CartML : ICartML
    {
        private readonly ICartRL icartRL;
        public CartML(ICartRL icartRL)
        {
            this.icartRL = icartRL;
        }

        public bool AddToCart(CartModel cartModel, int UserID)
        {
            try
            {
                return icartRL.AddToCart(cartModel, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCart(int CartID, int CartQty, int UserID)
        {
            try
            {
                return icartRL.UpdateCart(CartID, CartQty, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCart(int CartID, int UserID)
        {
            try
            {
                return icartRL.DeleteCart(CartID, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GetCartModel> GetCart(int UserID)
        {
            try
            {
                return icartRL.GetCart(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
