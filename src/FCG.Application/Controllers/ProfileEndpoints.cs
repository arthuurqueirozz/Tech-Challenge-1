using System.Security.Claims;
using FCG.Application.Shared.Extensions;
using FCG.Domain.Interfaces.Identity;

namespace FCG.Application.Controllers;

public static class ProfileEndpoints
{
    public static IEndpointRouteBuilder MapProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/me").RequireAuthorization().WithTags("Identity");

        group.MapGet("/profile", GetProfileAsync);

        return app;
    }

    private static async Task<IResult> GetProfileAsync(
        ClaimsPrincipal user,
        IUserProfileService profileService,
        CancellationToken cancellationToken)
    {
        var id = user.GetUserId();
        if (id is null)
            return Results.Unauthorized();

        var profile = await profileService.GetProfileAsync(id.Value, cancellationToken);
        return profile is null ? Results.NotFound() : Results.Ok(profile);
    }
}
