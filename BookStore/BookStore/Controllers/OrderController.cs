using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderML iorderML;
        public OrderController(IOrderML iorderML)
        {
            this.iorderML = iorderML;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("add")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iorderML.AddOrder(orderModel, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Successfully placed order." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot place order." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteOrder(int OrderID)
        {
            try
            {
                var result = iorderML.CancelOrder(OrderID);
                if (result)
                {
                    return Ok(new { success = true, message = "Order cancelled successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot cancel order." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public IActionResult GetOrder()
        {
            try
            {
                var result = iorderML.GetAllOrders();
                if (result != null)
                {
                    return Ok(new { success = true, message = "Orders got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get order." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
