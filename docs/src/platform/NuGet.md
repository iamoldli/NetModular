# Nuget 服务器搭建

随着公司业务发展，模块会越来越多，模块之间的依赖如果还是采用引用的方式管理，那真的是一件很痛苦的事情，而 [NuGet](https://www.nuget.org/) 就是为了解决这个问题而生的。NuGet 不仅仅包含 dll 文件，它提供了很多方便的功能，比如安装某个包后执行一个脚本，或者是复制一些文件等等，这里不再介绍，感兴趣的 Coder 可以自行查看官方文档[NuGet 文档](https://docs.microsoft.com/zh-cn/nuget/what-is-nuget)

微软官方提供了的包管理平台[NuGet](https://www.nuget.org/)，它允许任何人发布任何包，但是这些包都是公开的，对于企业业务模块来说，应该是私有的，不允许对外开放的。所以为了更好的管理业务模块的包，企业需要搭建私有的 NuGet 服务器。

## BaGet Or NuGet.Server

为什么用 [BaGet](https://loic-sharma.github.io/BaGet/) 而不用 [NuGet.Server](https://github.com/NuGet/NuGet.Server)？

[NuGet.Server](https://github.com/NuGet/NuGet.Server)是微软推出的轻量级的 NuGet 服务器，它是开源的，但是它是基于.Net Framework 的，只能运行在 Windows 服务器上，而且它不支持 NuGet 的 v3 版本的 API，不包含管理页面。

[BaGet](https://loic-sharma.github.io/BaGet/)是国外大神开发的一款基于最新的.Net Core 的 NuGet 服务器，所以它是跨平台的，同时它也提供了一套友好的管理页面。

## 获取源码

从 GitHub 获取最新的代码[https://github.com/loic-sharma/BaGet](https://github.com/loic-sharma/BaGet)

## 配置

使用 VS 打开项目，编辑`appsettings.json`配置文件

```json
{
  //发布包时用到密钥
  "ApiKey": "15722E23-4D72-43D9-B8FA-264F7745778C",
  //包删除级别，默认Unlist，只允许卸载，不能硬删除，如果要硬删除修改为HardDelete
  "PackageDeletionBehavior": "Unlist",
  //启用包覆盖，启用后同一个包发布时相同的版本号会覆盖旧的文件，否则不允许发布
  "AllowPackageOverwrites": true,
  //数据库类型
  "Database": {
    "Type": "Sqlite",
    "ConnectionString": "Data Source=baget.db"
  },
  //存储方式，默认即可
  "Storage": {
    "Type": "FileSystem",
    "Path": ""
  },
  //查询方式
  "Search": {
    "Type": "Database"
  },
  //镜像配置，如果为true，则会自动从指定的PackageSource下载第三方包
  "Mirror": {
    "Enabled": false,
    "PackageSource": "https://api.nuget.org/v3/index.json"
  },
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  }
}
```

## 发布部署

BaGet 本身就是一个 ASP.NET Core 项目，所以发布部署参考官方文档即可，传送门：

[使用 IIS 在 Windows 上托管 ASP.NET Core](https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/iis/?view=aspnetcore-2.2)

[使用 Nginx 在 Linux 上托管 ASP.NET Core](https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-2.2)

## 创建包

创建 NuGet 包有好几种方式，这里不再一一介绍，需要了解的请移步[创建包](https://docs.microsoft.com/zh-cn/nuget/create-packages/overview-and-workflow)。
这里只介绍一种，也是个人认为最方便的一种，那就是[MSBuild](../extend/MSBuild)。

通过 MSBuild 可以在编译项目时同步生成对应的 NuGet 包，包的信息通过在项目文件(.csproj)中进行配置，以下时一些配置说明，更多内容请移步[使用 MSBuild 创建 NuGet 包](https://docs.microsoft.com/zh-cn/nuget/create-packages/creating-a-package-msbuild)

```xml
  <PropertyGroup>
    <!--作者-->
    <Authors>Oldli</Authors>
    <!--包版本-->
    <Version>1.2.5</Version>
    <!--包描述-->
    <Description>模块核心库</Description>
    <!--是否生成xml文档（该文档会包含在nuget包中）-->
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <!--编译项目时生成包-->
    <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
    <!--所属公司-->
    <Company></Company>
    <!--Copyright-->
    <Copyright></Copyright>
    <!--项目地址-->
    <PackageProjectUrl>https://github.com/iamoldli/NetModular</PackageProjectUrl>
    <!--仓储地址-->
    <RepositoryUrl>https://github.com/iamoldli/NetModular</RepositoryUrl>
  </PropertyGroup>
```

## 发布包

发布包有两种方式：

1、通过[NuGetPackageExplorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer)图形工具来发布，可在 windows10 应用商店里面直接搜索安装，该工具也可以创建编辑 NuGet 包

打开一个包

<nm-img id="20190821135546"/>

点击 File>Publish

<nm-img id="20190821140203"/>

最后发布即可~

2、通过 dotnet nuget CLI 发布

dotnet nuget push `包文件路径` -k `密钥` -s `包服务器地址`

```
dotnet nuget push Nm.Module.Admin.Domain.1.2.1.nupkg -k qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 -s http://localhost:5000/v3/index.json
```

如果有很多包，一个个发布是很通过的，所以你可以写一个 ps 脚本来批量发布

```
Get-ChildItem -Path ./NetModular/_packages | ForEach-Object -Process{
    dotnet nuget push $_.fullname  -k qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 -s http://localhost:5000/v3/index.json
}
```
