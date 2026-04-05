using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly LibraryContext _context;

        public StaffRepository(LibraryContext context)
        {
            _context = context;
        }

        public Staff? GetByEmail(string email)
        {
            return _context.Staff
                .FirstOrDefault(s => s.Email.ToLower() == email.ToLower());
        }

        public void Add(Staff staff)
        {
            staff.Email = staff.Email.ToLower();
            _context.Staff.Add(staff);
            _context.SaveChanges();
        }
    }
}