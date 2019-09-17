# 通用库(Utils)

## 说明

通用库是整个框架的基础库，它包含了常用的封装类和扩展类，目前包括两部分`Utils.Core`和`Utils.Mvc`，从名字就可以看出来，一个是核心库，一个是针对 Mvc 的库。

因为里面东西较多，而且比较杂，所以仅仅挑选一些用的最多的来介绍，其它的可自行查看源码。

## 数据转换扩展

C#中，当需要数据转换的时候，可以有多种方式，比如：

```csharp
//第一种
var str = "123";
var num = Convert.ToInt32(str);

//第二种，如果str包含非数字类型，不会异常
var str = "123";
if (int.TryParse(str, out int num))
{

}
```

以上两种方式都可以转换，区别在于第一种如果 str 包含了非数字类型，会抛出异常，，所以一般为了确保转换成功，会采用第二种方式。但是，如果每次数据转换都这样写也挺麻烦的，好在 C#有扩展方法的存在。

```csharp
/// <summary>
/// 转换成Int/Int32
/// </summary>
/// <param name="s"></param>
/// <param name="round">是否四舍五入，默认false</param>
/// <returns></returns>
public static int ToInt(this object s, bool round = false)
{
    if (s == null || s == DBNull.Value)
        return 0;

    if (s.GetType().IsEnum)
    {
        return (int)s;
    }

    if (s is bool b)
        return b ? 1 : 0;

    if (int.TryParse(s.ToString(), out int result))
        return result;

    var f = s.ToFloat();
    return round ? Convert.ToInt32(f) : (int)f;
}
```

这样，当你需要将字符串转换为整数时，只需这样：

```csharp
var str = "123";
var num = str.ToInt();
```

是不是很简单~

其它数据类型转换方法都是类似的，比如字符串转日期，则是`str.ToDatetime()`。

完整的数据转换扩展方法可以查看该文件[CommonExtensions.cs](https://github.com/iamoldli/NetModular/blob/master/src/Library/Utils/Utils.Core/Extensions/CommonExtensions.cs)，这里就不再详细列出了。

## Guid 扩展

```csharp
var id = Guid.NewGuid();

//判断Guid是否为空
var isEmpty = id.IsEmpty();

//判断Guid是否不为空
var notEmpty = id.NotEmpty();

```

## 枚举扩展

经常遇到需要显示枚举值对应的中文名称，可以使用以下扩展方法：

```csharp
public enum Sex{
    [Description("男")]
    Boy,
    [Description("女")]
    Girl
}

var name = Sex.Boy.ToDescription();
//name:男
```

`ToDescription`方法用的最多的地方就是在实体的扩展类里面，实体的属性是枚举类型的，当查询某个实体列表的时候，往往希望返回的是枚举值对应的中文名称，比如`User`实体，里面有`Sex`属性，一般会在`User.Extend.cs`类里面添加一个`SexName`的属性

```csharp
public string SexName => Sex.ToDescription();
```

这样，迁回就能通过`sexName`来获取性别名称了~

## 字符串扩展

```csharp
var str = "123";

//判断字符串是否为Null、空
var b = str.IsNull();

//判断字符串是否不为Null、空
var b = str.NotNull();

// 与字符串进行比较，忽略大小写
var b = str.EqualsIgnoreCase("123");
```

## 加解密

> DES 加解密：[DesEncrypt.cs](https://github.com/iamoldli/NetModular/blob/master/src/Library/Utils/Utils.Core/Encrypt/DesEncrypt.cs)

> MD5 加密：[Md5Encrypt.cs](https://github.com/iamoldli/NetModular/blob/master/src/Library/Utils/Utils.Core/Encrypt/MD5Encrypt.cs)

## 返回结果

方法或接口经常需要返回包含多个状态的接口，所以通用库提供了[IResultModel.cs](https://github.com/iamoldli/NetModular/blob/master/src/Library/Utils/Utils.Core/Result/IResultModel.cs)接口和[ResultModel.cs](https://github.com/iamoldli/NetModular/blob/master/src/Library/Utils/Utils.Core/Result/ResultModel.cs)实现类。

在 ResultModel 中，Code 为 1 表示成功，为 0 表示失败，系统中已提供了一些非常方便的方法

```csharp
//成功
ResultModel.Success("这里可以传任何类型的数据");

//失败
ResultModel.Failed();

//根据bool返回success/failed
ResultModel.Result(bool success);

//数据已存在
ResultModel.HasExists;

//数据不存在
ResultModel.NotExists;
```

示例：

```csharp
public IResultModel Create(int id)
{
    if(id>0)
        return ResultModel.Success("创建成功");
    else
        return ResultModel.Failed("创建失败");
}

//如果上面的方法是一个action，那么前端接收的数据应该是
{
    code:1,
    msg:'创建成功'
}
```

## 数据检测

数据检测用于需要判断数据是否符合条件，不符合会抛出异常的情况。

所有检测方法都在[Check.cs](https://github.com/iamoldli/NetModular/blob/master/src/Library/Utils/Utils.Core/Check.cs)里面

```csharp
publick void Create(string name)
{
    //如果名称为空，会抛出异常
    Check.NotNull(name,nameof(name), "名称不能为空~");
}
```
