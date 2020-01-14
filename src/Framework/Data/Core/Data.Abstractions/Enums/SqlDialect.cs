using System.ComponentModel;

namespace NetModular.Lib.Data.Abstractions.Enums
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum SqlDialect
    {
        /// <summary>
        /// SqlServer
        /// </summary>
        [Description("SqlServer")]
        SqlServer,
        /// <summary>
        /// MySql
        /// </summary>
        [Description("MySql")]
        MySql,
        /// <summary>
        /// SQLite
        /// </summary>
        [Description("SQLite")]
        SQLite,
        /// <summary>
        /// PostgreSQL
        /// </summary>
        [Description("PostgreSQL")]
        PostgreSQL,
        /// <summary>
        /// Oracle
        /// </summary>
        [Description("Oracle")]
        Oracle
    }
}
