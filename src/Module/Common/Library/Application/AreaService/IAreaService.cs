using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AreaService.ViewModels;
using Nm.Module.Common.Domain.Area.Models;

namespace Nm.Module.Common.Application.AreaService
{
    /// <summary>
    /// 区划代码服务
    /// </summary>
    public interface IAreaService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(AreaQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(AreaAddModel model);

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
        Task<IResultModel> Update(AreaUpdateModel model);

        /// <summary>
        /// 爬取数据
        /// <see cref="http://www.mca.gov.cn/article/sj/xzqh/2019/201901-06/201905271445.html"/>
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Crawling();

        /// <summary>
        /// 查询子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IResultModel> QueryChildren(int parentId);
    }
}
