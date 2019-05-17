using System.Collections.Generic;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Models
{
    /// <summary>
    /// 项目生成模型
    /// </summary>
    public class ProjectBuildModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 类列表
        /// </summary>
        public List<ClassBuildModel> ClassList { get; set; } = new List<ClassBuildModel>();

        /// <summary>
        /// 前端代码目录名称
        /// </summary>
        public string WebUIDicName => $"{Prefix.ToLower()}-module-{Code.ToLower()}";
    }
}
