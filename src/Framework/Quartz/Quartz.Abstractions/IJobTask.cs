using Quartz;
using System.Threading.Tasks;

namespace NetModular.Lib.Quartz.Abstractions
{
    public interface IJobTask : IJob
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task Execute(IJobTaskContext context);
    }
}
