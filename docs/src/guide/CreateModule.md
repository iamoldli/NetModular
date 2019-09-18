# 创建模块

## 说明

想要实现快速创建模块，离不开代码生成器~

大部分代码生成器都是根据数据库的表结构信息来生成项目代码，而本框架的代码生成器有些不一样，它是先设计实体然后生成数据库以及项目代码，这样设计的目的主要是因为当需要开发一个新的模块的时候，团队要聚在一起讨论实体模型，大部分都是采用 Excel 文件或者 Word 文件来记录，讨论完成后发送给各个人员。个人觉得这样不是很友好，每次修改一个实体，都要重新发送最新文件给其他人员。

所以，为了可以方便的查看项目信息和实体信息，便开发了 Web 版的代码生成器，把代码生成器项目部署到公司的内网环境，这样就能随时在线查看修改了~

当然，上面的方式仅适合新项目，如果是已有数据库了，还是要根据数据库来生成项目文件的，不过暂时还没做这个功能，目前代码生成器只能算是 1.0 版本，距离我最终想要的版本还有很大距离，比如可视化拖拽配置前端页面等等，任重而道远~

## 1、添加项目

打开代码生成器中的项目列表，点击添加按钮，填写模块名称、编码，前缀可以填写 Nm

<nm-img id="20190918152255"/>

::: warning

#### Note

如果您使用的是自己发布后的包，并修改了包命名空间的前缀，请填写您自己的前缀名称

:::

## 2、添加实体

点击项目列表的实体按钮

<nm-img id="20190918152725"/>

点击添加实体按钮

<nm-img id="20190918152831"/>

填写 类名、表名、基类类型和备注

实体基类类型包括

> IEntity 不包含任何属性

> Entity 继承自 IEntity，包含 Id 属性

> EntityBase 继承自 Entity，包含 Id、CreatedBy(创建人)、CreatedTime(创建时间)、ModifiedBy(修改人)、ModifiedTime(修改时间)属性，以及 Creator(创建人名称)、Modifier(修改人名称)两个扩展属性

> EntityWithSoftDelete 继承自 IEntity，包含 Id、Deleted(已删除)、DeletedTime(删除时间)、DeletedBy(删除人编号)属性，以及 Deleter(删除人)扩展属性

> EntityBaseWithSoftDelete 继承自 EntityWithSoftDelete，除了包含软删除的属性外，还包含了 EntityBase 的属性

::: warning

#### Note

以上实体基类类型，除了 IEntity 以外，全部支持四种主键类型：Int、Long、Guid、String

:::

基础方法可以选择生成代码时，需要生成哪些方法，保存~

## 3、添加属性

点击刚刚添加的 Article 实体名称，配置实体的属性

<nm-img id="20190918154356"/>

创建实体的时候，会默认创建实体基类的属性信息，会有一个星号标注，这些属性是无法修改编辑的~

点击添加按钮添加属性，填写对应信息即可~

<nm-img id="20190918154625" />

::: warning

#### Note

如果属性时枚举类型，需要先添加枚举，然后再添加属性的时候类型选择枚举并绑定

:::

## 4、配置实体模型

返回实体列表，选择刚刚添加的 Article 实体，点击模型按钮

<nm-img id="20190918155141" />

目前包括三种模型，查询、添加、修改，模型的属性可以手动添加，也可以从实体中导入

<nm-img id="20190918155254" />

## 5、生成

当把需要的实体和属性配置完成后，返回项目列表，点击生成代码，会自动生成并下载代码文件

加压后目录结构如下：

<nm-img id="20190918155614" />

里面已经包含了数据库文件以及前后端代码

## 6、创建数据库

创建 nm_blog 数据库，并执行 data/MySql/Schema.sql 文件创建数据库表

修改数据库连接信息，修改方法可以参考 [快速启动](./GetingStart.html#配置数据库)

```json
{
  "Logging": false,
  "Connections": [
    {
      "Name": "Admin",
      "Dialect": 0,
      "Database": "{数据库名称}",
      "ConnString": "Server={数据库地址};Database=Nm_Admin;Uid=sa;Pwd=sa;MultipleActiveResultSets=true;"
    },
    {
      "Name": "Blog",
      "Dialect": 0,
      "Database": "{数据库名称}",
      "ConnString": "Server={数据库地址};Database=Nm_Blog;Uid=sa;Pwd=sa;MultipleActiveResultSets=true;"
    }
  ]
}
```

::: warning

#### Note

因为所有模块都依赖于 Admin 模块，所以也需要配置 Admin 模块的数据库连接信息

:::

## 7、升级包

代码生成器没有办法保证生成的代码引用的 Nuget 都是最新版本的，所以需要手动取更新下包版本

## 8、运行

剩下的与 [快速启动](./GetingStart.html)里面是一样的~
