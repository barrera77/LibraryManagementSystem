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

        //Home/Index 
        public IActionResult Index()
        {
            ViewBag.TotalBooks = _bookRepository.GetAll().Count;
            ViewBag.TotalReaders = _readerRepository.GetAll().Count;
            ViewBag.ActiveBorrowings = _borrowingRepository.GetAll().Count(b => b.Status == "Active");
            ViewBag.AvailableBooks = _bookRepository.GetAll().Sum(b => b.AvailableCopies);

            return View();
        }
    }
}