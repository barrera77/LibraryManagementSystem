using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {       

        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //Get all
        public IActionResult Index()
        {
            var books = _bookRepository.GetAll();
            return View(books);
        }

        //Details
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);

        }

        //Create
        public IActionResult CreateBook()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        //Update
        public IActionResult UpdateBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();                
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bookRepository.Update(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteBook(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}