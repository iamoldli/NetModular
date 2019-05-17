using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates
{
    /// <summary>
    /// 模板生成器接口
    /// </summary>
    public interface ITemplateBuilder
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="model"></param>
        void Build(TemplateBuildModel model);
    }
}
