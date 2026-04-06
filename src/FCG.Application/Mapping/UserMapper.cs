using FCG.Application.Dtos.Users;
using FCG.Domain.Entities;

namespace FCG.Application.Mapping;

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
