using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.Utils.Core;

/// <summary>
/// 全局服务提供者
/// </summary>
public class GlobalServiceProvider
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static void Set(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public static T GetService<T>()
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");

        return ServiceProvider.GetService<T>();
    }

    public static object GetService(Type serviceType)
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.GetService(serviceType);
    }

    public static T GetKeyedServices<T>(string serviceKey)
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.GetKeyedService<T>(serviceKey);
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.GetRequiredService<T>();
    }

    public static object GetRequiredService(Type serviceType)
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.GetRequiredService(serviceType);
    }

    public static T GetRequiredKeyedService<T>(string serviceKey) where T : notnull
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.GetRequiredKeyedService<T>(serviceKey);
    }

    public static IServiceScope CreateScope()
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.CreateScope();
    }

    public static AsyncServiceScope CreateAsyncScope()
    {
        if (ServiceProvider == null)
            throw new NullReferenceException("The global IServiceProvider is null, please call GlobalServiceProvider.Set");
        return ServiceProvider.CreateAsyncScope();
    }
}