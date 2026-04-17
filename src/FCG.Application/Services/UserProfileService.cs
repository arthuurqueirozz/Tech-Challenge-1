using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Interfaces.Identity;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Services;

public sealed class UserProfileService : IUserProfileService
{
    private readonly IUserRepository _userRepository;

    public UserProfileService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        return user is null ? null : UserMapper.ToDto(user);
    }
}
