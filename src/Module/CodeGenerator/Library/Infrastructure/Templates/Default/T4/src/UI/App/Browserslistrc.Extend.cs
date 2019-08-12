using System.IO;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App
{
    public partial class Browserslistrc : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public Browserslistrc(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, $"src/UI/{_model.Project.WebUIDicName}");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            var filePath = Path.Combine(dir, ".browserslistrc");
            File.WriteAllText(filePath, content);
        }
    }
}
