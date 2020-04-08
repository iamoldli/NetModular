namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 租户解析器
    /// </summary>
    public interface ITenantResolver
    {
        /// <summary>
        /// 解析租户ID
        /// </summary>
        /// <returns>租户ID</returns>
        int Resolve();
    }
}
