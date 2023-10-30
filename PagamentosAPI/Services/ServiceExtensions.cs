using Microsoft.EntityFrameworkCore;
using PagamentosApi.Data;
#pragma warning disable 1591
namespace PagamentosApi.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomDbContexts(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<PagamentoContext>(opts =>
            opts.UseLazyLoadingProxies().UseNpgsql(connectionString));

        services.AddDbContext<UsuarioDbContext>(opts =>
            opts.UseLazyLoadingProxies().UseNpgsql(connectionString));

        return services;
    }
}
