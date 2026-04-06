namespace FCG.Application.Dtos.Games;

public sealed class CreateGameRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Developer { get; set; }
}
