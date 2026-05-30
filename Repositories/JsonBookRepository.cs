using System.Text.Json;
using LibraryBookAccounting.Models;

namespace LibraryBookAccounting.Repositories;

public class JsonBookRepository : IBookRepository
{
    private readonly string _filePath = "Data/books.json";

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<Book>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Book>();
        }

        var json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<Book>>(json, _options) ?? new List<Book>();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        var books = await GetAllAsync();

        return books.FirstOrDefault(book => book.Id == id);
    }

    public async Task AddAsync(Book book)
    {
        var books = await GetAllAsync();

        books.Add(book);

        await SaveAsync(books);
    }

    public async Task UpdateAsync(Book book)
    {
        var books = await GetAllAsync();

        var index = books.FindIndex(item => item.Id == book.Id);

        if (index >= 0)
        {
            books[index] = book;
        }

        await SaveAsync(books);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var books = await GetAllAsync();

        var removed = books.RemoveAll(book => book.Id == id) > 0;

        await SaveAsync(books);

        return removed;
    }

    private async Task SaveAsync(List<Book> books)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        var json = JsonSerializer.Serialize(books, _options);

        await File.WriteAllTextAsync(_filePath, json);
    }
}