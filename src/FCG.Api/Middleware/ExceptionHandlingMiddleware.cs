using System.Net;
using System.Text.Json;
using FCG.Application.Exceptions;
using FCG.Domain.Exceptions;
using FluentValidation;

namespace FCG.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "FluentValidation rejected request");
            await WriteProblemAsync(
                context,
                HttpStatusCode.BadRequest,
                "Validation failed",
                ex.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));
        }
        catch (DomainValidationException ex)
        {
            _logger.LogWarning(ex, "Domain validation failed");
            await WriteProblemAsync(context, HttpStatusCode.BadRequest, "Invalid request", ex.Message);
        }
        catch (DomainConflictException ex)
        {
            _logger.LogWarning(ex, "Domain conflict");
            await WriteProblemAsync(context, HttpStatusCode.Conflict, "Conflict", ex.Message);
        }
        catch (ApplicationUnauthorizedException ex)
        {
            _logger.LogWarning(ex, "Unauthorized");
            await WriteProblemAsync(context, HttpStatusCode.Unauthorized, "Unauthorized", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteProblemAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Server error",
                "An unexpected error occurred.");
        }
    }

    private async Task WriteProblemAsync(
        HttpContext context,
        HttpStatusCode status,
        string title,
        string detail)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)status;
        var body = new
        {
            type = $"https://httpstatuses.io/{(int)status}",
            title,
            detail,
            traceId = context.TraceIdentifier
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, JsonOptions));
    }

    private async Task WriteProblemAsync(
        HttpContext context,
        HttpStatusCode status,
        string title,
        IDictionary<string, string[]> errors)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)status;
        var body = new
        {
            type = $"https://httpstatuses.io/{(int)status}",
            title,
            errors,
            traceId = context.TraceIdentifier
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, JsonOptions));
    }
}
