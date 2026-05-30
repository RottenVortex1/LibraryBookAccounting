using LibraryBookAccounting.Models;

namespace LibraryBookAccounting.Services;

public interface IBookService
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(Guid id);

    Task<Book> CreateAsync(Book book);

    Task<bool> UpdateAsync(Guid id, Book book);

    Task<bool> ChangeStatusAsync(Guid id, BookStatus status);

    Task<bool> DeleteAsync(Guid id);
}