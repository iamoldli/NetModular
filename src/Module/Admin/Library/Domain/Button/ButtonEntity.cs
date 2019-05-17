using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Admin.Domain.Button
{
    /// <summary>
    /// 按钮
    /// </summary>
    [Table("Button")]
    public partial class ButtonEntity : EntityBase
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
