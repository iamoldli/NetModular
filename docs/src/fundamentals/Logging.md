# 日志

## 官方文档

[.NET Core 和 ASP.NET Core 中的日志记录](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2)

## Serilog

本框架采用的是[Serilog](https://github.com/serilog/serilog-aspnetcore)日志库，已经集成并替换了官方的日志功能，你可以完全按照官方 API 用法来使用日志记录功能。

默认仅配置了输出到控制台和文件两种方式

```json
{
  "Serilog": {
    //日志输出最小等级
    "MinimumLevel": {
      "Default": "Error",
      "Override": {
        "Microsoft": "Error",
        "System": "Error"
      }
    },
    "WriteTo": [
      //输入到控制台
      {
        "Name": "Console",
        "Args": {
          "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}"
        }
      },
      //输出到文件
      {
        "Name": "File",
        "Args": {
          //文件路径
          "path": "log/log.log",
          //文件滚动方式
          "rollingInterval": "Day",
          //消息输出格式
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
          //文件数量
          "retainedFileCountLimit": 60,
          //使用缓冲，提高写入效率
          "buffered": false
        }
      }
    ]
  }
}
```

Serilog 提供了很多种日志输出方式，如果需要使用其它的方式，可以自行安装对应的包并进行配置，[Sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks)。

如果想要更改日志等级，只需要修改上面`MinimumLevel`属性，比如要记录 Debug 日志，只需把 Error 修改为 Debug 即可
