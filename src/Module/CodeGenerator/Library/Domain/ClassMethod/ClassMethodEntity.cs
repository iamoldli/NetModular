using System;
using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities.Extend;

namespace Tm.Module.CodeGenerator.Domain.ClassMethod
{
    /// <summary>
    /// 实体方法
    /// </summary>
    [Table("Class_Method")]
    public class ClassMethodEntity : EntityBase
    {
        /// <summary>
        /// 实体编号
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        public bool Query { get; set; }

        /// <summary>
        /// 添加
        /// </summary>
        public bool Add { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// 编辑
        /// </summary>
        public bool Edit { get; set; }
    }
}
