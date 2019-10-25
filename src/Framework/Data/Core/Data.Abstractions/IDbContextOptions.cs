using System.Data;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Options;

namespace NetModular.Lib.Data.Abstractions
{
    /// <summary>
    /// 数据库配置项
    /// </summary>
    public interface IDbContextOptions
    {
        /// <summary>
        /// 数据库适配器
        /// </summary>
        ISqlAdapter SqlAdapter { get; }

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

        /// <summary>
        /// 数据库模块信息
        /// </summary>
        DbModuleOptions DbModuleOptions { get; }

        /// <summary>
        /// 创建数据库事件
        /// </summary>
        IDatabaseCreateEvents DatabaseCreateEvents { get; set; }
    }
}
