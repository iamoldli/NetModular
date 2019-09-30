namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer.Sql
{
    public static class AuditInfoSql
    {
        /// <summary>
        /// 查询最近一周访问量
        /// </summary>
        public const string QueryLatestWeekPv = @"SELECT CONVERT(VARCHAR(100), ExecutionTime, 23) AS [Key],
       COUNT(0) AS [Value]
FROM dbo.{0}
WHERE ExecutionTime > DATEADD(DAY, -6, GETDATE())
GROUP BY CONVERT(VARCHAR(100), ExecutionTime, 23)
ORDER BY [Key];";
    }
}
