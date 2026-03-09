using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IStaffRepository _staffRepository;

        public AuthController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Staff staff)
        {
            if (ModelState.IsValid)
            {
                var existingStaff = _staffRepository.GetByEmail(staff.Email);
                if (existingStaff != null)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    return View(staff);
                }

                if (staff.Password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password must be at least 6 characters");
                    return View(staff);
                }

                _staffRepository.Add(staff);
                TempData["SuccessMessage"] = "Registration successful! Please login.";
                return RedirectToAction(nameof(Login));
            }
            return View(staff);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and password are required");
                return View();
            }

            var staff = _staffRepository.GetByEmail(email);
            if (staff == null || staff.Password != password)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View();
            }

            HttpContext.Session.SetString("StaffId", staff.Id.ToString());
            HttpContext.Session.SetString("StaffName", staff.Name);

            TempData["SuccessMessage"] = $"Welcome back, {staff.Name}!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Logged out successfully";
            return RedirectToAction(nameof(Login));
        }
    }
}