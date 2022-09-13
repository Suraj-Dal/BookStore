using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IOrderML
    {
        public bool AddOrder(OrderModel orderModel, int UserID);
        public bool CancelOrder(int OrderID);
        public IEnumerable<GetOrderModel> GetAllOrders();
    }
}
