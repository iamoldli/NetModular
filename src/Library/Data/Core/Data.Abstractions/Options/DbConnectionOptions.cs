using System;
using System.Collections.Generic;
using Nm.Lib.Data.Abstractions.Enums;

namespace Nm.Lib.Data.Abstractions.Options
{
    public class DbConnectionOptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public SqlDialect Dialect { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnString { get; set; }

        /// <summary>
        /// 实体类型列表
        /// </summary>
        public List<Type> EntityTypes { get; set; }
    }
}
