using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Entities.Identity;

namespace FCG.Infrastructure.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(User user) =>
        new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Value,
            Role = user.Role.ToString()
        };
}
