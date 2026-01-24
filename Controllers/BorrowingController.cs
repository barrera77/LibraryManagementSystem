using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBorrowings()
        {
            return Ok(new { message = "Retrieved all borrowings successfully" });
        }

        [HttpGet("{id}")]
        public IActionResult GetBorrowingById(int id)
        {
            return Ok(new { message = $"Retrieved borrowing with ID: {id}" });
        }

        [HttpPost]
        public IActionResult CreateBorrowing([FromBody] Borrowing borrowing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Borrowing created successfully", data = borrowing });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBorrowing(int id, [FromBody] Borrowing borrowing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = $"Borrowing with ID {id} updated successfully", data = borrowing });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            return Ok(new { message = $"Borrowing with ID {id} deleted successfully" });
        }
    }
}