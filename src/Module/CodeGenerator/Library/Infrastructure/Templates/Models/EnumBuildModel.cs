using System;
using System.Collections.Generic;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Models
{
    /// <summary>
    /// 枚举生成模型
    /// </summary>
    public class EnumBuildModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 项列表
        /// </summary>
        public List<EnumItemBuildModel> ItemList { get; set; }
    }
}
