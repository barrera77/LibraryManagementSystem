using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllReaders()
        {
            return Ok(new { message = "Retrieved all readers successfully" });
        }

        [HttpGet("{id}")]
        public IActionResult GetReaderById(int id)
        {
            return Ok(new { message = $"Retrieved reader with ID: {id}" });
        }

        [HttpPost]
        public IActionResult CreateReader([FromBody] Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Reader created successfully", data = reader });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReader(int id, [FromBody] Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = $"Reader with ID {id} updated successfully", data = reader });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReader(int id)
        {
            return Ok(new { message = $"Reader with ID {id} deleted successfully" });
        }
    }
}