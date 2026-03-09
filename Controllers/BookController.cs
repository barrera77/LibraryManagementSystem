using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {       

        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        private bool IsAuthenticated()
        {
            var staffId = HttpContext.Session.GetString("StaffId");
            if (string.IsNullOrEmpty(staffId))
            {
                return false;
            }
            return true;
        }

        //Get all
        public IActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }

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
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        //Update
        public IActionResult Update(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();                
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(int id, Book book)
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
        public IActionResult DeleteConfirmed(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}