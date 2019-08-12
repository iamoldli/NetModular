using System;
using System.Threading.Tasks;

namespace Tm.Lib.Quartz.Abstractions
{
    /// <summary>
    /// 工作任务日志记录器接口
    /// </summary>
    public interface IJobLogger
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        Guid JobId { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">消息</param>
        Task Info(string msg);

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        Task Debug(string msg);

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        Task Error(string msg);
    }
}
