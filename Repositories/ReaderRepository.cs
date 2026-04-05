using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly LibraryContext _context;

        public ReaderRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<Reader> GetAll()
        {
            return _context.Readers.ToList();
        }

        public Reader? GetById(int id)
        {
            return _context.Readers.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Reader reader)
        {
            _context.Readers.Add(reader);
            _context.SaveChanges();
        }

        public void Update(Reader reader)
        {
            var existingReader = _context.Readers.FirstOrDefault(r => r.Id == reader.Id);

            if (existingReader != null)
            {
                existingReader.FirstName = reader.FirstName;
                existingReader.LastName = reader.LastName;
                existingReader.Email = reader.Email;
                existingReader.PhoneNumber = reader.PhoneNumber;
                existingReader.Address = reader.Address;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var reader = _context.Readers.FirstOrDefault(r => r.Id == id);

            if (reader != null)
            {
                _context.Readers.Remove(reader);
                _context.SaveChanges();
            }
        }
    }
}