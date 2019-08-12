using System;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.PositionService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Position.Models;

namespace Tm.Module.PersonnelFiles.Application.PositionService
{
    /// <summary>
    /// 岗位服务
    /// </summary>
    public interface IPositionService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(PositionQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(PositionAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(PositionUpdateModel model);

        /// <summary>
        /// 查询指定部门下的岗位列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<IResultModel> Select(Guid departmentId);
    }
}
