using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{   
    public class ReaderController : Controller
    {
        private readonly IReaderRepository _readerRepository;

        public ReaderController(IReaderRepository readerRepository)
        {
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

            var readers = _readerRepository.GetAll();
            return View(readers);        
        }

       public IActionResult Details(int id)
        {
            var reader = _readerRepository.GetById(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);   
        }
       
        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                _readerRepository.Add(reader);
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        //Update
        public IActionResult Update(int id)
        {
            var reader = _readerRepository.GetById(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _readerRepository.Update(reader);
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }


        //Delete
        public IActionResult Delete(int id)
        {
            var reader = _readerRepository.GetById(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _readerRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}