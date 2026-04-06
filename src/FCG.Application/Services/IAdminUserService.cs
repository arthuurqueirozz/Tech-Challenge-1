using FCG.Application.Dtos.Users;

namespace FCG.Application.Services;

public interface IAdminUserService
{
    Task<IReadOnlyList<UserDto>> ListUsersAsync(CancellationToken cancellationToken = default);
    Task PromoteToAdminAsync(Guid userId, CancellationToken cancellationToken = default);
}
