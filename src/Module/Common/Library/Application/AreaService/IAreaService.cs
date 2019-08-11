using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AreaService.ViewModels;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Domain.Area.Models;
using Nm.Module.Common.Infrastructure.AreaCrawling;

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
        /// 插入爬取数据
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> CrawlInsert(IList<AreaCrawlingModel> list);

        /// <summary>
        /// 查询子节点
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        Task<IResultModel<IList<AreaEntity>>> QueryChildren(string parentCode);
    }
}
