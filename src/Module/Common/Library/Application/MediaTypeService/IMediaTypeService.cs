using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.MediaTypeService.ViewModels;
using Nm.Module.Common.Domain.MediaType.Models;

namespace Nm.Module.Common.Application.MediaTypeService
{
    public interface IMediaTypeService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(MediaTypeQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MediaTypeAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(int id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(MediaTypeUpdateModel model);
    }
}
