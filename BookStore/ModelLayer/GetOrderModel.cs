using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class GetOrderModel
    {
        public string OrderID { get; set; }
        public string OrderQty { get; set; }
        public string TotalPrice { get; set; }
        public string BookID { get; set; }
        public string AddressID { get; set; }
        public string UserID { get; set; }
        public string DateTime { get; set; }
    }
}
