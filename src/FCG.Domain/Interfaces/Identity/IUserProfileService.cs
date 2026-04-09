using FCG.Domain.Dtos.Models.Identity;

namespace FCG.Domain.Interfaces.Identity;

public interface IUserProfileService
{
    Task<UserDto?> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
}
