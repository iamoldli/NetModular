# 配置项

## 官方文档

[ASP.NET Core 中的配置](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2)

[ASP.NET Core 中的选项模式](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.2)

## 约定

在`NetModular`中，约定所有的配置信息按照其功能放到不同的文件中，所有配置文件放到 config 目录下，而不是把所有配置信息都放在 appsettings.json 文件里面。而且每个配置文件都包括生产环境(db.json)和开发环境(db.Development.json)两个文件。

如下图：

<nm-img id="20190918134933"/>

说明：

> cache.json 存放缓存有关的配置信息

> db.json 存放数据库有关的配置信息

> host.json 存放 WebHost 启动有关的配置信息，如绑定的 IP 和端口号等

> jwt.json 存放 jwt 认证有关的配置信息

> logging.json 存放日志有关的配置信息

> module.json 存放业务模块有关的配置信息

## 读取

在通用库(Utils)里面，提供了`ConfigurationHelper.cs`类，该类里面提供了封装好的、专门用于读取按照上面约定定义的配置文件的方法

```csharp
/// <summary>
/// 根据配置名称加载指定的配置项
/// </summary>
/// <param name="configFileName">配置文件名称，使用约定，配置文件放在项目的config目录中，如logging配置：config/logging.json</param>
/// <param name="environmentName"></param>
/// <param name="reloadOnChange">自动更新</param>
/// <returns></returns>
public IConfiguration Load(string configFileName, string environmentName = "", bool reloadOnChange = false)
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "config");
    if (!Directory.Exists(filePath))
        return null;

    var builder = new ConfigurationBuilder()
        .SetBasePath(filePath)
        .AddJsonFile(configFileName.ToLower() + ".json", true, reloadOnChange);

    if (environmentName.NotNull())
    {
        builder.AddJsonFile(configFileName.ToLower() + "." + environmentName + ".json", true, reloadOnChange);
    }

    return builder.Build();
}

/// <summary>
/// 根据配置名称加载指定的配置项
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="configFileName">配置文件名称，使用约定，配置文件放在项目的config目录中，如logging配置：config/logging.json</param>
/// <param name="environmentName">环境名称</param>
/// <param name="reloadOnChange">自动更新</param>
/// <returns></returns>
public T Get<T>(string configFileName, string environmentName = "", bool reloadOnChange = false)
{
    var configuration = Load(configFileName, environmentName, reloadOnChange);
    if (configuration == null)
        return default;

    return configuration.Get<T>();
}
```

已读取缓存配置信息为例，下面时使用方法

```csharp
/// <summary>
/// 添加缓存
/// </summary>
/// <param name="services"></param>
/// <param name="environmentName"></param>
/// <returns></returns>
public static IServiceCollection AddCache(this IServiceCollection services, string environmentName)
{
    var cfgHelper = new ConfigurationHelper();
    //不需要填写Cache.json，只需要传名称即可，不区分大小写
    var cacheOptions = cfgHelper.Get<CacheOptions>("Cache", environmentName);

    if (cacheOptions == null)
        return services;

    services.AddSingleton(cacheOptions);

    var ns = typeof(ServiceCollectionExtensions).Namespace;

    Check.NotNull(ns, "Namespace is null");

    var name = ns.Replace("Integration", cacheOptions.Mode.ToString());

    var assemblyHelper = new AssemblyHelper();
    var assembly = assemblyHelper.Load(m => m.Name.Equals(name)).FirstOrDefault();

    Check.NotNull(assembly, cacheOptions.Mode + "缓存实现程序集未找到");

    var configType = assembly.GetTypes().FirstOrDefault(m => m.Name.Equals("ServiceCollectionConfig"));
    if (configType != null)
    {
        var instance = (IServiceCollectionConfig)Activator.CreateInstance(configType);
        instance.Config(services, cacheOptions);
    }

    return services;
}
```
