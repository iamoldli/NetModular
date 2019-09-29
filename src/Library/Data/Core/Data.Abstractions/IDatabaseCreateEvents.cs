using System.Threading.Tasks;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 数据库创建事件
    /// </summary>
    public interface IDatabaseCreateEvents
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        IDbContext DbContext { get; set; }

        /// <summary>
        /// 创建之前
        /// </summary>
        /// <returns></returns>
        Task Before();

        /// <summary>
        /// 创建之后
        /// </summary>
        /// <returns></returns>
        Task After();
    }
}
