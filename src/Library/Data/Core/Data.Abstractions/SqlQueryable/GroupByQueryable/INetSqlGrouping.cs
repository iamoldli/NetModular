namespace Tm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询对象
    /// </summary>
    public interface INetSqlGrouping<out TKey>
    {
        TKey Key { get; }

        long Count();
    }
}
