# 对象映射

对象映射功能使用的是[AutoMapper](http://automapper.org/)，AutoMapper 功能很强大，但是它有个确定就是需要先配置好映射关系，才能够使用映射功能 **(也许是我自己没找到 o(_￣ ▽ ￣_)ブ)**。

为了能够方便的管理配置映射关系，所以做了一个这样的约定

### 1、定义 IMapperConfig 接口，该接口包含一个 Bind 方法

```csharp
using AutoMapper;

namespace Nm.Lib.Mapper.AutoMapper
{
    /// <summary>
    /// 对象映射绑定
    /// </summary>
    public interface IMapperConfig
    {
        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="cfg"></param>
        void Bind(IMapperConfigurationExpression cfg);
    }
}
```

### 2、在 Application 层里面，每个 Service 里面创建一个 MapperConfig.cs 类，并集成上面的 IMapperConfig 接口，然后将该 Service 有关的绑定关系定义在 Bind 方法里

Note：把文件名改为\_MapperConfig，这样该文件会始终位于顶部，方便查找~

以角色相关为例，角色 Entity 与 ViewModel 之间的映射关系：

```csharp
public class MapperConfig : IMapperConfig
{
    public void Bind(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<RoleAddModel, RoleEntity>();
        cfg.CreateMap<RoleEntity, RoleUpdateModel>();
        cfg.CreateMap<RoleUpdateModel, RoleEntity>();
        cfg.CreateMap<RoleMenuButtonBindModel, RoleMenuButtonEntity>();
    }
}
```

### 3、将所有绑定关系注册到系统中

现在只是在每个 MapperConfig.cs 类中定义了映射关系，这样是没办法使用的，AutoMapper 需要创建`MapperConfiguration`的实例，并通过`CreateMapper`方法创建映射器`IMapper`，并将映射器注入到容器中，这样才能方便的使用映射功能。所以需要进行如下扩展：

遍历所有模块，通过反射找到所有 IMapperConfig 接口的实现并创建其实例，最后创建 IMapper 并使用单例模式注入到容器中

```csharp
/// <summary>
/// 添加对象映射
/// </summary>
/// <param name="services"></param>
/// <param name="modules">模块集合</param>
/// <returns></returns>
public static IServiceCollection AddMappers(this IServiceCollection services, IModuleCollection modules)
{
    var config = new MapperConfiguration(cfg =>
    {
        foreach (var moduleInfo in modules)
        {
            var types = moduleInfo.AssemblyDescriptor.Application.GetTypes().Where(t => typeof(IMapperConfig).IsAssignableFrom(t));

            foreach (var type in types)
            {
                ((IMapperConfig)Activator.CreateInstance(type)).Bind(cfg);
            }
        }
    });

    services.AddSingleton(config.CreateMapper());

    return services;
}
```

### 4、使用

在需要使用映射的地方，注入`IMapper`即可，比如：

```csharp
public class RoleService : IRoleService
{
    //注入
    private readonly IMapper _mapper;

    public RoleService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<IResultModel> Add(RoleAddModel model)
    {
        if (await _repository.Exists(model.Name))
            return ResultModel.HasExists;

        //映射
        var entity = _mapper.Map<RoleEntity>(model);

        var result = await _repository.AddAsync(entity);

        return ResultModel.Result(result);
    }
}

```
