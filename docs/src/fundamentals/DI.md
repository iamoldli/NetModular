# 依赖注入

## 依赖注入(DI)与控制反转(IoC)

提到依赖注入，不得不提控制反转，大部分人往往把它们两个当作是一个东西，其实不是的。控制反转全称是`Inversion of Control`，简称`IoC`，他是面向对象编程的一种设计原则，而依赖注入是控制反转的一种 **实现方式** 。

我们可以举个例子来说明。

比如要在系统中记录开发日志，一开始决定采用文本方式记录。

首先创建日志记录器类`Logger`

```csharp
public class TextLogger
{
    public void Debug(string msg)
    {
        //写入文本
    }
}
```

然后再系统中需要记录开发日志的地方去实例化`Logger`并调用记录方法。

```csharp
public class Product
{
    private readonly TextLogger _logger = new TextLogger();
    public void Create()
    {
        //创建商品

        _logger.Debug("创建产品");
    }
}
```

这样就可以将开发日志记录到文件来调试了，但是这样用起来不是很方便，每次都要去服务器上查看日志文件，所以为了方便调试，不如把日志保存到数据库里面。于是，便有了下面的类

```csharp
//文本日志记录器
public class TextLogger
{
    public void Debug(string msg)
    {
        //写入文本
    }
}

//数据库日志记录器
public class MySqlLogger
{
    public void Debug(string msg)
    {
        //写入数据库
    }
}
```

然后你需要把所有用到`TextLogger`的地方，全部修改为`MySqlLogger`，如果你的系统很大，很多地方用到了`TextLogger`，修改起来岂不是很痛苦~

为了避免这种情况，一般再设计的时候会把日志记录器设计为一个接口，在使用时通过构造函数来传递实现

```csharp
//日志记录器接口
public interface ILogger
{
    void Debug(string msg);
}

//文本日志记录器
public class TextLogger:ILogger
{
    public void Debug(string msg)
    {
        //写入文本
    }
}

//数据库日志记录器
public class MySqlLogger:ILogger
{
    public void Debug(string msg)
    {
        //写入数据库
    }
}
```

使用

```csharp
public class Product
{
    private readonly ILogger _logger;
    public Product(ILogger logger)
    {
        _logger=logger;
    }

    public void Create()
    {
        //创建商品

        _logger.Debug("创建产品");
    }
}

var logger = new MySqlLogger();
var p = new Product(logger);
```

这时候你会发现，Product 依赖于日志记录器的接口，而具体的实现则交给了调用方来决定，这样是不是就实现了控制反转的功能~

虽然实现了 Product 的控制反转，但是 Product 的调用方，还是要去创建一个具体的日志记录器实现，如果有很多个地方需要 Product，不还是一样吗？这里就需要依赖注入了~

依赖注入的核心是一个容器，这个容器会帮你管理类的实例，自动解决类之间的依赖管理，你需要哪个类的实例，就取哪个就行了。比如你需要 Product 的一个实例，只要再在你使用的地方注入即可，不需要关系 Product 依赖了哪些类，容器也会会自动帮你创建依赖的实例。

常用的依赖注入方式有两种，1、构造函数注入 2、属性注入，属性注入用的很少，这里就不做介绍了。

## .NET Core 的依赖注入

.NET Core 本身提供了一个简单高效的依赖注入功能，如果觉得无法满足自己的需求，可以自己扩展或者使用第三方的依赖注入框架，比如[Autofac](https://github.com/autofac/Autofac)。不过我个人觉得官方的已经够用了~

官方文档：[在 ASP.NET Core 依赖注入](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#framework-provided-services)

## 框架的依赖注入

其实官方文档讲的已经很详细了，这里就没必要再去重复讲了，我只介绍下本框架用到的依赖注入有关的点~

> 1、官方注入的时候，仅支持一个一个注入，没办法注入整个程序集的(Autofac 可以~)，所以为了方便，做了一个简单的封装，框架会自动将包含`SingletonAttribute`特性的类使用单例模式注入到容器中。但是该功能仅扫描框架定义好的结构，比如 Domian、Application、Infrastructure、Web 这些程序集，如果你是自己定义了一个程序集，则需要用到下面的方式

> 2、`IServiceCollection`添加了一个扩展方法`AddSingletonFromAssembly`，它接收一个`Assembly`类型的参数，该方法会自动扫描所有包含`SingletonAttribute`特性的类，使用单例模式注入到容器中。

> 3、原则上，能够使用单例注入的，不再使用静态类的方式。

> 4、框架的所有功能基本都采用了依赖注入，比如数据访问(Data)、对象映射(Mapper)、缓存(Cache)等待，在这些库里面，都会有一个`ServiceCollectionExtensions.cs`扩展类，里面就是当前库使用的注入信息。
