using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private static List<Staff> _staff = new List<Staff>();
        private static int _nextId = 1;

        public Staff? GetByEmail(string email)
        {
            return _staff.FirstOrDefault(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Staff staff)
        {
            staff.Id = _nextId++;
            staff.Email = staff.Email.ToLower();
            _staff.Add(staff);
        }
    }
}