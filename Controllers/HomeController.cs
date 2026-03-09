using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IBorrowingRepository _borrowingRepository;

        public HomeController(IBookRepository bookRepository,
                             IReaderRepository readerRepository,
                             IBorrowingRepository borrowingRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
            _borrowingRepository = borrowingRepository;
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


        //Home/Index 
        public IActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }


            ViewBag.TotalBooks = _bookRepository.GetAll().Count;
            ViewBag.TotalReaders = _readerRepository.GetAll().Count;
            ViewBag.ActiveBorrowings = _borrowingRepository.GetAll().Count(b => b.Status == "Active");
            ViewBag.AvailableBooks = _bookRepository.GetAll().Sum(b => b.AvailableCopies);

            return View();
        }
    }
}