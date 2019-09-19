# 模块化原理

## 应用程序部件

使用 ASP.NET Core 实现模块化是非常简单的，这得益于它提供的 [应用程序部件](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-2.2) 功能。

官方文档介绍的很详细了，这里不在介绍，简单做下总结：

1、通过应用程序部件能够实现加载或删除 MVC 相关的功能，如控制器、视图等

2、ASP.NET Core 应用本身也是通过应用程序部件来加载 MVC 相关功能的，当调用`services.AddMvc()`方法添加 MVC 功能时，会创建`ApplicationPartManager`对象并会扫描入口程序集以及依赖项树来加载控制器等资源。[相关代码](https://github.com/aspnet/AspNetCore/blob/master/src/Mvc/Mvc.Core/src/DependencyInjection/MvcCoreServiceCollectionExtensions.cs)

模块化系统最重要的就是如何去加载模块，从上面的总结可以看出，通过应用程序部件可以很方便的实现~

## 模块化场景

