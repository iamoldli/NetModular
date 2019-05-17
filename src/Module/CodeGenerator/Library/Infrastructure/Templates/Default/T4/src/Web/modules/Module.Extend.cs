using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.Web.modules
{
    public partial class Module : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public Module(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/Web/modules/" + _model.Project.Code);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            var filePath = Path.Combine(dir, "module.json");
            File.WriteAllText(filePath, content);
        }
    }
}
