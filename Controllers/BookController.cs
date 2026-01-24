using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {       
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(new { message = "Retrieved all books successfully" });
        }
       
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            return Ok(new { message = $"Retrieved book with ID: {id}" });
        }
     
        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Book created successfully", data = book });
        }
       
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = $"Book with ID {id} updated successfully", data = book });
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            return Ok(new { message = $"Book with ID {id} deleted successfully" });
        }
    }
}