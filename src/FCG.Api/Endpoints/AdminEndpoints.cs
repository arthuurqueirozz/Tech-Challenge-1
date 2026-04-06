using System.Security.Claims;
using FCG.Api.Extensions;
using FCG.Application.Dtos.Promotions;
using FCG.Application.Services;
using FluentValidation;

namespace FCG.Api.Endpoints;

public static class AdminEndpoints
{
    public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/admin").RequireAuthorization("Admin").WithTags("Admin");

        group.MapGet("/users", ListUsersAsync);
        group.MapPost("/users/{userId:guid}/promote", PromoteUserAsync);
        group.MapGet("/promotions", ListPromotionsAsync);
        group.MapPost("/promotions", CreatePromotionAsync);

        return app;
    }

    private static async Task<IResult> ListUsersAsync(
        IAdminUserService admin,
        CancellationToken cancellationToken)
    {
        var users = await admin.ListUsersAsync(cancellationToken);
        return Results.Ok(users);
    }

    private static async Task<IResult> PromoteUserAsync(
        Guid userId,
        ClaimsPrincipal actor,
        IAdminUserService admin,
        CancellationToken cancellationToken)
    {
        var actorId = actor.GetUserId();
        if (actorId == userId)
            return Results.BadRequest(new { error = "Cannot change your own role with this operation." });

        await admin.PromoteToAdminAsync(userId, cancellationToken);
        return Results.NoContent();
    }

    private static async Task<IResult> ListPromotionsAsync(
        IPromotionService promotions,
        CancellationToken cancellationToken)
    {
        var list = await promotions.ListAsync(cancellationToken);
        return Results.Ok(list);
    }

    private static async Task<IResult> CreatePromotionAsync(
        CreatePromotionRequest request,
        IValidator<CreatePromotionRequest> validator,
        IPromotionService promotions,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var created = await promotions.CreateAsync(request, cancellationToken);
        return Results.Created($"/api/admin/promotions/{created.Id}", created);
    }
}
