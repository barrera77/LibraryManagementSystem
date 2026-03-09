using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        
        public BorrowingController(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _borrowingRepository = borrowingRepository;
            _readerRepository = readerRepository;
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

            var borrowings = _borrowingRepository.GetAll();
            var books = _bookRepository.GetAll();
            var readers = _readerRepository.GetAll();

            ViewBag.Books = books.ToDictionary(b => b.Id, b => b.Title);
            ViewBag.Readers = readers.ToDictionary(r => r.Id, r => $"{r.FirstName} {r.LastName}");

            return View(borrowings);
        }

        //Details
        public IActionResult Details(int id)
        {
            var borrowing = _borrowingRepository.GetById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            var book = _bookRepository.GetById(borrowing.BookId);
            var reader = _readerRepository.GetById(borrowing.ReaderId);

            return View(borrowing);
        }

        //Create
        public IActionResult Create()
        {
            var books = _bookRepository.GetAll().Where(b => b.AvailableCopies > 0);
            var readers = _readerRepository.GetAll();

            ViewBag.Books = new SelectList(books, "Id", "Title");
            ViewBag.Readers = new SelectList(readers.Select(r => new
            {
                Id = r.Id,
                FullName = $"{r.FirstName} {r.LastName}"
            }), "Id", "FullName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                var book = _bookRepository.GetById(borrowing.BookId);
                if (book != null && book.AvailableCopies > 0)
                {
                    book.AvailableCopies--;
                    book.IsAvailable = book.AvailableCopies > 0;
                    _bookRepository.Update(book);
                    _borrowingRepository.Add(borrowing);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Book is not available");
            }

            var books = _bookRepository.GetAll().Where(b => b.AvailableCopies > 0);
            var readers = _readerRepository.GetAll();
            ViewBag.Books = new SelectList(books, "Id", "Title");
            ViewBag.Readers = new SelectList(readers.Select(r => new
            {
                Id = r.Id,
                FullName = $"{r.FirstName} {r.LastName}"
            }), "Id", "FullName");

            return View(borrowing);
        }

        //Update
        public IActionResult Update(int id)
        {
            var borrowing = _borrowingRepository.GetById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            var books = _bookRepository.GetAll();
            var readers = _readerRepository.GetAll();

            ViewBag.Books = new SelectList(books, "Id", "Title", borrowing.BookId);
            ViewBag.Readers = new SelectList(readers.Select(r => new
            {
                Id = r.Id,
                FullName = $"{r.FirstName} {r.LastName}"
            }), "Id", "FullName", borrowing.ReaderId);

            return View(borrowing);
        }

        [HttpPost]
        public IActionResult Update(int id, Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _borrowingRepository.Update(borrowing);
                return RedirectToAction(nameof(Index));
            }

            var books = _bookRepository.GetAll();
            var readers = _readerRepository.GetAll();
            ViewBag.Books = new SelectList(books, "Id", "Title", borrowing.BookId);
            ViewBag.Readers = new SelectList(readers.Select(r => new
            {
                Id = r.Id,
                FullName = $"{r.FirstName} {r.LastName}"
            }), "Id", "FullName", borrowing.ReaderId);

            return View(borrowing);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var borrowing = _borrowingRepository.GetById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            var book = _bookRepository.GetById(borrowing.BookId);
            var reader = _readerRepository.GetById(borrowing.ReaderId);

            ViewBag.BookTitle = book?.Title ?? "Unknown";
            ViewBag.ReaderName = reader != null ? $"{reader.FirstName} {reader.LastName}" : "Unknown";

            return View(borrowing);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var borrowing = _borrowingRepository.GetById(id);
            if (borrowing != null && borrowing.Status == "Active")
            {
                var book = _bookRepository.GetById(borrowing.BookId);
                if (book != null)
                {
                    book.AvailableCopies++;
                    book.IsAvailable = book.AvailableCopies > 0;
                    _bookRepository.Update(book);
                }
            }

            _borrowingRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

         //Return
        [HttpPost]
        public IActionResult Return(int id)
        {
            var borrowing = _borrowingRepository.GetById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            borrowing.ReturnDate = DateTime.UtcNow;
            borrowing.Status = "Returned";

            var book = _bookRepository.GetById(borrowing.BookId);
            if (book != null)
            {
                book.AvailableCopies++;
                book.IsAvailable = book.AvailableCopies > 0;
                _bookRepository.Update(book);
            }

            _borrowingRepository.Update(borrowing);
            return RedirectToAction(nameof(Index));
        }    
    }
}