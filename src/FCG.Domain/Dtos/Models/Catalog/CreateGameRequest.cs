namespace FCG.Domain.Dtos.Models.Catalog;

public sealed class CreateGameRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Developer { get; set; }
    public decimal Price { get; set; }
}
