using System;
using System.ComponentModel.DataAnnotations;
using NetModular.Module.CodeGenerator.Domain.Property;

namespace NetModular.Module.CodeGenerator.Application.PropertyService.ViewModels
{
    public class PropertyBaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "请选择类型")]
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
        public Guid? EnumId { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "请输入备注")]
        public string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否在列表页中显示
        /// </summary>
        public bool ShowInList { get; set; }

        /// <summary>
        /// 启用默认值
        /// </summary>
        public bool HasDefaultValue { get; set; }
    }
}
