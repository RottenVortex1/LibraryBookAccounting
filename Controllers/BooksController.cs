using LibraryBookAccounting.Models;
using LibraryBookAccounting.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookAccounting.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAll()
    {
        var books = await _service.GetAllAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById(Guid id)
    {
        var book = await _service.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book book)
    {
        var created = await _service.CreateAsync(book);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Book book)
    {
        var updated = await _service.UpdateAsync(id, book);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, BookStatus status)
    {
        var changed = await _service.ChangeStatusAsync(id, status);

        if (!changed)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}