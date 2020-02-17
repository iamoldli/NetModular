namespace NetModular.Lib.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体Sql生成器
    /// </summary>
    public interface IEntitySqlBuilder
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        EntitySql Build();
    }
}
