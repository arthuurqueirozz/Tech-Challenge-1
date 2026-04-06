using System.Security.Claims;
using FCG.Api.Extensions;
using FCG.Application.Services;

namespace FCG.Api.Endpoints;

public static class MeEndpoints
{
    public static IEndpointRouteBuilder MapMeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/me").RequireAuthorization().WithTags("Me");

        group.MapGet("/profile", GetProfileAsync);
        group.MapGet("/games", GetMyGamesAsync);
        group.MapPost("/games/{gameId:guid}", AcquireGameAsync);

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

    private static async Task<IResult> GetMyGamesAsync(
        ClaimsPrincipal user,
        IUserLibraryService library,
        CancellationToken cancellationToken)
    {
        var id = user.GetUserId();
        if (id is null)
            return Results.Unauthorized();

        var games = await library.GetMyGamesAsync(id.Value, cancellationToken);
        return Results.Ok(games);
    }

    private static async Task<IResult> AcquireGameAsync(
        Guid gameId,
        ClaimsPrincipal user,
        IUserLibraryService library,
        CancellationToken cancellationToken)
    {
        var id = user.GetUserId();
        if (id is null)
            return Results.Unauthorized();

        await library.AcquireGameAsync(id.Value, gameId, cancellationToken);
        return Results.NoContent();
    }
}
