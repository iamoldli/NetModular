using System.ComponentModel;

namespace NetModular.Lib.Quartz.Abstractions
{
    public enum QuartzProvider
    {
        [Description("SqlServer")]
        SqlServer,
        [Description("MySql")]
        MySql,
        [Description("SQLite-Microsof")]
        SQLite,
        [Description("OracleODP")]
        Oracle,
        [Description("Npgsql")]
        PostgreSQL
    }
}
