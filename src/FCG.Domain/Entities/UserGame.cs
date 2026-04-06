namespace FCG.Domain.Entities;

public class UserGame
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid GameId { get; private set; }
    public DateTime AcquiredAt { get; private set; }

    public User User { get; private set; } = null!;
    public Game Game { get; private set; } = null!;

    private UserGame()
    {
    }

    public static UserGame Create(Guid userId, Guid gameId)
    {
        return new UserGame
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GameId = gameId,
            AcquiredAt = DateTime.UtcNow
        };
    }
}
