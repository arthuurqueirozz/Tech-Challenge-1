namespace FCG.Domain.Dtos.Models.Catalog;

public sealed class GameDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Developer { get; set; }
    public decimal FullPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public bool IsOnSale { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
