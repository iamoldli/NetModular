using System.Threading.Tasks;

namespace Nm.Lib.Utils.Core.Interfaces
{
    /// <summary>
    /// 应用关闭处理接口
    /// </summary>
    public interface IAppShutdownHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <returns></returns>
        Task Handle();
    }
}
