# 快速上手

本篇文档让你能够快速的启动`NetModular`项目。

## 创建数据库

目前`NetModular`仅支持代码生成器生成的时候生成实体对应的创建表语句，不支持类似`CodeFirst`的功能，所以在运行项目之前需要先手动创建数据库。数据库文件放在了 [data](https://github.com/iamoldli/NetModular/tree/master/data) 目录中，您可以根据自己的需要来创建指定的数据库。

## 配置

进入`src/WebHost/config`目录，如下：

<nm-img id="20190821142628"/>

::: tip

### 文件说明

> db：存放与数据库有关的配置信息

> host：存放站点启动有关的配置信息

> cache：与缓存有关的配置信息

> jwt：与 JWT 认证有关的配置信息

> logging：与日志有关的配置信息

> module：与模块有关的配置信息

:::

::: warning

### Note

如上图所示，配置信息按照功能放在的不同的文件中，而每一种配置文件都有两种类型，其中带`.Development.json`表示开发环境配置文件，顾名思义，当你是开发环境时，配置信息以带`.Development.json`的为先，否则以不带`.Development.json`的为先。

:::

## 配置数据库

根据自己的数据库配置信息，编辑`db.json`文件，配置信息说明：

已 SQLite 为例，也是代码中的默认配置

```json
{
  //是否开启日志
  "Logging": false,
  //数据库类型 0、SqlServer 1、MySql 2、SQLite
  "Dialect": 2,
  //数据库版本
  "Version": "",
  //数据库地址
  "Server": "..\\..\\data\\SQLite",
  //端口号
  "Port": 0,
  //用户名
  "UserId": "",
  //密码
  "Password": "",
  //是否创建数据库和表
  "CreateDatabase": false,
  //模块列表
  "Modules": [
    {
      //模块名称
      "Name": "Admin",
      //数据库名称
      "Database": "Nm_Admin"
    },
    {
      "Name": "CodeGenerator",
      "Database": "Nm_CodeGenerator"
    },
    {
      "Name": "Common",
      "Database": "Nm_Common"
    },
    {
      "Name": "PersonnelFiles",
      "Database": "Nm_PersonnelFiles"
    },
    {
      "Name": "Quartz",
      "Database": "Nm_Quartz"
    }
  ]
}
```

::: warning

### Note

切换不同的数据库，请修改对应的连接信息！

:::

## 其它配置信息

其它功能模块的配置信息使用默认值就行，如果想修改可以自己查看文件中的说明。

## 启动服务端

这里就使用命令来启动，进入`src/WebHost`目录，打开 CMD 或 PowerShell 执行以下命令

```
dotnet watch run
```

如果结果如下图所示则表示启动成功

<nm-img id="20190821144717"/>

#### 可访问[http://localhost:6220/swagger/index.html](http://localhost:6220/swagger/index.html)浏览接口文档。

<nm-img id="20190821145531"/>

## 启动前端

::: warning
前端运行环境依赖`Node.js 10+`，如果无法启动请检查自己的版本是否匹配`node -v`。  
前端运行环境依赖`Node.js 10+`，如果无法启动请检查自己的版本是否匹配`node -v`。  
前端运行环境依赖`Node.js 10+`，如果无法启动请检查自己的版本是否匹配`node -v`。
:::

进入`src/WebUI`目录，执行以下命令：

```
npm install
npm update
npm run serve
```

如下图所示表示成功

<nm-img id="20190821145614"/>

访问[http://localhost:5220/app](http://localhost:5220/app)浏览，默认账户密码为 admin/admin

## 发布

进入`src/WebUI`目录，执行以下命令打包前端代码：

```
npm run build
```

成功后打包的文件会保存到`src/WebHost/wwwroot/app`目录下面

然后用 vs 打开项目，发布 WebHost 即可
