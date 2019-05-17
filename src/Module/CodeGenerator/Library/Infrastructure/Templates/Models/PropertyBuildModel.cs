using System.Linq;
using Nm.Module.CodeGenerator.Domain.Property;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Models
{
    /// <summary>
    /// 属性生成模型
    /// </summary>
    public class PropertyBuildModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否继承自基类实体
        /// </summary>
        public bool IsInherit { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 刻度
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 关联枚举
        /// </summary>
        public EnumBuildModel Enum { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否在列表页中显示
        /// </summary>
        public bool ShowInList { get; set; }

        /// <summary>
        /// 启用默认值
        /// </summary>
        public bool HasDefaultValue { get; set; }

        /// <summary>
        /// 小驼峰名称
        /// </summary>
        public string CamelName => Name.First().ToString().ToLower() + Name.Substring(1);
    }
}
