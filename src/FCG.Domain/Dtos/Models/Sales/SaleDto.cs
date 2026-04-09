namespace FCG.Domain.Dtos.Models.Sales;

public sealed class SaleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Code { get; set; }
    public decimal DiscountPercent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidUntil { get; set; }
    public Guid GameId { get; set; }
    public DateTime CreatedAt { get; set; }
}
