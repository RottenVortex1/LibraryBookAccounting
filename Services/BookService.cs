using LibraryBookAccounting.Models;
using LibraryBookAccounting.Repositories;

namespace LibraryBookAccounting.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Book>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
        book.Id = Guid.NewGuid();
        book.AddedAt = DateTime.Now;
        book.Status = BookStatus.Available;

        await _repository.AddAsync(book);

        return book;
    }

    public async Task<bool> UpdateAsync(Guid id, Book book)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
        {
            return false;
        }

        book.Id = id;

        await _repository.UpdateAsync(book);

        return true;
    }

    public async Task<bool> ChangeStatusAsync(Guid id, BookStatus status)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
        {
            return false;
        }

        book.Status = status;

        await _repository.UpdateAsync(book);

        return true;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return _repository.DeleteAsync(id);
    }
}