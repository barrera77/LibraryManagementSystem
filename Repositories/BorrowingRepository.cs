using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly LibraryContext _context;

        public BorrowingRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<Borrowing> GetAll()
        {
            return _context.Borrowings.ToList();
        }

        public Borrowing? GetById(int id)
        {
            return _context.Borrowings.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Borrowing borrowing)
        {
            borrowing.BorrowDate = DateTime.UtcNow;
            borrowing.Status = "Active";

            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }

        public void Update(Borrowing borrowing)
        {
            var existingBorrowing = GetById(borrowing.Id);

            if (existingBorrowing != null)
            {
                existingBorrowing.BookId = borrowing.BookId;
                existingBorrowing.ReaderId = borrowing.ReaderId;
                existingBorrowing.ReturnDate = borrowing.ReturnDate;
                existingBorrowing.Status = borrowing.Status;
                existingBorrowing.OverdueDays = borrowing.OverdueDays;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var borrowing = GetById(id);

            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                _context.SaveChanges();
            }
        }
    }
}