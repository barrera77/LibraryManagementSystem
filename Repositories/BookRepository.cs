using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Add(Book book)
        {
            // Your business logic
            book.AvailableCopies = book.TotalCopies;
            book.IsAvailable = book.AvailableCopies > 0;

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            var existingBook = GetById(book.Id);
            if (existingBook != null)
            {
                // Your business logic - update fields
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.Publisher = book.Publisher;
                existingBook.PublicationYear = book.PublicationYear;
                existingBook.Category = book.Category;
                existingBook.TotalCopies = book.TotalCopies;
                existingBook.AvailableCopies = book.AvailableCopies;
                existingBook.IsAvailable = book.AvailableCopies > 0;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}