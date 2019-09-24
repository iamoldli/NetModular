using System.Threading.Tasks;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 创建数据库事件
    /// </summary>
    public interface ICreateDatabaseEvent
    {
        /// <summary>
        /// 创建之前
        /// </summary>
        /// <param name="dbContext">数据库上线问</param>
        /// <returns></returns>
        Task Before(IDbContext dbContext);

        /// <summary>
        /// 创建之后
        /// </summary>
        /// <param name="dbContext">数据库上线问</param>
        /// <returns></returns>
        Task After(IDbContext dbContext);
    }
}
