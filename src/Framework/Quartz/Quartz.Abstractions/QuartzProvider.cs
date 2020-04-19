using System.ComponentModel;

namespace NetModular.Lib.Quartz.Abstractions
{
    public enum QuartzProvider
    {
        [Description("SqlServer")]
        SqlServer,
        [Description("MySql")]
        MySql,
        [Description("SQLite-Microsoft")]
        SQLite,
        [Description("OracleODP")]
        Oracle,
        [Description("Npgsql")]
        PostgreSQL
    }
}
