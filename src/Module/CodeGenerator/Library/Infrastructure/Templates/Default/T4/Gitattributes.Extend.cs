using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4
{
    public partial class Gitattributes : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public Gitattributes(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var content = TransformText();
            var filePath = Path.Combine(_model.RootPath, _model.Project.Code, ".gitattributes");
            File.WriteAllText(filePath, content);
        }
    }
}
