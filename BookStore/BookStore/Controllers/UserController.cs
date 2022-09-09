using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserML iuserML;
        public UserController(IUserML iuserML)
        {
            this.iuserML = iuserML;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(UserRegistrationModel userModel)
        {
            try
            {
                var result = iuserML.Register(userModel);
                if (result)
                {
                    return Ok(new { success = true, message = "Registration successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                var result = iuserML.UserLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result});
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

        [Authorize]
        [HttpPost]
        [Route("reset")]
        public IActionResult ResetPassword(ResetModel resetModel)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserML.ResetPassword(resetModel, Email);
                if (result)
                {
                    return Ok(new { success = true, message = "Password reset Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password cannot be reset." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("forget")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = iuserML.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset mail sent successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset mail couldn't be send" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
