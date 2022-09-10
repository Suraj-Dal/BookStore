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
    public class AddressController : ControllerBase
    {
        private readonly IAddressML iaddressML;
        public AddressController(IAddressML iaddressML)
        {
            this.iaddressML = iaddressML;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("add")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iaddressML.AddAddress(addressModel, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Address Added successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot add address." });
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
        public IActionResult UpdateAddress(AddressModel addressModel, int AddressID)
        {
            try
            {
                var result = iaddressML.UpdateAddress(addressModel, AddressID);
                if (result)
                {
                    return Ok(new { success = true, message = "Successfully updated address." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update address." });
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
        public IActionResult GetAddress()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iaddressML.GetAddress(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Address got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get address." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
