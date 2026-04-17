using FCG.Domain.Dtos.Models.Catalog;
using FCG.Domain.Interfaces;
using FluentValidation;

namespace FCG.Application.Controllers;

public static class GamesEndpoints
{
    public static IEndpointRouteBuilder MapGamesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/games").WithTags("Games");

        group.MapGet("/", ListAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost("/", CreateAsync).RequireAuthorization("Admin");

        return app;
    }

    private static async Task<IResult> ListAsync(IGameCatalogService games, CancellationToken cancellationToken)
    {
        var list = await games.ListAsync(cancellationToken);
        return Results.Ok(list);
    }

    private static async Task<IResult> GetByIdAsync(Guid id, IGameCatalogService games, CancellationToken cancellationToken)
    {
        var game = await games.GetByIdAsync(id, cancellationToken);
        return game is null ? Results.NotFound() : Results.Ok(game);
    }

    private static async Task<IResult> CreateAsync(
        CreateGameRequest request,
        IValidator<CreateGameRequest> validator,
        IGameCatalogService games,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var created = await games.CreateAsync(request, cancellationToken);
        return Results.Created($"/api/games/{created.Id}", created);
    }
}
