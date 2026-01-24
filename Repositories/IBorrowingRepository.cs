using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBorrowingRepository
    {
        List<Borrowing> GetAll();
        Borrowing? GetById(int id);
        void Add(Borrowing borrowing);
        void Update(Borrowing borrowing);
        void Delete(int id);
    }
}