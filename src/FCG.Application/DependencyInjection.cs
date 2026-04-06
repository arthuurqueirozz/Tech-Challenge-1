using FluentValidation;
using FCG.Application.Services;
using FCG.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IGameCatalogService, GameCatalogService>();
        services.AddScoped<IUserLibraryService, UserLibraryService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IPromotionService, PromotionService>();

        return services;
    }
}
