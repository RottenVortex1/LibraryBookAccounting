namespace LibraryBookAccounting.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int PublishYear { get; set; }

    public string InventoryNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime AddedAt { get; set; } = DateTime.Now;

    public BookStatus Status { get; set; } = BookStatus.Available;
}