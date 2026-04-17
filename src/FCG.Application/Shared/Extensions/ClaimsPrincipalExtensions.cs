using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FCG.Application.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var sub = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(sub, out var id) ? id : null;
    }
}
