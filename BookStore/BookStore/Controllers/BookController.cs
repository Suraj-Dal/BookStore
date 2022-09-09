using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookML ibookML;
        public BookController(IBookML ibookML)
        {
            this.ibookML = ibookML;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("add")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = ibookML.AddBook(bookModel);
                if (result)
                {
                    return Ok(new { success = true, message = "Successfully added book." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot add book." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateBook(BookModel bookModel, int BookID)
        {
            try
            {
                var result = ibookML.UpdateBook(bookModel, BookID);
                if (result)
                {
                    return Ok(new { success = true, message = "Successfully updated book." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update book." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteBook(int BookID)
        {
            try
            {
                var result = ibookML.DeleteBook(BookID);
                if (result)
                {
                    return Ok(new { success = true, message = "Book deleted successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete book." });
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
        public IActionResult GetBook()
        {
            try
            {
                var result = ibookML.GetAllBooks();
                if (result != null)
                {
                    return Ok(new { success = true, message = "Books got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get book." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("getById")]
        public IActionResult GetBookById(int BookID)
        {
            try
            {
                var result = ibookML.GetBookById(BookID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Book got successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get book." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
