using System;
using System.Collections.Generic;
using Nm.Module.Admin.Application.MenuService.ResultModels;

namespace Nm.Module.Admin.Application.DepartmentService.ResultModels
{
    public class DepartmentTreeResultModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<DepartmentTreeResultModel> Children = new List<DepartmentTreeResultModel>();
    }
}
