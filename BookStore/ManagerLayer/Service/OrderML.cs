using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class OrderML : IOrderML
    {

        private readonly IOrderRL iorderRL;
        public OrderML(IOrderRL iorderRL)
        {
            this.iorderRL = iorderRL;
        }

        public bool AddOrder(OrderModel orderModel, int UserID)
        {
            try
            {
                return iorderRL.AddOrder(orderModel, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CancelOrder(int OrderID)
        {
            try
            {
                return iorderRL.CancelOrder(OrderID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GetOrderModel> GetAllOrders()
        {
            try
            {
                return iorderRL.GetAllOrders();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
