## .Net Core 模块化的前后端分离的快速开发框架

### 使用方法

#### 1、还原数据库(data\Admin\Mn_Admin_SqlServer.sql)

#### 2、启动服务端

进入`src\WebHost\config`，修改数据库配置文件`db.Development.json`

进入`src\WebHost`，启动命令行，使用 dotnet watch run 命令运行

#### 3、启动前端

进入`src\Module\Admin\UI\mn-module-admin`，执行一下命令

```js
npm i
npm run serve
```

#### 4、访问 [http://localhost:5220/](http://localhost:5220/)

### 5、输入账号密码登录

账户：admin  
密码：admin
