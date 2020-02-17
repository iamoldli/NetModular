using Quartz;
using System.Threading.Tasks;

namespace NetModular.Lib.Quartz.Abstractions
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface ITask : IJob
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task Execute(ITaskExecutionContext context);
    }
}
