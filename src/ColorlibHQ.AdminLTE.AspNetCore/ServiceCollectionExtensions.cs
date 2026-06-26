using ColorlibHQ.AdminLTE.AspNetCore.Menu;
using ColorlibHQ.AdminLTE.AspNetCore.Theming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ColorlibHQ.AdminLTE.AspNetCore;

/// <summary>
/// Registration helpers for the AdminLTE services (menu, theming, options).
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds AdminLTE services and binds <see cref="AdminLteOptions"/> from the
    /// <c>"AdminLte"</c> configuration section.
    /// </summary>
    public static IServiceCollection AddAdminLte(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AdminLteOptions>()
            .Bind(configuration.GetSection(AdminLteOptions.SectionName))
            .ValidateOnStart();

        return services.AddAdminLteCore();
    }

    /// <summary>
    /// Adds AdminLTE services and configures <see cref="AdminLteOptions"/> in code.
    /// </summary>
    public static IServiceCollection AddAdminLte(
        this IServiceCollection services, Action<AdminLteOptions> configure)
    {
        services.AddOptions<AdminLteOptions>().Configure(configure).ValidateOnStart();
        return services.AddAdminLteCore();
    }

    private static IServiceCollection AddAdminLteCore(this IServiceCollection services)
    {
        services.AddAuthorizationCore();
        services.TryAddScoped<IMenuService, MenuService>();
        // One ColorModeService per Blazor circuit / request scope.
        services.TryAddScoped<ColorModeService>();
        return services;
    }
}
