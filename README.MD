<h1 align="center">
  <img src="./img/logo.png" alt="NetModular" width="175"/>
<br>
NetModular
</h1>

<p align="center">
  <a href="https://github.com/iamoldli/NetModular/blob/master/LICENSE"><img src="https://img.shields.io/github/license/iamoldli/NetModular"></a>
</p>

## NetModular 是什么

NetModular 是一款为中小型企业而生的基于.Net Core 平台的业务模块化快速开发解决方案，最终目的是能够让中小团队快速搭建公司内部开发平台。

## 演示地址

本仓库只包含后端框架代码，为了方便大家调试运行，同时包含了权限管理模块的代码，但是演示地址是把现有的几个模块整合后发布的，所以当你运行本仓库发现与线上不一样的时候，请下载演示源码，并且需要自己配置菜单~

演示地址：[https://demo.17mkh.com/](https://demo.17mkh.com/)  
账 户：admin  
密 码: admin

演示源码：[https://github.com/iamoldli/NetModular.Demo](https://github.com/iamoldli/NetModular.Demo)

## 演示截图

<h1 align="center">
  <img src="./img/20201108235810.png" alt="NetModular" style="margin-right:2%;width:48%;"/>
  <img src="./img/20201108235849.png" alt="NetModular" style="width:48%;"/>
  <img src="./img/20201108235914.png" alt="NetModular" style="margin-right:2%;width:48%;"/>
  <img src="./img/20201108235933.png" alt="NetModular" style="width:48%;"/>
<br>

## 后端框架结构说明

| 框架模块      | 说明                                                                                                      | 版本                                                                                                                                                                                                                                                       |
| ------------- | --------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 00_Host       | 主机库                                                                                                    | <a href="https://www.nuget.org/packages/NetModular.Lib.Host.Web/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Host.Web"/></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Host.Web"/>                                             |
| 01_Utils      | 通用基础库                                                                                                | <a href="https://www.nuget.org/packages/NetModular.Lib.Utils.Core/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Utils.Core"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Utils.Core"/>                                        |
| 02_Data       | 轻量级 ORM 库(基于 <a href="https://github.com/StackExchange/Dapper">Dapper</a> 扩展)                     | <a href="https://www.nuget.org/packages/NetModular.Lib.Data.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Data.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Data.Abstractions"/>                   |
| 03_Logging    | 日志库(基于 <a href="https://github.com/serilog/serilog">Serilog</a> 扩展)                                | <a href="https://www.nuget.org/packages/NetModular.Lib.Logging.Serilog/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Logging.Serilog"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Logging.Serilog"/>                         |
| 04_Mapper     | 对象映射库(基于 <a href="https://github.com/AutoMapper/AutoMapper">AutoMapper</a> 扩展)                   | <a href="https://www.nuget.org/packages/NetModular.Lib.Mapper.AutoMapper/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Mapper.AutoMapper"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Mapper.AutoMapper"/>                   |
| 05_Swagger    | 接口文档库(基于 <a href="https://github.com/domaindrivendev/Swashbuckle.AspNetCore">Swashbuckle</a> 扩展) | <a href="https://www.nuget.org/packages/NetModular.Lib.Swagger.Core"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Swagger.Core"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Swagger.Core"/>                                   |
| 06_Cache      | 缓存库，支持内存缓存和 Redis                                                                              | <a href="https://www.nuget.org/packages/NetModular.Lib.Cache.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Cache.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Cache.Abstractions"/>                |
| 07_Validation | 模型验证库(基于 <a href="https://github.com/FluentValidation/FluentValidation">FluentValidation</a> 扩展) | <a href="https://www.nuget.org/packages/NetModular.Lib.Validation.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Validation.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Validation.Abstractions"/> |
| 08_Auth       | 身份认证库，已实现 JWT 认证方式                                                                           | <a href="https://www.nuget.org/packages/NetModular.Lib.Auth.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Auth.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Auth.Abstractions"/>                   |
| 09_Module     | 业务模块化核心库                                                                                          | <a href="https://www.nuget.org/packages/NetModular.Lib.Module.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Module.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Module.Abstractions"/>             |
| 10_MQ         | 消息通讯库，实现对 RabbitMQ 的封装                                                                        | <a href="https://www.nuget.org/packages/NetModular.Lib.MQ.RabbitMQ/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.MQ.RabbitMQ"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.MQ.RabbitMQ"/>                                     |
| 11_Quartz     | 基于 <a href="https://github.com/quartznet/quartznet">Quartz.Net</a> 封装的任务调度库                     | <a href="https://www.nuget.org/packages/NetModular.Lib.Quartz.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Quartz.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Quartz.Abstractions"/>             |
| 12_Excel      | Excel 扩展库                                                                                              | <a href="https://www.nuget.org/packages/NetModular.Lib.Excel.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Excel.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Excel.Abstractions"/>                |
| 13_Config     | 通用配置库                                                                                                | <a href="https://www.nuget.org/packages/NetModular.Lib.Config.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Config.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Config.Abstractions"/>             |
| 14_Pdf        | PDF 扩展库                                                                                                | <a href="https://www.nuget.org/packages/NetModular.Lib.Pdf.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.Pdf.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.Pdf.Abstractions"/>                      |
| 15_MQTT       | MQTT 扩展库(未实现)                                                                                       |                                                                                                                                                                                                                                                            |
| 16_OSS        | 对象存储库，内含七牛云 SDK 的 Core 版本                                                                   | <a href="https://www.nuget.org/packages/NetModular.Lib.OSS.Abstractions/"><img src="https://img.shields.io/nuget/v/NetModular.Lib.OSS.Abstractions"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Lib.OSS.Abstractions"/>                      |

## 开发环境

> 开发工具
>
> > [Visual Studio 2019+](https://visualstudio.microsoft.com/zh-hans/downloads/)、[Visual Studio Code](https://code.visualstudio.com/)

> 后端
>
> > [.Net Core 3.1](https://dotnet.microsoft.com/download)、[Dapper](https://github.com/StackExchange/Dapper)、[Serilog](https://serilog.net/)、[AutoMapper](https://automapper.org/)、[FluentValidation](https://fluentvalidation.net)、[Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)、RabbitMQ、Redis、EPPlus

> 前端
>
> > [Node.js 10+](https://nodejs.org/en/)、[Vue.js 2.6+](https://cn.vuejs.org/)、[Vue CLI](https://cli.vuejs.org/zh/guide/)、[Vuex](https://vuex.vuejs.org/zh/)、[VueRouter](https://router.vuejs.org/zh/)、[Element-UI](https://element.eleme.cn/#/zh-CN/component/installation)

## 前端框架 NetModular.UI

<img src="https://img.shields.io/npm/v/netmodular-ui"/> <img src="https://img.shields.io/npm/dt/netmodular-ui"/>

前端采用 Vue.js 全家桶+Element UI 开发，也是基于模块化思想设计，源码不在本仓库，请移步 [NetModular.UI](https://github.com/iamoldli/NetModular.UI)

## 前端皮肤

前端支持自定义皮肤功能，框架本身提供了两套皮肤，其中一套已经内置作为默认皮肤，另外一套需要单独安装，源码如下：

| 编码     | 版本                                                                                                                                     | 仓库地址                                                                           |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------- |
| Classics | <img src="https://img.shields.io/npm/v/netmodular-skins-classics"/> <img src="https://img.shields.io/npm/dt/netmodular-skins-classics"/> | [NetModular.Skins.Classics](https://github.com/iamoldli/NetModular.Skins.Classics) |

## 官方文档

文档地址： [https://docs.17mkh.com](https://docs.17mkh.com)

文档项目的源码请移步： [https://github.com/iamoldli/NetModular.Docs](https://github.com/iamoldli/NetModular.Docs)

## 视频教程

[NetModular 的正确打开方式~](https://www.bilibili.com/video/BV14v41147TT/)

## 模块列表

本仓库只包含基础框架以及权限管理(Admin)模块代码，其它模块请点击以下链接查看~

| 编号 | 编码           | 说明                     | 版本                                                                                                                                                                                                                                                 | 仓库地址                                                                                         |
| ---- | -------------- | ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------ |
| 00   | Admin          | 权限管理                 | <a href="https://www.nuget.org/packages/NetModular.Module.Admin.Web/"><img src="https://img.shields.io/nuget/v/NetModular.Module.Admin.Web"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Module.Admin.Web"/>                            | 本仓库                                                                                           |
| 01   | Common         | 通用模块                 | <a href="https://www.nuget.org/packages/NetModular.Module.Common.Web/"><img src="https://img.shields.io/nuget/v/NetModular.Module.Common.Web"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Module.Common.Web"/>                         | [NetModular.Module.Common](https://github.com/iamoldli/NetModular.Module.Common)                 |
| 02   | CodeGenerator  | 模块创建(又名代码生成器) | <a href="https://www.nuget.org/packages/NetModular.Module.CodeGenerator.Web/"><img src="https://img.shields.io/nuget/v/NetModular.Module.CodeGenerator.Web"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Module.CodeGenerator.Web"/>    | [NetModular.Module.CodeGenerator](https://github.com/iamoldli/NetModular.Module.CodeGenerator)   |
| 03   | Quartz         | 任务调度                 | <a href="https://www.nuget.org/packages/NetModular.Module.Quartz.Web/"><img src="https://img.shields.io/nuget/v/NetModular.Module.Quartz.Web"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Module.Quartz.Web"/>                         | [NetModular.Module.Quartz](https://github.com/iamoldli/NetModular.Module.Quartz)                 |
| 04   | PersonnelFiles | 人事档案                 | <a href="https://www.nuget.org/packages/NetModular.Module.PersonnelFiles.Web/"><img src="https://img.shields.io/nuget/v/NetModular.Module.PersonnelFiles.Web"></a> <img src="https://img.shields.io/nuget/dt/NetModular.Module.PersonnelFiles.Web"/> | [NetModular.Module.PersonnelFiles](https://github.com/iamoldli/NetModular.Module.PersonnelFiles) |

## 技术交流

QQ 群：点击加群 [903210423](http://shang.qq.com/wpa/qunwpa?idkey=cfc871fccc7173f17ac2c9d12c8a31a7549c260e6aefcb6a40fdcc4b423940b0)

<img src="./img/qq.jpg" alt="QQ" height="300"/>

## 贡献者

<a href="https://github.com/JTOne123"><img src="https://avatars2.githubusercontent.com/u/3457140?s=60&amp;v=4" style="border-radius:19px" width="38" height="38"></a>
<a href="https://github.com/hajiuxbz"><img src="https://avatars2.githubusercontent.com/u/22442565?s=60&v=4" style="border-radius:19px" width="38" height="38"></a>
<a href="https://github.com/chylove1982"><img src="https://avatars0.githubusercontent.com/u/18054267?s=60&v=4" style="border-radius:19px" width="38" height="38"></a>
<a href="https://github.com/autukill"><img src="https://avatars1.githubusercontent.com/u/5186639?s=60&v=4" style="border-radius:19px" width="38" height="38"></a>
<a href="https://github.com/alasai"><img src="https://avatars1.githubusercontent.com/u/26901102?s=60&v=4" style="border-radius:19px" width="38" height="38"></a>

## 贡献准则

欢迎任何人贡献代码或提供好的功能建议~
