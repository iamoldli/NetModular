using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4
{
    public partial class PublishScript : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;

        public PublishScript(TemplateBuildModel model)
        {
            _model = model;
            _prefix = _model.Project.Prefix.ToLower();
        }

        public void Save()
        {
            var content = TransformText();
            var filePath = Path.Combine(_model.RootPath, _model.Project.Code, "publish.ps1");
            File.WriteAllText(filePath, content);
        }
    }
}
