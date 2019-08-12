using System.IO;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4
{
    public partial class Gitignore : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public Gitignore(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var content = TransformText();
            var filePath = Path.Combine(_model.RootPath, _model.Project.Code, ".gitignore");
            File.WriteAllText(filePath, content);
        }
    }
}
