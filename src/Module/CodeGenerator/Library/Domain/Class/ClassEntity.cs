using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.CodeGenerator.Domain.Class
{
    /// <summary>
    /// 类信息
    /// </summary>
    [Table("Class")]
    public partial class ClassEntity : EntityBase
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 基类类型
        /// </summary>
        public BaseEntityType BaseEntityType { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remarks { get; set; }
    }
}
