## .Net Core 模块化的前后端分离的快速开发框架

### 框架特点

1、模块化  
2、前后端分离  
3、多数据库任意切换(目前支持 SqlServer、MySql、SQLite，源码中只上传了MySql和SqlServer的数据库文件，有时候会更新不及时，最好用MySql来测试~)  
4、异步到底

### Demo

地址：[https://nm.iamoldli.com](https://nm.iamoldli.com)

账户：admin  
密码：admin

### 前端框架 Demo

地址：[http://nm.demo.iamoldli.com/index.html](http://nm.demo.iamoldli.com/index.html)  
账户：admin  
密码：admin

### 文档

> [1、开篇](https://www.cnblogs.com/oldli/p/10895001.html)  
> [2、快速创建一个业务模块](https://www.cnblogs.com/oldli/p/10905415.html)  
> [3、数据访问模块介绍](https://www.cnblogs.com/oldli/p/10910247.html)  
> [4、模块化实现思路](https://www.cnblogs.com/oldli/p/10945137.html)

### 交流QQ群

南京.Net Core交流群 903210423 ，欢迎加群交流学习

### 开发环境

#### 服务端

IDE：vs2017+  
框架：[.Net Core 2.2+](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/?tabs=windows&view=aspnetcore-2.2#tabpanel_Rl26OFmlz8_aspnetcore2x)  
数据库：MSSQL、MySql、SQLite

#### 前端

IDE：vscode  
框架：[Vue.js 全家桶](https://cn.vuejs.org/)、[Element-UI](https://element.eleme.cn/#/zh-CN/component/layout)

### 运行方式

1、找到`data`目录，按需要创建对应数据库(目前只上传了 SqlServer 和 MySql)

> > 需要注意的时，在 linux 的 docker 中 mysql 默认启用了区分大小写，需要先关闭

2、打开`src/WebHost/conifg/db.json`文件，配置数据库连接

数据库切换通过`Dialect`配置：0、SqlServer 1、MySql 2、SQLite

```json
{
  // 是否开启SQL日志
  "Logging": true,
  "Connections": [
    {
      "Name": "Admin",
      "Dialect": 1,
      "Database": "Nm_Admin",
      "ConnString": "Server=localhost;Database=nm_admin;Uid=root;Pwd=root;Allow User Variables=True;charset=utf8;SslMode=none;"
    },
    {
      "Name": "CodeGenerator",
      "Dialect": 1,
      "Database": "Nm_CodeGenerator",
      "ConnString": "Server=localhost;Database=nm_codegenerator;Uid=root;Pwd=root;Allow User Variables=True;charset=utf8;SslMode=none;"
    },
    {
      "Name": "Common",
      "Dialect": 1,
      "Database": "Nm_Common",
      "ConnString": "Server=localhost;Database=nm_common;Uid=root;Pwd=root;Allow User Variables=True;charset=utf8;SslMode=none;"
    },
    {
      "Name": "PersonnelFiles",
      "Dialect": 1,
      "Database": "Nm_PersonnelFiles",
      "ConnString": "Server=localhost;Database=Nm_PersonnelFiles;Uid=root;Pwd=root;Allow User Variables=True;charset=utf8;SslMode=none;"
    }
  ]
}
```

3、启动服务端，进入 `src/WebHost/` 目录，使用控制台运行 `dotnet watch run` 命令

4、启动前端，进入 `src/WebUI/` 目录，运行以下命令

```js
npm i
npm update
npm run serve
```

node: _如果安装包失败，请使用官方的镜像，不要使用国内的_

5、然后打开浏览器，输入`http://localhost:5220/app`进行浏览

可访问 `http://localhost:6220/swagger/index.html` 查看接口文档

### 功能模块

> 权限管理模块(00_Admin)

> > - [x] 模块管理
> > - [x] 权限管理
> > - [x] 菜单管理
> > - [x] 按钮管理
> > - [x] 角色管理
> > - [x] 账户管理
> > - [x] 审计日志
> > - [x] 配置管理
> > - [x] 系统设置

> 代码生成模块(01_CodeGenerator)

> > - [x] 项目管理
> > - [x] 实体配置
> > - [x] 枚举管理

> 通用模块(02_Common)

> > - [x] 行政区划代码管理
> > - [x] 附件管理
> > - [ ] 字典表管理

> 人事档案(03_PersonnelFiles)

> > - [x] 公司单位管理
> > - [x] 部门管理
> > - [x] 岗位管理
> > - [x] 人员信息管理
> > - [x] 人员工作经历管理
> > - [x] 人员教育经历管理

> 任务调度(04_Quartz)

> > - [x] 任务组
> > - [x] 任务管理(添加、暂停、启动、删除)
