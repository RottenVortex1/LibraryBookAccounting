using LibraryBookAccounting.Models;

namespace LibraryBookAccounting.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(Guid id);

    Task AddAsync(Book book);

    Task UpdateAsync(Book book);

    Task<bool> DeleteAsync(Guid id);
}