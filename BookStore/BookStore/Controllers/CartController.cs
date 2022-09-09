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
    public class CartController : ControllerBase
    {
        private readonly ICartML icartML;
        public CartController(ICartML icartML)
        {
            this.icartML = icartML;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("add")]
        public IActionResult AddCart(CartModel cartModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartML.AddToCart(cartModel, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Book Added to cart." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot add book to cart." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCart(int CartID, int CartQty)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartML.UpdateCart(CartID, CartQty, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Successfully updated cart." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update cart." });
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
        public IActionResult DeleteCart(int CartID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartML.DeleteCart(CartID, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Cart deleted successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete cart." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("get")]
        public IActionResult GetCart()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartML.GetCart(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Cart got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get cart." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
