using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities.Extend;

namespace Tm.Module.CodeGenerator.Domain.Project
{
    /// <summary>
    /// 项目
    /// </summary>
    [Table("Project")]
    public partial class ProjectEntity : EntityBaseWithSoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
