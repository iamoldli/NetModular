using System;
using System.Collections.Generic;

namespace Nm.Lib.Data.Abstractions.Options
{
    public class DbModuleOptions
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
        /// 表前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 数据库版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 实体类型列表
        /// </summary>
        public List<Type> EntityTypes { get; set; }
    }
}
