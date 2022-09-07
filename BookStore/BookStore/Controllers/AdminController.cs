using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminML iadminML;
        public AdminController(IAdminML iadminML)
        {
            this.iadminML = iadminML;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult AdminLogin(LoginModel loginModel)
        {
            try
            {
                var result = iadminML.AdminLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Logged in as Admin.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
