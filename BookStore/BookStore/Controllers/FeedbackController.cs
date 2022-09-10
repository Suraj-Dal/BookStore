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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackML ifeedbackML;
        public FeedbackController(IFeedbackML ifeedbackML)
        {
            this.ifeedbackML = ifeedbackML;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("add")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ifeedbackML.AddFeedback(feedbackModel, UserID);
                if (result)
                {
                    return Ok(new { success = true, message = "Feedback added successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot add feedback." });
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
        public IActionResult GetFeedback()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ifeedbackML.GetFeedback(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Feedback got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get feedback." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
