using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IStaffRepository
    {
        Staff? GetByEmail(string email);
        void Add(Staff staff);
    }
}