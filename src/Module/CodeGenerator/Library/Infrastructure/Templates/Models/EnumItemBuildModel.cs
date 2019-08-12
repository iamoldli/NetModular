namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Models
{
    /// <summary>
    /// 枚举项生成模型
    /// </summary>
    public class EnumItemBuildModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
