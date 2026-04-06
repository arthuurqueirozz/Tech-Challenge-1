using FCG.Application.Dtos.Users;

namespace FCG.Application.Services;

public interface IUserProfileService
{
    Task<UserDto?> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
}
