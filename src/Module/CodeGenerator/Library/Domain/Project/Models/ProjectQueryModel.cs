using Nm.Lib.Data.Query;

namespace Nm.Module.CodeGenerator.Domain.Project.Models
{
    public class ProjectQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
