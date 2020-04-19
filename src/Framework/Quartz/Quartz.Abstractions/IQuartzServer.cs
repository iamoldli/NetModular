using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace NetModular.Lib.Quartz.Abstractions
{
    /// <summary>
    /// Quartz服务接口
    /// </summary>
    public interface IQuartzServer
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task Start(CancellationToken cancellation = default);

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task Stop(CancellationToken cancellation = default);

        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task Restart(CancellationToken cancellation = default);

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="trigger"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task AddJob(IJobDetail jobDetail, ITrigger trigger, CancellationToken cancellation = default);

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task PauseJob(JobKey jobKey, CancellationToken cancellation = default);

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task ResumeJob(JobKey jobKey, CancellationToken cancellation = default);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteJob(JobKey jobKey, CancellationToken cancellation = default);
    }
}
