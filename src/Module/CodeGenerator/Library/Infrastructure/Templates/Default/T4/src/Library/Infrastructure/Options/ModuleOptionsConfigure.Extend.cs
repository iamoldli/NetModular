using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.Library.Infrastructure.Options
{
    public partial class ModuleOptionsConfigure : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;

        public ModuleOptionsConfigure(TemplateBuildModel model)
        {
            _model = model;
            _prefix = model.Project.Prefix;
        }


        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/Library/Infrastructure/Options");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            var filePath = Path.Combine(dir, "ModuleOptionsConfigure.cs");
            File.WriteAllText(filePath, content);
        }
    }
}
