using System;
using System.Linq;
using Tm.Module.CodeGenerator.Domain.ModelProperty;
using Tm.Module.CodeGenerator.Domain.Property;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Models
{
    /// <summary>
    /// 模型属性模型
    /// </summary>
    public class ModelPropertyBuildModel
    {
        /// <summary>
        /// 模型类型
        /// </summary>
        public ModelType ModelType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool Nullable { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 关联枚举
        /// </summary>
        public EnumBuildModel Enum { get; set; }

        /// <summary>
        /// 小驼峰名称
        /// </summary>
        public string CamelName => Name.First().ToString().ToLower() + Name.Substring(1);
    }
}
