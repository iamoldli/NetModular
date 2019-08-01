using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Domain.Button.Models;

namespace Nm.Module.Admin.Application.ButtonService
{
    /// <summary>
    /// 按钮服务
    /// </summary>
    public interface IButtonService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ButtonQueryModel model);
    }
}
