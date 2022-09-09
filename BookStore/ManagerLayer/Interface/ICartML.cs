using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface ICartML
    {
        public bool AddToCart(CartModel cartModel, int UserID);
        public bool UpdateCart(int CartID, int CartQty, int UserID);
        public bool DeleteCart(int CartID, int UserID);
        public IEnumerable<GetCartModel> GetCart(int UserID);
    }
}
