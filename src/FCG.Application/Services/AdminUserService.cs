using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Interfaces;
using FCG.Domain.Interfaces.Identity;
using FCG.Domain.Shared;
using FCG.Infrastructure.Entities.Identity;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Services;

public sealed class AdminUserService : IAdminUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminUserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<UserDto>> ListUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.ListAsync(cancellationToken);
        return users.Select(UserMapper.ToDto).ToList();
    }

    public async Task PromoteToAdminAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new DomainValidationException("User not found.");

        if (user.Role == UserRole.Admin)
            return;

        user.PromoteToAdmin();
        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
