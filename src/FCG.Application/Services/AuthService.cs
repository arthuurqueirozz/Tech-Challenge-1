using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces;
using FCG.Domain.Interfaces.Identity;
using FCG.Domain.Shared;
using FCG.Domain.Entities.Identity;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(request.Email);
        var existing = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (existing is not null)
            throw new DomainConflictException("Email is already registered.");

        PasswordPolicy.ValidateOrThrow(request.Password);
        var hash = _passwordHasher.Hash(request.Password);
        var user = User.Create(request.Name, email, hash, UserRole.User);
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenService.CreateToken(user.Id, user.Email.Value, user.Role.ToString());
        return ToAuthResponse(token.AccessToken, token.ExpiresAt, user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(request.Email);
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new ApplicationUnauthorizedException();

        var token = _jwtTokenService.CreateToken(user.Id, user.Email.Value, user.Role.ToString());
        return ToAuthResponse(token.AccessToken, token.ExpiresAt, user);
    }

    private static AuthResponse ToAuthResponse(string accessToken, DateTime expiresAt, User user) =>
        new()
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            User = UserMapper.ToDto(user)
        };
}
