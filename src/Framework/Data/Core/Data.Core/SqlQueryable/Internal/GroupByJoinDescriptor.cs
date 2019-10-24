namespace Nm.Lib.Data.Core.SqlQueryable.Internal
{
    internal class GroupByJoinDescriptor
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 连接信息
        /// </summary>
        public QueryJoinDescriptor JoinDescriptor { get; set; }
    }
}
