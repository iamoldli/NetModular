using System.Data;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 数据库配置项
    /// </summary>
    public interface IDbContextOptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 数据库适配器
        /// </summary>
        ISqlAdapter SqlAdapter { get; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection NewConnection();

        /// <summary>
        /// 日志工厂
        /// </summary>
        ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 登录信息
        /// </summary>
        ILoginInfo LoginInfo { get; set; }

        /// <summary>
        /// 所有数据库配置信息
        /// </summary>
        DbOptions DbOptions { get; }
    }
}
