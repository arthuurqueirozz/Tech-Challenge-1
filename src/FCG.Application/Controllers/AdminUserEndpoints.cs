using System.Security.Claims;
using FCG.Application.Shared.Extensions;
using FCG.Domain.Interfaces.Identity;

namespace FCG.Application.Controllers;

public static class AdminUserEndpoints
{
    public static IEndpointRouteBuilder MapAdminUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/admin/users").RequireAuthorization("Admin").WithTags("Identity");

        group.MapGet("/", ListUsersAsync);
        group.MapPost("/{userId:guid}/promote", PromoteUserAsync);

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
}
