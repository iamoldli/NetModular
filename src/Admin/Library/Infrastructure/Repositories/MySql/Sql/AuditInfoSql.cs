namespace Nm.Module.Admin.Infrastructure.Repositories.MySql.Sql
{
    public static class AuditInfoSql
    {
        /// <summary>
        /// 查询最近一周访问量
        /// </summary>
        public const string QueryLatestWeekPv = @"SELECT
	DATE_FORMAT(ExecutionTime, '%Y-%m-%d' ) `Key`,
	COUNT(0) `Value`
FROM
	{0} 
WHERE
	ExecutionTime > DATE_FORMAT( DATE_ADD(NOW(), interval -6 day), '%Y-%m-%d' )
GROUP BY
	`Key` ORDER BY `Key`";
    }
}
