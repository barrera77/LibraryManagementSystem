using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;

        public List<Book> GetAll()
        {
            return _books;
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Book book)
        {
            book.Id = _nextId++;
            book.AvailableCopies = book.TotalCopies;
            book.IsAvailable = book.AvailableCopies > 0;
            _books.Add(book);
        }

        public void Update(Book book)
        {
            var existingBook = GetById(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.Publisher = book.Publisher;
                existingBook.PublicationYear = book.PublicationYear;
                existingBook.Category = book.Category;
                existingBook.TotalCopies = book.TotalCopies;
                existingBook.AvailableCopies = book.AvailableCopies;
                existingBook.IsAvailable = book.AvailableCopies > 0;
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}