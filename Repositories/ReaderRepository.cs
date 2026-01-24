using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private static List<Reader> _readers = new List<Reader>();
        private static int _nextId = 1;

        public List<Reader> GetAll()
        {
            return _readers;
        }

        public Reader? GetById(int id)
        {
            return _readers.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Reader reader)
        {
            reader.Id = _nextId++;
            _readers.Add(reader);
        }

        public void Update(Reader reader)
        {
            var existingReader = GetById(reader.Id);
            if (existingReader != null)
            {
                existingReader.FirstName = reader.FirstName;
                existingReader.LastName = reader.LastName;
                existingReader.Email = reader.Email;
                existingReader.PhoneNumber = reader.PhoneNumber;
                existingReader.Address = reader.Address;
            }
        }

        public void Delete(int id)
        {
            var reader = GetById(id);
            if (reader != null)
            {
                _readers.Remove(reader);
            }
        }
    }
}