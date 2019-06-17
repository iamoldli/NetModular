using System.Collections.Generic;

namespace Nm.Module.Common.Application.AreaService.ResultModels
{
    /// <summary>
    /// 区域树形结构
    /// </summary>
    public class AreaTreeResultModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<AreaTreeResultModel> Children { get; set; } = new List<AreaTreeResultModel>();
    }
}
