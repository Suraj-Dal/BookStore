using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderModel orderModel, int UserID);
        public bool CancelOrder(int OrderID);
        public IEnumerable<GetOrderModel> GetAllOrders();
    }
}
