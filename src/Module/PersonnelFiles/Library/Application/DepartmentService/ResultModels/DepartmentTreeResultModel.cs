using System;
using Tm.Lib.Utils.Core.Result;

namespace Tm.Module.PersonnelFiles.Application.DepartmentService.ResultModels
{
    public class DepartmentTreeResultModel : TreeResultModel<DepartmentTreeResultModel>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
