using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private static List<Borrowing> _borrowings = new List<Borrowing>();
        private static int _nextId = 1;

        public List<Borrowing> GetAll()
        {
            return _borrowings;
        }

        public Borrowing? GetById(int id)
        {
            return _borrowings.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Borrowing borrowing)
        {
            borrowing.Id = _nextId++;
            borrowing.BorrowDate = DateTime.UtcNow;
            borrowing.Status = "Active";
            _borrowings.Add(borrowing);
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
            }
        }

        public void Delete(int id)
        {
            var borrowing = GetById(id);
            if (borrowing != null)
            {
                _borrowings.Remove(borrowing);
            }
        }
    }
}