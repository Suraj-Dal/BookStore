using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistML iwishlistML;
        public WishlistController(IWishlistML iwishlistML)
        {
            this.iwishlistML = iwishlistML;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("add")]
        public IActionResult AddWishlist(int BookID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iwishlistML.AddToWishlist(BookID, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Book Added to wishlist." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot add book to wishlist." });
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
        public IActionResult DeleteWishlist(int WishlistID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iwishlistML.DeleteWishlist(WishlistID, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Wishlist deleted successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete Wishlist." });
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
        public IActionResult GetWishlist()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iwishlistML.GetWishlist(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Wishlist got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get wishlist." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
