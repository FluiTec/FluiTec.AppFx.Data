using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data;

/// <summary>   An extensions. </summary>
public static class Extensions
{
    public static IServiceCollection AddDataService<TInterface, TDataService>(this IServiceCollection services) 
        where TInterface : class, IDataService
        where TDataService : class, TInterface
    {
        services.AddSingleton<TDataService>();
        services.AddSingleton<IDataService>(provider => provider.GetRequiredService<TDataService>());
        services.AddSingleton<TInterface>(provider => provider.GetRequiredService<TDataService>());

        return services;
    }
}