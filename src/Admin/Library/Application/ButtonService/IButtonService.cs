using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.Button.Models;

namespace NetModular.Module.Admin.Application.ButtonService
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
