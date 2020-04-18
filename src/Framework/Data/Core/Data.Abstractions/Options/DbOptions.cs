using System.Collections.Generic;
using NetModular.Lib.Data.Abstractions.Enums;

namespace NetModular.Lib.Data.Abstractions.Options
{
    /// <summary>
    /// 数据库配置项
    /// </summary>
    public class DbOptions
    {
        /// <summary>
        /// 是否开启日志
        /// </summary>
        public bool Logging { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public SqlDialect Dialect { get; set; }

        /// <summary>
        /// 数据库版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否创建数据库
        /// </summary>
        public bool CreateDatabase { get; set; }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public bool InitData { get; set; }

        /// <summary>
        /// 是否加密用户Id和密码
        /// </summary>
        public bool Encrypt { get; set; }

        /// <summary>
        /// PostgreSQL的数据库名称
        /// </summary>
        public string NpgsqlDatabaseName { get; set; }

        /// <summary>
        /// 模块列表
        /// </summary>
        public List<DbModuleOptions> Modules { get; set; }

        /// <summary>
        /// 池中允许的最小连接数。
        /// </summary>
        public int MinPoolSize { get; set; }

        /// <summary>
        /// 池中允许的最大连接数。
        /// </summary>
        public int MaxPoolSize { get; set; }

        /// <summary>
        /// 启用多租户
        /// </summary>
        public bool EnableTenant { get; set; }
    }
}
