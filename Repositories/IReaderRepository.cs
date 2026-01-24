using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IReaderRepository
    {
        List<Reader> GetAll();
        Reader? GetById(int id);
        void Add(Reader reader);
        void Update(Reader reader);
        void Delete(int id);
    }
}