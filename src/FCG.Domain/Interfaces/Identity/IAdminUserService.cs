using FCG.Domain.Dtos.Models.Identity;

namespace FCG.Domain.Interfaces.Identity;

public interface IAdminUserService
{
    Task<IReadOnlyList<UserDto>> ListUsersAsync(CancellationToken cancellationToken = default);
    Task PromoteToAdminAsync(Guid userId, CancellationToken cancellationToken = default);
}
