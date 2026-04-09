using System.Security.Claims;
using FCG.Application.Shared.Extensions;
using FCG.Domain.Interfaces;

namespace FCG.Application.Controllers;

public static class LibraryEndpoints
{
    public static IEndpointRouteBuilder MapLibraryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/me").RequireAuthorization().WithTags("Me");

        group.MapGet("/games", GetMyGamesAsync);
        group.MapPost("/games/{gameId:guid}", AcquireGameAsync);

        return app;
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
